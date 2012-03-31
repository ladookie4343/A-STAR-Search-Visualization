using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1Main
{
    /// <summary>
    /// This class handles the calculation of distance between two
    /// vertices. This code is used several times in different files
    /// so I decided to separate it out into its own class.
    /// </summary>
    static class DistanceCalculator
    {
        public static double computeDistance(Vertex firstVertex, Vertex secondVertex)
        {
            double xDistance = secondVertex.X - firstVertex.X;
            double yDistance = secondVertex.Y - firstVertex.Y;
            return Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
        }
    }
}
