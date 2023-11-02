using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Homework_Delegates_Events_
{
    internal interface IStarter
    {
        static void Start()
        {
            var calc = new Calc();
            var action = "";
            while (true)
            {
                            
                Console.WriteLine($"Текущий результат: {calc.Result} {action}");
                if (string.IsNullOrEmpty(action))
                {
                    Console.WriteLine($"Выберите действие(для просмотра доступных сценариев введите - ?) или введите оператор:");
                }
                else
                {
                    Console.WriteLine("Введите число");
                }
                var str = Console.ReadLine();
                switch (str)
                {
                    case "?":
                        Console.WriteLine("Для получения суммы суммы введите '+'");
                        Console.WriteLine("Для получения разности суммы введите '-'");
                        Console.WriteLine("Для умножения введите '-'");
                        Console.WriteLine("Для деления введите '/'");
                        Console.WriteLine(($"Для изменения текушего значения {calc.Result} введите '!'"));
                        Console.WriteLine("Для выхода из приложения введите 'пустое' значение либо 'quit'"); ; break;

                    case "!":
                        Console.WriteLine($" Введите число:");
                        var num = Console.ReadLine();
                        if (string.IsNullOrEmpty(num))
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            calc.Result = Convert.ToDouble(num);
                        }
                        break;
                    case "+": action = "+"; break;
                    case "-": action = "-"; break;
                    case "*": action = "*"; break;
                    case "/": action = "/"; break;
                    case "" : Environment.Exit(0); break;
                    case "quit": Environment.Exit(0); break;

                    case "DevideByZero":
                        Console.WriteLine("На ноль делить нельзя");
                        break;
                    default:                       
                        if (string.IsNullOrEmpty(action))
                        {
                            Console.WriteLine("Необходим выбор действия!");                            
                            break;
                        }
                        
                        if (int.TryParse(str, out int number))
                        {       
                            calc.Result = action.Equals("+") ? calc.Result + number :
                            calc.Result = action.Equals("-") ? calc.Result - number :
                            calc.Result = action.Equals("*") ? calc.Result * number :
                            calc.Result = action.Equals("/") && number != 0 ? calc.Result / number :
                            calc.Result;

                            action = string.Empty;
                            Console.Clear();
                            if (number == 0) goto case "DevideByZero";
                        }
                       
                        
                        else
                        {
                            Console.WriteLine("Не удалось преобразовать в число");
                            break;
                        }
                        break;
                }
            }
        }

    }
}
