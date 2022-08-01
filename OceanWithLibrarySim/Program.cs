using System;
using OceanSimulation;

namespace OceanWithLibrarySim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IInput input = new ConsoleInput();
            IOutput output = new ConsoleOutput();
            Ocean ocean = new Ocean();
            ocean.SetOutput(output);
            ocean.SetInput(input); 

            ocean.InitCells();
            ocean.Process();
        }
    }
}
