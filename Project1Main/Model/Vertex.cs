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
        public string Name { get; private set; }
        public int ID { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int FScore { get; set; }
        public List<int> AdjList { get; set; }

        public Vertex(string name, int x, int y, int id)
        {
            Name = name;
            X = x;
            Y = y;
            ID = id;
            AdjList = new List<int>();
        }
    }
}
