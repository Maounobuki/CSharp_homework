using System.Text.Json;
using System.Xml;

namespace Homework_serialization_
{
    internal interface IFormatConverter
    {
        static string ConvertJsonToXml(string jsonData)
        {
           
            JsonDocument jsonDoc = JsonDocument.Parse(jsonData);

            
            var xmlDoc = new XmlDocument();
            var rootElement = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(rootElement);

            
            ParseJson(xmlDoc, rootElement, jsonDoc.RootElement);

            
            return xmlDoc.OuterXml;
        }

        static void ParseJson(XmlDocument xmlDoc, XmlNode parentNode, JsonElement jsonElement)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (var property in jsonElement.EnumerateObject())
                    {
                        var subElement = xmlDoc.CreateElement(property.Name);
                        parentNode.AppendChild(subElement);
                        ParseJson(xmlDoc, subElement, property.Value);
                    }
                    break;
                case JsonValueKind.Array:
                    int index = 1;
                    foreach (var item in jsonElement.EnumerateArray())
                    {
                        var subElement = xmlDoc.CreateElement("item");
                        parentNode.AppendChild(subElement);
                        ParseJson(xmlDoc, subElement, item);
                        index++;
                    }
                    break;
                default:
                    parentNode.InnerText = jsonElement.ToString();
                    break;
            }
        }
    }
}
