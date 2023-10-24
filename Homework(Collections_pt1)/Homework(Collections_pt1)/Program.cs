namespace Homework5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
                    Доработайте приложение поиска пути в лабиринте, 
                    но на этот раз вам нужно определить сколько всего 
                    выходов имеется в лабиринте:
                   */

            int[,] labirynth1 = new int[,]
            {
        {1, 1, 1, 1, 1, 1, 1 },
        {1, 0, 0, 0, 0, 0, 1 },
        {1, 0, 1, 1, 1, 0, 1 },
        {0, 0, 0, 0, 1, 0, 2 },
        {1, 1, 0, 0, 1, 1, 1 },
        {1, 1, 1, 0, 1, 1, 1 },
        {1, 1, 1, 2, 1, 1, 1 }
            };

            Console.WriteLine(HasExit(2, 1, labirynth1));
        }

        static int HasExit(int startI, int startJ, int[,] l)
        {
            int exist = 0;

            if (l[startI, startJ] == 1)
            {
                Console.WriteLine("Начальная точка находится в стене!");
                return exist;
            }

            var stack = new Stack<Tuple<int, int>>();
            stack.Push(new(startI, startJ));

            while (stack.Count > 0)
            {
                var temp = stack.Pop();

                if (l[temp.Item1, temp.Item2] == 2)
                {
                    exist++;
                }

                l[temp.Item1, temp.Item2] = 1;

                if (temp.Item2 > 0 && l[temp.Item1, temp.Item2 - 1] != 1)
                    stack.Push(new(temp.Item1, temp.Item2 - 1)); // вверх

                if (temp.Item2 + 1 < l.GetLength(1) && l[temp.Item1, temp.Item2 + 1] != 1)
                    stack.Push(new(temp.Item1, temp.Item2 + 1)); // низ

                if (temp.Item1 > 0 && l[temp.Item1 - 1, temp.Item2] != 1)
                    stack.Push(new(temp.Item1 - 1, temp.Item2)); // лево

                if (temp.Item1 + 1 < l.GetLength(0) && l[temp.Item1 + 1, temp.Item2] != 1)
                    stack.Push(new(temp.Item1 + 1, temp.Item2)); // право
            }
            Console.WriteLine($"Найдено выходов: {exist}");
            return exist;
        }
    }
}