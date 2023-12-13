using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class ClientWorker
    {
        private UdpClient udpClient;
        private IPEndPoint iPEndPoint;
        private string Name;

        public ClientWorker(IPEndPoint iEP,UdpClient udp)
        {
            this.udpClient = udp;
            this.iPEndPoint = iEP;
            this.Name = Registration();
        }

        public async Task ReceiveMessageAsync(CancellationToken token)
        {
            while (true)
            {
                var result = await udpClient.ReceiveAsync();
                var message = Encoding.UTF8.GetString(result.Buffer);
                if (token.IsCancellationRequested) { 

                    token.ThrowIfCancellationRequested();
                    break; }

                Print(message);
            }
        }

        void Print(string message)
        {
            Message mes = Message.DeserializeFromJson(message);

            if (OperatingSystem.IsWindows())
            {
                var position = Console.GetCursorPosition();
                int left = position.Left;
                int top = position.Top;
                Console.MoveBufferArea(0, top, left, 1, 0, top + 1);
                Console.SetCursorPosition(0, top);

                Console.WriteLine(mes);
                Console.SetCursorPosition(left, top + 1);
            }
            else Console.WriteLine(mes);
        }

        public async Task SentMessageAsync()
        {
            while (true)
            {
                Message message = СreatureMessage();
                if (message == null) break;

                string json = message.SerializeMessageToJson();

                byte[] data = Encoding.UTF8.GetBytes(json);
                await udpClient.SendAsync(data, data.Length, iPEndPoint);
            }

        }

        private Message СreatureMessage()
        {
            string messageText;
            do
            {
                Console.WriteLine("Введите сообщение, или exit для выхода");
                messageText = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(messageText));

            if (messageText.Equals("exit")) return null;

            Console.WriteLine("Кому вы хотите отправить сообщение?");
            Console.WriteLine("Введдите имя получателя, либо оставте пустой ввод для отправки сообщения всем");
            string nicknameTo = Console.ReadLine();

            Message message = new Message() { Text = messageText, NicknameFrom = Name, NicknameTo = nicknameTo, DateTime = DateTime.Now, Command = null};

            return message;
        }

        private string Registration()
        {
            string name;
            while (true)
            {
                Console.WriteLine("Введите имя");
                name = Console.ReadLine();

                if (!string.IsNullOrEmpty(name))
                {

                    Message message = new Message() { Text = "", NicknameFrom = name, NicknameTo = "Server", DateTime = DateTime.Now, Command = Commands.Register };
                    string json = message.SerializeMessageToJson();

                    byte[] data = Encoding.UTF8.GetBytes(json);
                    udpClient.Send(data, data.Length, iPEndPoint);

                    byte[] buffer = udpClient.Receive(ref iPEndPoint);
                    var answer = Encoding.UTF8.GetString(buffer);

                    if (answer.Equals("Пользователь зарегистрирован"))
                    {
                        Console.WriteLine(answer);
                        break;
                    }
                    else
                    {
                        Console.WriteLine(answer);
                    }
                }

            }

            return name;
        }

        private void Exit()
        {
            Message message = new Message() { Text = "", NicknameFrom = Name, NicknameTo = "Server", DateTime = DateTime.Now, Command = Commands.Delete };
            string json = message.SerializeMessageToJson();

            byte[] data = Encoding.UTF8.GetBytes(json);
            udpClient.Send(data, data.Length, iPEndPoint);
        }

        public async Task StartAsync()
        {
            var clt = new CancellationTokenSource();
            var token = clt.Token;

            try
            {
                Task.Run(() => ReceiveMessageAsync(token),token);
            }
            catch(OperationCanceledException e)
                {
                Console.WriteLine("Досвидания");
                
            }

            await SentMessageAsync();
            clt.Cancel();
            Exit();

        }
    }
}
