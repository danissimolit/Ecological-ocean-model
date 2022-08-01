using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    public class Obstacle : Cell
    {
        public Obstacle(Ocean owner, int X, int Y) : base(owner, X, Y)
        {
            image = Constants.defaultObstacleImage;
        }
    }
}
