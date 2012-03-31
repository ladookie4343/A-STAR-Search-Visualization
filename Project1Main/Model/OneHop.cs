using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1Main
{
    class OneHop : IHeurisic
    {
        public double costEstimate(Vertex start, Vertex goal)
        {
            return 1.0;
        }
    }
}
