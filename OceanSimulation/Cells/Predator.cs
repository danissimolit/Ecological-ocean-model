using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    public class Predator : Prey
    {
        public int timeToEat = Constants.timeToFeed;
        public bool isBonusMealOn = false;

        public Predator(Ocean owner, int X, int Y) : base(owner, X, Y)
        {
            image = Constants.defaultPredatorImage;
        }

        public override void ProcessForPredator()
        {
            if (timeToEat <= 0)
            {
                Die();
                return;
            }
            else
            {
                timeToEat--;

                if (timeToEat <= Constants.timeToFeed)
                {
                    isBonusMealOn = false;
                    _owner.AddChangedCellsCoord(new Coordinate(coord));
                }

                Coordinate neighborPrey = GetNeighborWithImageCoord(Constants.defaultPreyImage);
                Coordinate neighborBonus = GetNeighborWithImageCoord(Constants.defaultBonusImage);

                if (timeToReproduce <= 0)
                {
                    if (neighborBonus != coord)
                    {
                        Reproduce(neighborBonus);
                        isBonusMealOn = true;
                        timeToEat = Constants.BonusTimeToEatForPredator;
                    }
                    else if (neighborPrey != coord)
                    {
                        if (_owner._cells[neighborPrey.X, neighborPrey.Y].isBonusOn())
                        {
                            DieFromBonus(neighborPrey);
                            return;
                        }
                        else 
                        {
                            Reproduce(neighborPrey);
                            ChangeTimeToEat();
                        }
                    }
                    else 
                    {
                        Coordinate newCell = GetEmptyNeighborCoord();
                        if (newCell != coord)
                        {
                            Reproduce(newCell);
                        }
                    }
                }
                else if (neighborBonus != coord)
                {
                    EatBonus(neighborBonus);
                    timeToReproduce--;
                }
                else if (neighborPrey != coord)
                {
                    if (_owner._cells[neighborPrey.X, neighborPrey.Y].isBonusOn())
                    {
                        DieFromBonus(neighborPrey);
                        return;
                    }
                    else
                    {
                        Eat(neighborPrey);
                        timeToReproduce--;
                    }
                }
                else
                {
                    Coordinate newCell = GetEmptyNeighborCoord();
                    if (newCell != coord)
                    {
                        Move(newCell);
                    }
                    timeToReproduce--;
                }
            }
        }

        protected void ChangeTimeToEat()
        {
            if (isBonusMealOn)
            {
                return;
            }
            else timeToEat = Constants.timeToFeed;
        }

        protected void Die()
        {
            _owner[coord.X, coord.Y] = null;
            _owner.AddChangedCellsCoord(new Coordinate(coord));
            _owner.numPredators--;
        }

        protected void DieFromBonus(Coordinate preyWithBonus)
        {
            Die();
            _owner[preyWithBonus.X, preyWithBonus.Y].SetBonus(false);
            _owner.AddChangedCellsCoord(new Coordinate(preyWithBonus));
        }

        //protected void CheckPreyForSpikes(Coordinate prey)
        //{
        //    if (_owner._cells[prey.X, prey.Y])
        //    {

        //    }
        //}

        protected void EatBonus(Coordinate bonusCoord)
        {
            Move(bonusCoord);
            isBonusMealOn = true;
            timeToEat = Constants.BonusTimeToEatForPredator;
        }

        protected void Eat(Coordinate preyCoord)
        {
            Move(preyCoord);

            _owner.numPreys--; 
            ChangeTimeToEat();
        }

        protected override void Move(Coordinate newCoord)
        {
            _owner.AddChangedCellsCoord(new Coordinate(coord));
            _owner.AddChangedCellsCoord(new Coordinate(newCoord));
            _owner[newCoord.X, newCoord.Y] = _owner[coord.X, coord.Y];
            _owner[coord.X, coord.Y] = null;
            _owner[newCoord.X, newCoord.Y].coord.X = newCoord.X;
            _owner[newCoord.X, newCoord.Y].coord.Y = newCoord.Y;
            _owner[newCoord.X, newCoord.Y].isMoved = true;
        }

        protected override void Reproduce(Coordinate newCoord)
        {
            if (_owner[newCoord.X, newCoord.Y] != null 
                && _owner[newCoord.X, newCoord.Y].GetImage() == Constants.defaultPreyImage)
            {
                _owner.numPreys--;
            }

            _owner.AddChangedCellsCoord(new Coordinate(coord));
            _owner.AddChangedCellsCoord(new Coordinate(newCoord));
            _owner[newCoord.X, newCoord.Y] = _owner[coord.X, coord.Y];
            _owner[coord.X, coord.Y] = new Predator(_owner, coord.X, coord.Y);
            _owner[newCoord.X, newCoord.Y].coord.X = newCoord.X;
            _owner[newCoord.X, newCoord.Y].coord.Y = newCoord.Y;
            _owner[newCoord.X, newCoord.Y].isMoved = true;

            _owner.numPredators++;
            timeToReproduce = Constants.timeToReproduce;
        }

        public override bool isBonusOn()
        {
            return isBonusMealOn;
        }
    }

}
