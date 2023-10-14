namespace Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int[,] a =
                    {
            { 7, 3, 2 },
            { 4, 9, 6 },
            { 1, 8, 2 }
        };
            int p = 0;
            int[] b = new int[a.GetLength(0) * a.GetLength(1)];

            for (int j = 0; j < a.GetLength(0); j++)
            {
                for (int k = 0; k < a.GetLength(1); k++)
                {
                    b[p] = a[j, k];
                    p++;
                }
            }
            p = 0;
            Array.Sort(b);

            p = 0;
            for (int j = 0; j < a.GetLength(0); j++)
            {
                for (int k = 0; k < a.GetLength(1); k++)
                {
                    a[j, k] = b[p];
                    p++;
                }
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write(a[i, j]);
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}