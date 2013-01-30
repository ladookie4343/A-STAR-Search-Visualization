using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1Main
{
    class ConsideredPath
    {
        public string Tail { get; set; }
        public string Head { get; set; }
        public bool CurrentlyOptimal { get; set; }
        public double GScore { get; set; }
        public double HScore { get; set; }
        public double FScore { get; set; }

        public ConsideredPath(string tail, string head, bool currentlyOptimal,
            double gScore, double hScore, double fScore)
        {
            Tail = tail;
            Head = head;
            CurrentlyOptimal = currentlyOptimal;
            GScore = gScore;
            HScore = hScore;
            FScore = fScore;
        }
    }

    /// <summary>
    /// This Class implements the A* search algorithm with the getShortestPath method.
    /// </summary>
    class AStar
    {
        public List<ConsideredPath> ConsideredPath { get; private set; }
        public List<Vertex> Vertices { get; private set; }
        public HashSet<Vertex> ClosedSet { get; private set; }
        public PriorityQueue<double, Vertex> OpenSet { get; private set; }
        public double[] GScore { get; private set; }
        public double[] HScore { get; private set; }      

        public AStar(string locationFile, string connectionFile)
        {
            try
            {
                Vertices = Utilities.getVertices(locationFile);
                Utilities.createAdjLists(Vertices, connectionFile);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }  

        private Vertex startVertex, goalVertex;
        private int[] cameFrom;

        private void initShortestPath(string startCity, string destination, IHeurisic heuristic)
        {
            ClosedSet = new HashSet<Vertex>();
            OpenSet = new PriorityQueue<double, Vertex>(new ByFScore());
            cameFrom = new int[Vertices.Count];

            GScore = new double[Vertices.Count];
            HScore = new double[Vertices.Count];

            startVertex = Vertices.Find(vertex => vertex.Name == startCity);
            goalVertex = Vertices.Find(vertex => vertex.Name == destination);

            GScore[startVertex.ID] = 0;
            HScore[startVertex.ID] = heuristic.costEstimate(startVertex, goalVertex);
            startVertex.FScore = GScore[startVertex.ID] + HScore[startVertex.ID];

            OpenSet.Add(new KeyValuePair<double, Vertex>(startVertex.FScore, startVertex));

            ConsideredPath = new List<ConsideredPath>();
        }

        public List<Vertex> getShortestPath(string startCity, string destination, string[] locationsToAvoid, IHeurisic heuristic)
        {
            foreach (string s in locationsToAvoid)
            {
                Utilities.deleteVertex(s);
            }


            initShortestPath(startCity, destination, heuristic);

            while (!OpenSet.IsEmpty)
            {
                Vertex x = OpenSet.DequeueValue();

                if (x.Name != startVertex.Name)
                {
                    ConsideredPath.Add(new ConsideredPath(Vertices[cameFrom[x.ID]].Name, x.Name, true, 0, 0, 0));
                }

                if (x.ID == goalVertex.ID)
                {
                    return reconstructPath(cameFrom, startVertex, goalVertex);
                }

                ClosedSet.Add(x);

                foreach (int adjVertex in x.AdjList)
                {
                    Vertex y = Vertices[adjVertex];
                    if (ClosedSet.Contains(y)) continue;

                    double tentativeGScore = GScore[x.ID] + heuristic.costEstimate(x, y);
                    bool tentativeIsBetter;

                    if (!OpenSet.Contains(new KeyValuePair<double, Vertex>(y.FScore, y)))
                    {
                        HScore[y.ID] = heuristic.costEstimate(y, goalVertex);
                        tentativeIsBetter = true;
                    }
                    else if (tentativeGScore < GScore[y.ID])
                    {
                        tentativeIsBetter = true;
                    }
                    else
                    {
                        tentativeIsBetter = false;
                    }

                    if (tentativeIsBetter)
                    {
                        cameFrom[y.ID] = x.ID;
                        GScore[y.ID] = tentativeGScore;
                        y.FScore = GScore[y.ID] + HScore[y.ID];
                        OpenSet.Add(new KeyValuePair<double, Vertex>(y.FScore, y));
                    }


                    ConsideredPath.Add(new ConsideredPath(x.Name, y.Name, false, GScore[y.ID], HScore[y.ID], y.FScore));
                }
            }

            // return an empty list indicating no path exists.
            return new List<Vertex>();
        }

        private List<Vertex> reconstructPath(int[] cameFrom, Vertex start, Vertex end)
        {
            Stack<Vertex> stack = new Stack<Vertex>();
            int idx = end.ID;

            stack.Push(end);
            while (cameFrom[idx] != start.ID)
            {
                stack.Push(Vertices[cameFrom[idx]]);
                idx = cameFrom[idx];
            }
            stack.Push(start);

            List<Vertex> path = new List<Vertex>();
            foreach (Vertex v in stack)
            {
                path.Add(v);
            }
            return path;
        }

        class ByFScore : IComparer<double>
        {
            public int Compare(double x, double y)
            {
                if (x > y) return 1;
                if (x < y) return -1;
                return 0;
            }
        }
    }
}
