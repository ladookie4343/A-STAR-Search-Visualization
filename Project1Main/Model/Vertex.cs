using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1Main
{
    /// <summary>
    /// This class represents a node in the WeightedGraph class corresponding to a city.
    /// It stores the name and coordinates of each city.
    /// </summary>
    public class Vertex
    {
        private string name;
        public string Name { get { return name; } }

        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private double fScore;
        public double FScore 
        { 
            get { return fScore; } 
            set { fScore = value; } 
        }

        private List<int> adjList = new List<int>();
        public List<int> AdjList
        {
            get { return adjList; }
            set { adjList = value; }
        }

        private int id;
        public int ID { get { return id; } }

        public Vertex(string name, int x, int y, int id)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.id = id;
        }
    }
}
