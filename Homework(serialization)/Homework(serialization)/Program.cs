namespace Homework_serialization_
{
    internal class Program
    {
        static void Main(string[] args)
        {            
          StreamReader json = new StreamReader("E:\\C#\\CSharp\\Homework(serialization)\\Homework(serialization)\\sample4.json");
          string jsonString = json.ReadToEnd();
         

                
                string xmlData = IFormatConverter.ConvertJsonToXml(jsonString);

               
                Console.WriteLine(xmlData);
            }


        }
    }

