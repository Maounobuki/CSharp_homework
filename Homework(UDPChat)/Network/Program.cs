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

       



        public static void Server(string name)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Сервер ждет сообщение от клиента. Для завершения нажмите любую клавишу.");


            while (!Console.KeyAvailable)
            
            {
                    try
                    {
                        ThreadPool.QueueUserWorkItem(obj =>
                        {
                            byte[] buffer = udpClient.Receive(ref iPEndPoint);
                            var messageText = Encoding.UTF8.GetString(buffer);
                            Message message = Message.DeserializeFromJson(messageText);
                            message?.Print();
                            if (messageText.ToLower().Contains("exit"))
                            {
                                throw new ExceptionExit();
                            }

                           
                            byte[] reply = Encoding.UTF8.GetBytes("Cообщение доставлено");
                            udpClient.Send(reply, reply.Length, iPEndPoint);
                        });
                    }
                    catch (ExceptionExit e)
                    {
                        break;
                    }
                }
            }
            }
        }

    
