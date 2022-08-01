using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    interface IOcean
    {
        public Cell[,] _cells { get; set; }
        public int numObstacles { get; set; }
        public int numPreys { get; set; }
        public int numPredators { get; set; }
        public int numIterations { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }
        public int pickedSize { get; set; }

        public void InitCells();

        public void AddEmptyCells(int rows, int cols);

        public void AddPreys();

        public void AddPredators();

        public void AddObstacles();

        public void Process();

    }
}
