namespace Homework_streams_bufferization_
{
    internal class Program
    {
    
          
            private static void Main(string[] args)
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("Необходмо ввести мимнимум 2 аргумента");
                    Environment.Exit(0);
                }

                string extension = args[0];
                string targetWorld = args[1];

                var util = new FileUtils();
                util.SetMainDirectory(@"C:\Users\shapovalov\Documents\123");
                util.SetFileExtension(extension);
                util.SetTargetWorld(targetWorld);

                util.FindAllFiles();
                Console.WriteLine();
                Console.WriteLine(string.Join("\n", util.FindResult));
                Console.ReadKey();
            }
        }
    }

