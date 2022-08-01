using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    public class Rand
    {
        public static Random random = new Random();
        public static Coordinate GetRandomCoordinate(int maxX, int maxY)
        {
            Coordinate coord = new Coordinate();
            coord.X = random.Next(0, maxX);
            coord.Y = random.Next(0, maxY);
            return coord;
        }

        public static int GetRandomIndex(int maxIndex)
        {
            return random.Next(0, maxIndex);
        }
    }
}
