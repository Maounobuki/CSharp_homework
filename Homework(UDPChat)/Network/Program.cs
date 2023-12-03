using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Network
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server("Hello");
        }

        public void task1()
        {
            Message msg = new Message() { Text = "Hello", DateTime = DateTime.Now, NicknameFrom = "Artem", NicknameTo = "All" };
            string json = msg.SerializeMessageToJson();
            Console.WriteLine(json);
            Message? msgDeserialized = Message.DeserializeFromJson(json);
        }



        public static void Server(string name)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (true)
            {
                try
                {
                    ThreadPool.QueueUserWorkItem(obj => {
                        byte[] buffer = udpClient.Receive(ref iPEndPoint);
                        var messageText = Encoding.UTF8.GetString(buffer);
                        if (messageText == "Exit" || messageText == "exit")
                        {
                            throw new ExceptionExit();
                        }
                
                        Message message = Message.DeserializeFromJson(messageText);
                        message.Print();

                        byte[] reply = Encoding.UTF8.GetBytes("Cообщение доставлено");
                        udpClient.Send(reply, reply.Length, iPEndPoint);
                    });
                }catch (ExceptionExit e)
                {
                    break;
                }
              
            }
        }

    }
}