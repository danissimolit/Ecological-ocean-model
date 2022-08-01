using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    public class Coordinate
    {
        public int X;
        public int Y;

        public Coordinate()
        {
            X = 0;
            Y = 0;
        }

        public Coordinate(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Coordinate(Coordinate coord)
        {
            X = coord.X;
            Y = coord.Y;
        }


    }
}
