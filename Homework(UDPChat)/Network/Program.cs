using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Network
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() => Server());
        }

        

        public static async Task Server()
        {

            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Сервер ждет сообщение от клиента");
            var clt = new CancellationTokenSource();
            var token = clt.Token;

            while (true)
            {
                try
                {
                    if (token.IsCancellationRequested) { token.ThrowIfCancellationRequested(); }
                    await Task.Run(() =>
                    {
                        byte[] buffer = udpClient.Receive(ref iPEndPoint);
                        var messageText = Encoding.UTF8.GetString(buffer);
                        Message message = Message.DeserializeFromJson(messageText);
                        
                        if (!Console.KeyAvailable ) {
                            clt.Cancel(); 
                        }

                        if (message.Text.ToLower().Contains("exit"))
                        {
                            clt.Cancel();
                        }
                       


                        message.Print();

                        byte[] reply = Encoding.UTF8.GetBytes("Cообщение доставлено");
                        udpClient.Send(reply, reply.Length, iPEndPoint);
                    }, token);

                }
                catch (OperationCanceledException e)
                {   
                    Console.WriteLine("Запрошена процедура завершения работы сервера, нажмите любую клавишу для завершения работы");
                    Console.ReadKey();
                    break;
                }
                
            }
            clt.Dispose();
        }

    }
}

    
