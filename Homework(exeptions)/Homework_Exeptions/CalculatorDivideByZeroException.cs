using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Exeptions
{
   public class CalculatorDivideByZeroException : CalculatorExeption
    {
        public CalculatorDivideByZeroException()
        {
        
        }
        public CalculatorDivideByZeroException(string error) : base(error)
        {

        }
        
    }

}
