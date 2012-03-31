using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1Main
{
    /// <summary>
    /// I decided to use the strategy pattern for implementing different types of 
    /// heuristic. Different heuristics implement this interface in their own way
    /// to achieve that specific heuristic.
    /// </summary>
    interface IHeurisic
    {
        double costEstimate(Vertex start, Vertex goal);
    }
}
