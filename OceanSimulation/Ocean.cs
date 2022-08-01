using System;
using System.Threading;

namespace OceanSimulation
{
    public class Ocean : IOcean
    {
        public Cell[,] _cells { get; set; }
        public int numObstacles { get; set; }
        public int numPreys { get; set; }
        public int numPredators { get; set; }
        public int numIterations { get; set; }
        public int rows { get; set; } 
        public int cols { get; set; }
        public int pickedSize { get; set; }

        public int pickedCellBorderStyleIndex { get; set; }

        public bool successfulInitialization { get; set; } = false;

        public Coordinate[] changedCellsCoord { get; set; }

        public int changedCellsIndex { get; set; } = 0;
        public bool isUniqueCell { get; set; } = true;
        public int timeToSpawnBonus { get; set; }

        public IOutput output { get; set; }
        public IInput input { get; set; }

        


        public Ocean(IOutput output)
        {
            this.output = output;
        }

        public Ocean()
        {

        }

        public void SetOutput(IOutput output)
        {
            this.output = output;
        }

        public void SetInput(IInput input)
        {
            this.input = input;
        }

        public Cell this[int x, int y]
        {
            get
            {
                return _cells[x, y];
            }
            set
            {
                _cells[x, y] = value;
            }
        }

        public void InitCells()
        {
            EnterCells();
            AddCells();
        }

        public void SetDefaultOcean()
        {
            pickedSize = Constants.defaultPickedIndex;
            pickedCellBorderStyleIndex = Constants.defaultPickedCellBorderStyle;
            rows = Constants.defaultRows;
            cols = Constants.defaultCols;
            numPreys = Constants.defaultNumPreys;
            numPredators = Constants.defaultNumPredators;
            numObstacles = Constants.defaultNumObstacles;
            numIterations = Constants.defaultNumIterations;
            timeToSpawnBonus = Constants.timeToSpawnBonus[pickedSize];
        }

        public void CopyOceanDataFrom(Ocean oceanData)
        {
            pickedSize = oceanData.pickedSize;
            pickedCellBorderStyleIndex = oceanData.pickedCellBorderStyleIndex;
            rows = oceanData.rows;
            cols = oceanData.cols;
            numPreys = oceanData.numPreys;
            numPredators = oceanData.numPredators;
            numObstacles = oceanData.numObstacles;
            numIterations = oceanData.numIterations;
        }

        public void SetRows(int rows)
        {
            this.rows = rows;
        }

        public void SetCols(int cols)
        {
            this.cols = cols;
        }

        public void EnterCells()
        {
            try
            {
                input.EnterAll(this);
                timeToSpawnBonus = Constants.timeToSpawnBonus[pickedSize];
                successfulInitialization = true;
            }
            catch (WrongAmountInput e)
            {
                output.ShowException(e.Message);
                successfulInitialization = false;
                return;
            }
            catch (Exception ex)
            {
                output.ShowException(ex.Message);
                successfulInitialization = false;
                return;
            }
        }

        public void AddCells()
        {
            _cells = new Cell[rows, cols];
            changedCellsCoord = new Coordinate[rows * cols];
            //timeToSpawnBonus = 0;
            timeToSpawnBonus = Constants.timeToSpawnBonus[pickedSize];
            AddEmptyCells(rows, cols);
            AddObstacles();
            AddPreys();
            AddPredators();
        }

