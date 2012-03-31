using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Project1Main
{
    /// <summary>
    /// This class handles the locations.txt file. It reads the file a line at a time
    /// and creates a Vertex object for each line.
    /// It also handles the connections.txt file and creates adjLists for all the vertices.
    /// </summary>
    static class Utilties
    {
        public static List<Vertex> getVertices(string locations)
        {
            List<Vertex> vertices = new List<Vertex>();
            Vertex v = new Vertex("", -1, -1, 0); // dummy vertex
            vertices.Add(v);
            using (StreamReader reader = new StreamReader(locations))
            {
                string line;
                int vertexNumber = 1;
                while ((line = reader.ReadLine()) != "END")
                {
                    vertices.Add(createVertex(line, vertexNumber++));
                }
            }

            return vertices;
        }

        private static Vertex createVertex(string line, int vertexNumber)
        {
            string[] vertexPart = line.Split(' ');
            string name = vertexPart[0];
            int x, y;
                x = int.Parse(vertexPart[1]);
                y = int.Parse(vertexPart[2]);

            return new Vertex(name, x, y, vertexNumber);
        }

        private static List<Vertex> vertices_;

        public static void createAdjLists(List<Vertex> vertices, string connections)
        {
            vertices_ = vertices;
            initAdjList(connections);
        }

        public static List<int> getAdjList(int vertexNumber)
        {
            return vertices_[vertexNumber].AdjList;
        }

        public static void deleteVertex(string vertexName)
        {
            Vertex vertex = vertices_.Find(v => v.Name == vertexName);
            foreach (int adjVertex in vertex.AdjList)
            {
                List<int> adjadjList = vertices_[adjVertex].AdjList;
                adjadjList.Remove(adjadjList.Find(edge => edge == vertex.ID));
            }
            vertex.AdjList.Clear();
        }

        private static void initAdjList(string connections)
        {
            using (StreamReader reader = new StreamReader(connections))
            {
                string line;
                while ((line = reader.ReadLine()) != "END")
                {
                    createAdjList(line);
                }
            }
        }

        private static void createAdjList(string line)
        {
            string[] connectionPart = line.Split(' ');

            string vertexName = connectionPart[0];
            Vertex currentVertex = vertices_.Find(v => v.Name == vertexName);
            int numEdges = int.Parse(connectionPart[1]);

            List<int> adjList = new List<int>();
            for (int i = 0; i < numEdges; i++)
            {
                string adjVertexName = connectionPart[i + 2];
                Vertex adjacentVertex = vertices_.Find(v => v.Name == adjVertexName);
                adjList.Add(adjacentVertex.ID);
            }
            currentVertex.AdjList = adjList;
        }
    }
}
