using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    public interface IInput
    {
        void EnterAll(Ocean ocean);

        void EnterRows(Ocean ocean);

        void EnterCols(Ocean ocean);

        void EnterObstacles(Ocean ocean);

        void EnterPreys(Ocean ocean);

        void EnterPredators(Ocean ocean);

        void EnterIterations(Ocean ocean);

        
    }
}
