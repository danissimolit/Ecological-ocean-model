using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    abstract public class Cell
    {
        protected Ocean _owner;
        public Coordinate coord { get; set; }
        protected char image;
        public bool isMoved { get; set; } = false;


        public Cell(Ocean owner, int X, int Y)
        {
            image = Constants.defaultImage;
            coord = new Coordinate(X, Y);
            _owner = owner;
        }

        public Coordinate GetEmptyNeighborCoord()
        {
            Coordinate[] desiredCells = new Coordinate[4];
            int counter = 0;
            if (NorthCell() == null)
            {
                desiredCells[counter] = NorthCellCoord();
                counter++;
            }

            if (EastCell() == null)
            {
                desiredCells[counter] = EastCellCoord();
                counter++;
            }

            if (SouthCell() == null)
            {
                desiredCells[counter] = SouthCellCoord();
                counter++;
            }

            if (WestCell() == null)
            {
                desiredCells[counter] = WestCellCoord();
                counter++;
            }

            if (counter == 0)
            {
                return coord;
            }
            else if (counter == 1)
            {
                return desiredCells[0];
            }
            else return desiredCells[Rand.GetRandomIndex(counter)];
        }

        public Coordinate GetNeighborWithImageCoord(char image)
        {
            Cell[] desiredCells = new Cell[4];
            int counter = 0;
            if (NorthCell() != null && NorthCell().GetImage() == image)
            {
                desiredCells[counter] = NorthCell();
                counter++;
            }

            if (EastCell() != null && EastCell().image == image)
            {
                desiredCells[counter] = EastCell();
                counter++;
            }

            if (SouthCell() != null && SouthCell().image == image)
            {
                desiredCells[counter] = SouthCell();
                counter++;
            }

            if (WestCell() != null && WestCell().image == image)
            {
                desiredCells[counter] = WestCell();
                counter++;
            }

            if (counter == 0)
            {
                return coord;
            }
            else if (counter == 1)
            {
                return desiredCells[0].coord;
            }
            else return desiredCells[Rand.GetRandomIndex(counter)].coord;
            //else return Rand.GetRandomFromArray(desiredCells, counter);
        }

        public Cell NorthCell()
        {
            return _owner[NorthCellCoord().X, NorthCellCoord().Y];
        }

        public Cell WestCell()
        {
            return _owner[WestCellCoord().X, WestCellCoord().Y];
        }

        public Cell SouthCell()
        {
            return _owner[SouthCellCoord().X, SouthCellCoord().Y];
        }

        public Cell EastCell()
        {
            return _owner[EastCellCoord().X, EastCellCoord().Y];

            
        }

        public Coordinate NorthCellCoord()
        {
            if (coord.X != 0)
            {
                return new Coordinate(coord.X - 1, coord.Y);
            }
            else return new Coordinate(_owner.rows - 1, coord.Y);
        }

        public Coordinate WestCellCoord()
        {
            if (coord.Y != 0)
            {
                return new Coordinate(coord.X, coord.Y - 1);
            }
            else return new Coordinate(coord.X, _owner.cols - 1);
        }

        public Coordinate SouthCellCoord()
        {
            if (coord.X != _owner.rows - 1)
            {
                return new Coordinate(coord.X + 1, coord.Y);
            }
            else return new Coordinate(0, coord.Y);
        }

        public Coordinate EastCellCoord()
        {
            if (this.coord.Y != _owner.cols - 1)
            {
                return new Coordinate(coord.X, coord.Y + 1);
            }
            else return new Coordinate(coord.X, 0);
        }



        public void SetOcean(Ocean ocean)
        {
            _owner = ocean;
        }

        public char GetImage()
        {
            return image;
        }

        public virtual void ProcessForPredator()
        {

        }

        public virtual void ProcessForPrey()
        {

        }

        public virtual bool isBonusOn()
        {
            return false;
        }

        public virtual void SetBonus(bool bonus)
        {

        }
    }
}
