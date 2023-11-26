using System.Reflection;

namespace Homework_streams_bufferization_
{
    internal class FileUtils
    {
        private string MainDirectory { get; set; } = @"C:/";
        private string FileExtension { get; set; } = "";
        private string TargetWorld { get; set; } = "";
        public List<string> FindResult { get; set; } = new List<string>();

        public void SetMainDirectory(string path)
            => MainDirectory = path;
        public void SetFileExtension(string extension)
            => FileExtension = extension;
        public void SetTargetWorld(string world)
            => TargetWorld = world;

        public async void FindAllFiles(string dirrectory = "")
        {
            if (!CheckIncomingData()) return;
            var dirs = new List<string>();
            var files = new List<string>();
            try
            {
                dirs.AddRange(Directory.EnumerateDirectories(string.IsNullOrWhiteSpace(dirrectory) ? MainDirectory : dirrectory));
                files.AddRange(Directory.EnumerateFiles(MainDirectory).Where(x => x.Contains(FileExtension)));

            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Ошибка доступа к файлу или директории!" + ex.Message);
            }
            foreach (var file in files)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    var count = 0;
                    string? line;

                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (line.Contains(TargetWorld))
                        {
                            Console.WriteLine($"Файл {file}, " +
                                $"Содержит слово : '{TargetWorld}', в строке {count}");
                            FindResult.Add($"Файл: {file}, строка {count}");
                        }
                        count++;
                    }
                }
            }

            foreach (var dir in dirs)
            {
                FindAllFiles(dir);
            }

        }

        private bool CheckIncomingData()
        {
            bool isValid = true;
            var propeties = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var property in propeties.Where(x => x.Name != "<FindResult>k__BackingField"))
            {
                var value = property.GetValue(this) as String;
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine($"Входные данные не прошли проверку. Свойство {property.Name} не заполнено!");
                    return false;
                }
            }

            return isValid;
        }

    }
}
