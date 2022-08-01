using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using OceanSimulation;

namespace OceanWinForms
{
    public class WinFormOutput : IOutput
    {
        private readonly OceanForm oceanForm;
        private readonly Bitmap preyImg = Properties.Resources.PreyImg;
        private readonly Bitmap predatorImg = Properties.Resources.PredatorImg;
        private readonly Bitmap obstacleImg = Properties.Resources.ObstacleImg;
        private readonly Bitmap bonusImg = Properties.Resources.BonusImg;
        private readonly Bitmap preyImgWithBonus = Properties.Resources.PreyImgWithBonus;
        private readonly Bitmap predatorImgWithBonus = Properties.Resources.PredatorImgWithBonus;


        public WinFormOutput(OceanForm oceanForm)
        {
            this.oceanForm = oceanForm;
        }

        public WinFormOutput()
        {

        }

        public void ShowException(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Print(Ocean _owner)
        {
            PrintStats(_owner.numPreys, _owner.numPredators, _owner.numIterations);

            for(int i = 0; i < _owner.changedCellsIndex; i++)
            {
                PrintCell(_owner, _owner.changedCellsCoord[i].X, _owner.changedCellsCoord[i].Y);
            }
        }

        public void FirstPrint(Ocean _owner)
        {
            PrintStats(_owner.numPreys, _owner.numPredators, _owner.numIterations);

            for (int i = 0; i < _owner.rows; i++)
            {
                for (int j = 0; j < _owner.cols; j++)
                {
                    PrintCell(_owner, i, j);
                }
            }
        }

        public void PrintCell(Ocean _owner, int i, int j)
        {
            if (_owner[i, j] == null)
            {
                oceanForm.buttons[i, j].BackgroundImage = null;
            }
            else if (_owner[i, j].GetType() == typeof(Prey))
            {
                if (_owner[i, j].isBonusOn())
                {
                    oceanForm.buttons[i, j].BackgroundImage = preyImgWithBonus;
                }
                else oceanForm.buttons[i, j].BackgroundImage = preyImg;
            }
            else if (_owner[i, j].GetType() == typeof(Predator))
            {
                if (_owner[i, j].isBonusOn())
                {
                    oceanForm.buttons[i, j].BackgroundImage = predatorImgWithBonus;
                }
                else oceanForm.buttons[i, j].BackgroundImage = predatorImg;
            }
            else if (_owner[i, j].GetType() == typeof(Obstacle))
            {
                oceanForm.buttons[i, j].BackgroundImage = obstacleImg;
            }
            else if (_owner[i, j].GetType() == typeof(Bonus))
            {
                oceanForm.buttons[i, j].BackgroundImage = bonusImg;
            }
        }

        public void PrintStats(int numPreys, int numPredators, int numIterations)
        {
            oceanForm.lblStatsPrey.Text = $"Preys: {numPreys}";              
            oceanForm.lblStatsPredator.Text = $"Predators: {numPredators}";
            oceanForm.lblStatsIteration.Text = $"Iterations: {numIterations}";
        }

        public void EndOfSimulation(int numPreys, int numPredators, int numIterations)
        {
            if (numPredators == 0 && numPreys == 0)
            {
                Draw();
            }
            else if (numPredators == 0)
            {
                PreysWin();
            }
            else if (numPreys == 0)
            {
                PredatorsWin();
            }
            else if (numIterations <= 0)
            {
                OutOfIterations();
            }
        }

        public void Draw()
        {
            MessageBox.Show("All preys and predators are dead", "Draw!",
                MessageBoxButtons.OK);
        }

        public void PredatorsWin()
        {
            MessageBox.Show("All preys are dead", "Predators win!",
                MessageBoxButtons.OK);
        }

        public void PreysWin()
        {
            MessageBox.Show("All predators are dead", "Preys win!",
                MessageBoxButtons.OK);
        }

        public void OutOfIterations()
        {
            MessageBox.Show("The iterations are over!", "Simulation is over",
                MessageBoxButtons.OK);
        }
    }
}
