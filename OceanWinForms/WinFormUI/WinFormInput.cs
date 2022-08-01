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
    public class WinFormInput: IInput
    {
        private readonly SettingsForm settingsForm;

        public WinFormInput(SettingsForm settingsForm)
        {
            this.settingsForm = settingsForm;
        }

        public void EnterAll(Ocean ocean)
        {
            EnterOceanSize(ocean);
            EnterPreys(ocean);
            EnterPredators(ocean);
            EnterObstacles(ocean);
            EnterIterations(ocean);
            EnterCellBorderStyle(ocean);
        }

        public void EnterRows(Ocean ocean)
        {
            ocean.rows = Constants.defaultRows;
        }

        public void EnterCols(Ocean ocean)
        {
            ocean.cols = Constants.defaultCols;
        }

        public void EnterObstacles(Ocean ocean)
        {
            int obst = int.Parse(settingsForm.txtObstacle.Text);

            if (obst < 0 || obst > Constants.maxObstaclesArr[ocean.pickedSize])
            {
                throw new WrongAmountInput($"The number of obstacles must be between 0 and {Constants.maxObstaclesArr[ocean.pickedSize]}");
            }
            else
            {
                ocean.numObstacles = obst;
            }
        }

        public void EnterPreys(Ocean ocean)
        {
            int preys = int.Parse(settingsForm.txtPrey.Text);

            if (preys < 0 || preys > Constants.maxPreysArr[ocean.pickedSize])
            {
                throw new WrongAmountInput($"The number of preys must be between 0 and {Constants.maxPreysArr[ocean.pickedSize]}");
            }
            else
            {
                ocean.numPreys = preys;
            }
        }

        public void EnterPredators(Ocean ocean)
        {
            int predators = int.Parse(settingsForm.txtPredator.Text);

            if (predators < 0 || predators > Constants.maxPredatorsArr[ocean.pickedSize])
            {
                throw new WrongAmountInput($"The number of predators must be between 0 and {Constants.maxPredatorsArr[ocean.pickedSize]}");
            }
            else
            {
                ocean.numPredators = predators;
            }
        }

        public void EnterIterations(Ocean ocean)
        {
            int iterations = int.Parse(settingsForm.txtIteration.Text);

            if (iterations < 0 || iterations > Constants.maxIterations)
            {
                throw new WrongAmountInput($"The number of iterations must be between 0 and {Constants.maxIterations}");
            }
            else
            {
                ocean.numIterations = iterations;
            }
        }

        public void EnterOceanSize(Ocean ocean)
        {
            ocean.pickedSize = settingsForm.cmbboxOceanSize.SelectedIndex;
        }

        public void EnterCellBorderStyle(Ocean ocean)
        {
            ocean.pickedCellBorderStyleIndex = settingsForm.cmbbxCellBorder.SelectedIndex;
        }

    }
}
