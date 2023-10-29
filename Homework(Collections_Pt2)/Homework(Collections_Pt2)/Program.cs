using System.Diagnostics;

namespace Homework_Collections_Pt2_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };
            Console.Write("Введите целевое число:");
            int target = int.Parse(Console.ReadLine());

            DefineSummand(numbers, target);


            static bool DefineSummand(int[] numbers, int target)
            {
                for (int i = 1; i < numbers.Length - 1; i++)
                {
                    int left = i - 1;
                    int right = i + 1;
                    int result;

                    result = numbers[left] + numbers[i] + numbers[right];
                    if (result == target)
                    {
                        Console.WriteLine($"Искомое число {target} состоит из {numbers[left]} + {numbers[i]} + {numbers[right]}");
                        return true;
                    }
                    else
                    {
                        left++;
                        right++;
                    }
                }
                Console.WriteLine("Подходящих чисел не найдено");
                return false;
            }
        }
    }
    }
