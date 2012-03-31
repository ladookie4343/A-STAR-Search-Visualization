using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1Main
{
    struct ConsideredPath
    {
        public ConsideredPath(string tail, string head, bool currentlyOptimal,
            double gScore, double hScore, double fScore)
        {
            this.tail = tail;
            this.head = head;
            this.currentlyOptimal = currentlyOptimal;
            this.gScore = gScore;
            this.hScore = hScore;
            this.fScore = fScore;
        }

        private string tail;
        public string Tail
        {
            get { return tail; }
            set { tail = value; }
        }

        private string head;
        public string Head
        {
            get { return head; }
            set { head = value; }
        }

        private bool currentlyOptimal;
        public bool CurrentlyOptimal
        {
            get { return currentlyOptimal; }
            set { currentlyOptimal = value; }
        }

        private double gScore;
        public double GScore
        {
            get { return gScore; }
            set { gScore = value; }
        }

        private double hScore;
        public double HScore
        {
            get { return hScore; }
            set { hScore = value; }
        }

        private double fScore;
        public double FScore
        {
            get { return fScore; }
            set { fScore = value; }
        }
    }

    /// <summary>
    /// This Class implements the A* search algorithm with the getShortestPath method.
    /// </summary>
    class AStar
    {
        public AStar(string locationFile, string connectionFile)
        {
            try
            {
                vertices = Utilties.getVertices(locationFile);
                Utilties.createAdjLists(vertices, connectionFile);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #region Properties

        private List<ConsideredPath> consideredPath;
        public List<ConsideredPath> ConsideredPath
        {
            get { return consideredPath; }
        }

        private List<Vertex> vertices;
        public List<Vertex> Vertices { get { return vertices; } }

        private HashSet<Vertex> closedSet;
        public HashSet<Vertex> ClosedSet { get { return closedSet; } }

        private PriorityQueue<double, Vertex> openSet;
        public PriorityQueue<double, Vertex> OpenSet
        {
            get { return openSet; }
        }

        private double[] gScore;
        public double[] GScore { get { return gScore; } }

        private double[] hScore;
        public double[] HScore { get { return hScore; } }

        

        #endregion

        private Vertex startVertex, goalVertex;
        private int[] cameFrom;

        #region Methods

        private void initShortestPath(string startCity, string destination, IHeurisic heuristic)
        {
            closedSet = new HashSet<Vertex>();
            openSet = new PriorityQueue<double, Vertex>(new ByFScore());
            cameFrom = new int[vertices.Count];

            gScore = new double[vertices.Count];
            hScore = new double[vertices.Count];

            startVertex = vertices.Find(vertex => vertex.Name == startCity);
            goalVertex = vertices.Find(vertex => vertex.Name == destination);

            gScore[startVertex.ID] = 0;
            hScore[startVertex.ID] = heuristic.costEstimate(startVertex, goalVertex);
            startVertex.FScore = gScore[startVertex.ID] + hScore[startVertex.ID];

            openSet.Add(new KeyValuePair<double, Vertex>(startVertex.FScore, startVertex));

            consideredPath = new List<ConsideredPath>();
        }

        public List<Vertex> getShortestPath(string startCity, string destination, string[] locationsToAvoid, IHeurisic heuristic)
        {
            foreach (string s in locationsToAvoid)
            {
                Utilties.deleteVertex(s);
            }


            initShortestPath(startCity, destination, heuristic);

            while (!openSet.IsEmpty)
            {
                Vertex x = openSet.DequeueValue();

                if (x.Name != startVertex.Name)
                {
                    consideredPath.Add(new ConsideredPath(vertices[cameFrom[x.ID]].Name, x.Name, true, 0, 0, 0));
                }

                if (x.ID == goalVertex.ID)
                {
                    return reconstructPath(cameFrom, startVertex, goalVertex);
                }

                closedSet.Add(x);

                foreach (int adjVertex in x.AdjList)
                {
                    Vertex y = vertices[adjVertex];
                    if (closedSet.Contains(y)) continue;

                    double tentativeGScore = gScore[x.ID] + heuristic.costEstimate(x, y);
                    bool tentativeIsBetter;

                    if (!openSet.Contains(new KeyValuePair<double, Vertex>(y.FScore, y)))
                    {
                        hScore[y.ID] = heuristic.costEstimate(y, goalVertex);
                        tentativeIsBetter = true;
                    }
                    else if (tentativeGScore < gScore[y.ID])
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
                        gScore[y.ID] = tentativeGScore;
                        y.FScore = gScore[y.ID] + hScore[y.ID];
                        openSet.Add(new KeyValuePair<double, Vertex>(y.FScore, y));
                    }


                    consideredPath.Add(new ConsideredPath(x.Name, y.Name, false, gScore[y.ID], hScore[y.ID], y.FScore));
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
                stack.Push(vertices[cameFrom[idx]]);
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

        #endregion

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
