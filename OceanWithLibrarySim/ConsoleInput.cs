using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OceanSimulation;

namespace OceanWithLibrarySim
{
    class ConsoleInput : IInput
    {
        public void EnterRows(Ocean ocean)
        {
            Console.Write($"Enter the number of rows (default {Constants.defaultRows}): ");
            ocean.rows = int.Parse(Console.ReadLine());
            if (ocean.rows > Constants.maxRows)
            {
                ocean.rows = Constants.maxRows;
            }
            else if (ocean.rows <= 0)
            {
                ocean.rows = Constants.defaultRows;
            }
        }

        public void EnterCols(Ocean ocean)
        {
            Console.Write($"Enter the number of columns (default {Constants.defaultCols}): ");
            ocean.cols = int.Parse(Console.ReadLine());
            if (ocean.cols > Constants.maxCols)
            {
                ocean.cols = Constants.maxCols;
            }
            else if (ocean.cols <= 0)
            {
                ocean.cols = Constants.defaultCols;
            }
        }

        public void EnterObstacles(Ocean ocean)
        {
            Console.Write($"Enter the number of obstacles (default {Constants.defaultNumObstacles}): ");
            ocean.numObstacles = int.Parse(Console.ReadLine());
            int oceanSize = ocean.rows * ocean.cols;


            if (ocean.numObstacles < 0 || ocean.numObstacles > oceanSize / 4)
            {
                if (Constants.defaultNumObstacles > oceanSize / 4)
                {
                    ocean.numObstacles = oceanSize / 4;
                }
                else
                {
                    ocean.numObstacles = Constants.defaultNumObstacles;
                }
            }

        }

        public void EnterPreys(Ocean ocean)
        {
            Console.Write($"Enter the number of preys (default {Constants.defaultNumPreys}): ");
            ocean.numPreys = int.Parse(Console.ReadLine());
            int oceanSize = ocean.rows * ocean.cols;

            if (ocean.numPreys <= 0 || ocean.numPreys > oceanSize)
            {
                if (Constants.defaultNumPreys > oceanSize)
                {
                    ocean.numPreys = oceanSize / 4;
                }
                else
                {
                    ocean.numPreys = Constants.defaultNumPreys;
                }
            }
        }

        public void EnterPredators(Ocean ocean)
        {
            Console.Write($"Enter the number of predators (default {Constants.defaultNumPredators}): ");
            ocean.numPredators = int.Parse(Console.ReadLine());
            int oceanSize = ocean.rows * ocean.cols;

            if (ocean.numPredators <= 0 || ocean.numPredators > oceanSize)
            {
                if (Constants.defaultNumPredators > oceanSize)
                {
                    ocean.numPredators = oceanSize / 4;
                }
                else
                {
                    ocean.numPredators = Constants.defaultNumPredators;
                }
            }
        }

        public void EnterIterations(Ocean ocean)
        {
            Console.Write($"Enter the number of iterations (default {Constants.defaultNumIterations}): ");
            ocean.numIterations = int.Parse(Console.ReadLine());

            if (ocean.numIterations > Constants.maxIterations)
            {
                ocean.numIterations = Constants.maxIterations;
            }
            else if (ocean.numIterations <= 0)
            {
                ocean.numIterations = Constants.defaultNumIterations;
            }

            Console.Clear();
        }



        public void EnterAll(Ocean ocean)
        {
            EnterRows(ocean);
            EnterCols(ocean);
            EnterPreys(ocean);
            EnterPredators(ocean);
            EnterObstacles(ocean);
            EnterIterations(ocean);
        }

        
    }
}
