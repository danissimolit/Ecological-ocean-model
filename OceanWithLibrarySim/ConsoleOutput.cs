using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OceanSimulation;

namespace OceanWithLibrarySim
{
    public class ConsoleOutput : IOutput
    {
        

        public void Print(Ocean _owner)
        {
            Console.SetCursorPosition(0, 0);

            PrintStats(_owner.numPreys, _owner.numPredators, _owner.numIterations);

            Console.WriteLine();

            PrintBorder(_owner.cols);

            for (int i = 0; i < _owner.rows; i++)
            {
                for (int j = 0; j < _owner.cols; j++)
                {
                    if (_owner[i, j] == null)
                    {
                        Console.Write(Constants.defaultImage);
                    }
                    else Console.Write(_owner[i, j].GetImage());
                }
                Console.WriteLine();
            }

            PrintBorder(_owner.cols);

            Thread.Sleep(600);
        }

        public void PrintStats(int numPreys, int numPredators, int numIterations)
        {
            Console.WriteLine("Stats:");
            Console.WriteLine($"The number of preys: {numPreys}   ");
            Console.WriteLine($"The number of predators: {numPredators}   ");
            Console.WriteLine($"The number of iterations: {numIterations}   ");

        }

        public void ShowException(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintBorder(int columns)
        {
            for (int i = 0; i < columns; i++)
            {
                Console.Write("*");
            }

            Console.WriteLine();
        }

        public void Draw()
        {
            Console.WriteLine("\n-----------------");
            Console.WriteLine("******Draw!******");
            Console.WriteLine("-----------------");
        }

        public void PredatorsWin()
        {
            Console.WriteLine("\n--------------------------");
            Console.WriteLine("******Predators win!******");
            Console.WriteLine("--------------------------");
        }

        public void PreysWin()
        {
            Console.WriteLine("\n----------------------");
            Console.WriteLine("******Preys win!******");
            Console.WriteLine("----------------------");
        }

        public void OutOfIterations()
        {
            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("******The iterations are over!******");
            Console.WriteLine("------------------------------------");
        }

    }
}