        public void AddEmptyCells(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    _cells[i, j] = null;
                }
            }
        }

        public void AddPreys()
        {
            for (int i = 0; i < numPreys; i++)
            {
                Coordinate coord = new Coordinate();
                coord = Rand.GetRandomCoordinate(rows, cols);

                if (_cells[coord.X, coord.Y] == null)
                {
                    _cells[coord.X, coord.Y] = new Prey(this, coord.X, coord.Y);
                }
                else i--;
            }
        }

        public void AddPredators()
        {

            for (int i = 0; i < numPredators; i++)
            {
                Coordinate coord = new Coordinate();
                coord = Rand.GetRandomCoordinate(rows, cols);

                if (_cells[coord.X, coord.Y] == null)
                {
                    _cells[coord.X, coord.Y] = new Predator(this, coord.X, coord.Y);

                }
                else i--;
            }
        }

        public void AddObstacles()
        {
            for (int i = 0; i < numObstacles; i++)
            {
                Coordinate coord = new Coordinate();
                coord = Rand.GetRandomCoordinate(rows, cols);

                if (_cells[coord.X, coord.Y] == null)
                {
                    _cells[coord.X, coord.Y] = new Obstacle(this, coord.X, coord.Y);
                }
                else i--;
            }
        }

        public void AddChangedCellsCoord(Coordinate coord)
        {
            for (int i = 0; i < changedCellsIndex; i++)
            {
                if (changedCellsCoord[i].X == coord.X && changedCellsCoord[i].Y == coord.Y)
                {
                    isUniqueCell = false;
                    break;
                }
            }

            if (isUniqueCell)
            {
                changedCellsCoord[changedCellsIndex] = coord;
                changedCellsIndex++;
            }

            isUniqueCell = true;
            //if (changedCellsIndex < changedCellsCoord.Length)
            //{

            //}
        }

        public void ClearChangedCellsArr()
        {
            for (int i = 0; i < changedCellsCoord.Length; i++)
            {
                changedCellsCoord[i] = null;
            }
            changedCellsIndex = 0;
        }

        public void Process()
        {
            //output.Print(this);

            while (numIterations > 0)
            {
                Iteration();

                if (numPredators == 0 && numPreys == 0)
                {
                    output.Draw();
                    break;
                }
                else if (numPredators == 0)
                {
                    output.PreysWin();
                    break;
                }
                else if (numPreys == 0)
                {
                    output.PredatorsWin();
                    break;
                }
                else if (numIterations <= 0)
                {
                    output.OutOfIterations();
                    break;
                }
            }
        }

        public void Iteration()
        {
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (_cells[i, j] == null || _cells[i, j].GetType() == typeof(Bonus))
                    {
                        continue;
                    }

                    switch (_cells[i, j].GetImage())
                    {
                        case Constants.defaultPredatorImage:
                            if (_cells[i, j].isMoved == false)
                            {
                                _cells[i, j].ProcessForPredator();
                            }

                            break;
                        case Constants.defaultPreyImage:
                            if (_cells[i, j].isMoved == false)
                            {
                                _cells[i, j].ProcessForPrey();
                            }

                            break;
                        default:
                            break;
                    }


                }
            }

            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; j < cols; j++)
            //    {
            //        if (_cells[i, j] == null)
            //        {
            //            _cells[i, j] = new Prey(this, i, j);
            //        }
            //    }
            //}


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (_cells[i, j] != null)
                    {
                        _cells[i, j].isMoved = false;
                    }
                }
            }


            timeToSpawnBonus--;

            if (timeToSpawnBonus <= 0 && !isFull())
            {
                SpawnBonus();
                timeToSpawnBonus = Constants.timeToSpawnBonus[pickedSize];
            }
            //else 
            //{
            //    timeToSpawnBonus--;
            //}

            
            numIterations--;

            output.Print(this);
            ClearChangedCellsArr();
        }


        public bool isFull()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (_cells[i, j] == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void SpawnBonus()
        {
            Coordinate coord = new Coordinate();
            for (int i = 0; i < 1; i++)
            {
                coord = Rand.GetRandomCoordinate(rows, cols);

                if (_cells[coord.X, coord.Y] == null)
                {
                    _cells[coord.X, coord.Y] = new Bonus(this, coord.X, coord.Y);
                }
                else i--;
            }
            AddChangedCellsCoord(new Coordinate(coord));
        }
    }


    






}
