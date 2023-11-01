using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Delegates_Events_
{
    public interface ICalc
    {
        //Спроектируем интерфейс калькулятора, поддерживающего простые
        //арифметические действия, хранящего результат и также способного
        //выводить информацию о результате  при помощи события

        double Result { get; set; }
        void Sum(int x);
        void Sub(int x);
        void Multy(int x);
        void Divide(int x);
        void CancelLast();
        event EventHandler<EventArgs> MyEventHandler;

    }
}
