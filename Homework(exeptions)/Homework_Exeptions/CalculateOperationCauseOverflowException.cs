using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Exeptions
{
   public class CalculateOperationCauseOverflowException : CalculatorExeption
    {
        public CalculateOperationCauseOverflowException()
        {
        
        }
        public CalculateOperationCauseOverflowException(string error) : base(error)
        {

        }
        
    }

}
