using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    public class Prey : Cell
    {
        public int timeToReproduce;
        //public int immunityTime = -1;
        public bool areSpikesOn { get; set; } = false;

        public Prey(Ocean owner, int X, int Y) : base(owner, X, Y)
        {
            image = Constants.defaultPreyImage;
            timeToReproduce = Constants.timeToReproduce;
            //_owner = owner;
        }

        public override void ProcessForPrey()
        {
            Coordinate emptyNeighbor = GetEmptyNeighborCoord();
            Coordinate bonusNeigbor = GetNeighborWithImageCoord(Constants.defaultBonusImage);
            if (timeToReproduce <= 0)
            {
                if (bonusNeigbor != coord)
                {
                    Reproduce(bonusNeigbor);
                    areSpikesOn = true;

                    return;
                }
                else if(emptyNeighbor != coord)
                {
                    Reproduce(emptyNeighbor);
                    return;
                }
            }
            else if (bonusNeigbor != coord)
            {
                Move(bonusNeigbor);
                areSpikesOn = true;
                timeToReproduce--;
            }
            else if (emptyNeighbor != coord)
            {
                Move(emptyNeighbor);
                timeToReproduce--;
            }
            else timeToReproduce--;
        }

        

        protected virtual void Move(Coordinate newCoord)
        {
            _owner.AddChangedCellsCoord(new Coordinate(coord));
            _owner.AddChangedCellsCoord(new Coordinate(newCoord));
            _owner[newCoord.X, newCoord.Y] = _owner[coord.X, coord.Y];
            _owner[coord.X, coord.Y] = null;
            _owner[newCoord.X, newCoord.Y].coord.X = newCoord.X;
            _owner[newCoord.X, newCoord.Y].coord.Y = newCoord.Y;
            _owner[newCoord.X, newCoord.Y].isMoved = true;
        }

        protected virtual void Reproduce(Coordinate newCoord)
        {
            _owner.AddChangedCellsCoord(new Coordinate(coord));
            _owner.AddChangedCellsCoord(new Coordinate(newCoord));
            _owner[newCoord.X, newCoord.Y] = _owner[coord.X, coord.Y];
            _owner[coord.X, coord.Y] = new Prey(_owner, coord.X, coord.Y);
            _owner[newCoord.X, newCoord.Y].coord.X = newCoord.X;
            _owner[newCoord.X, newCoord.Y].coord.Y = newCoord.Y;
            _owner[newCoord.X, newCoord.Y].isMoved = true;

            _owner.numPreys++;
            timeToReproduce = Constants.timeToReproduce;
        }

        public override bool isBonusOn()
        {
            return areSpikesOn;
        }

        public override void SetBonus(bool bonus)
        {
            areSpikesOn = bonus;
        }

    }

}
