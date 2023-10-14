namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                int result = 0;
                int number1 = int.Parse(args[0]);
                string operation = args[1];
                int number2 = int.Parse(args[2]);
                if (operation == "+")
                {
                    result = number1 + number2;
                }
                if (operation == "-")
                {
                    result = number1 - number2;
                }
                if (operation == "/")
                {
                    result = number1 / number2;
                }
                if (operation == "*")
                {
                    result = number1 * number2;
                }
                Console.WriteLine("Результат выполнения операции cd(" + operation + ") = " + result);
}
            else
                Console.WriteLine("Для использования программы введите: число знак(+-/*) число");
}

        }
    }
