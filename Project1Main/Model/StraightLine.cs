using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1Main
{
    /// <summary>
    /// This class represents the straight line distance heuristic
    /// </summary>
    class StraightLine : IHeurisic
    {
        public double costEstimate(Vertex start, Vertex goal)
        {
            return DistanceCalculator.computeDistance(start, goal);
        }
    }
}
