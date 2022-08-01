using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    public interface IOutput
    {
        void ShowException(string message);

        void Print(Ocean _owner);

        //void PrintBorder(int columns);

        void Draw();

        void PredatorsWin();

        void PreysWin();

        void OutOfIterations();

    }
}
