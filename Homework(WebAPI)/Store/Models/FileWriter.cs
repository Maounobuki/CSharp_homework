using NetStore.Abstraction;

namespace NetStore.Models
{
    public class FileWriter : IWriter
    {
        private readonly string _fileName;

        public FileWriter(string fileName) => _fileName = fileName;
        public void Write(string value)
        {
            File.AppendAllText(_fileName, value);
        }
    }
}
