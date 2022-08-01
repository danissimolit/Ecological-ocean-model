using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulation
{
    public class WrongAmountInput : Exception
    {
        public WrongAmountInput() : base()
        {

        }

        public WrongAmountInput(string message) : base(message)
        {

        }

        public WrongAmountInput(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
