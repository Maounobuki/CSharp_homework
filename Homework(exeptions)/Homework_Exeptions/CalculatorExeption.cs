using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Exeptions
{
   public class CalculatorExeption : Exception
    {
        public CalculatorExeption()
        {
        
        }
        public CalculatorExeption(string error) : base(error)
        {

        }
        
    }

}
