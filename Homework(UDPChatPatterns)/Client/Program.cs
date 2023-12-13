using Network;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static UdpClient udpClient = new UdpClient();
        static IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

        static async Task Main(string[] args)
        {

            ClientWorker client = new ClientWorker(iPEndPoint, udpClient);
            client.StartAsync();

        }



        
        public static void m1()
        {
            Message message = new Message() { Text = "ПРивет", NicknameFrom = "Artem", NicknameTo = "Server", DateTime = DateTime.Now, Command = Commands.Register };
            string json = message.SerializeMessageToJson();

            byte[] data = Encoding.UTF8.GetBytes(json);
            udpClient.Send(data, data.Length, iPEndPoint);
        }
        public static void m2()
        {
            Message message = new Message() { Text = "ПРивет", NicknameFrom = "Vasa", NicknameTo = "Server", DateTime = DateTime.Now, Command = Commands.Register };
            string json = message.SerializeMessageToJson();

            byte[] data = Encoding.UTF8.GetBytes(json);
            udpClient.Send(data, data.Length, iPEndPoint);
        }
        public static void m3()
        {
            Message message = new Message() { Text = "ПРивет", NicknameFrom = "Artem", NicknameTo = "Vasa", DateTime = DateTime.Now };
            string json = message.SerializeMessageToJson();

            byte[] data = Encoding.UTF8.GetBytes(json);
            udpClient.Send(data, data.Length, iPEndPoint);
        }
        public static void m4()
        {
            Message message = new Message() { Text = "ПРивет", NicknameFrom = "Artem", NicknameTo = "Server", DateTime = DateTime.Now, Command = Commands.Delete };
            string json = message.SerializeMessageToJson();

            byte[] data = Encoding.UTF8.GetBytes(json);
            udpClient.Send(data, data.Length, iPEndPoint);
        }

    }
}