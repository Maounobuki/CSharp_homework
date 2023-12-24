using System.Net.Sockets;
using System.Net;
using ChatNetwork;
using ChatCommon.Models;
using NetMQ.Sockets;
using System.ServiceModel.Channels;

namespace ChatApp

{
    public class Client
    {
        private readonly string _name;
        private readonly string _socketListen;
        private readonly string _socketSender;
        private Queue<NetMessage> _messages;
        public NetMQMessageSourceClient _messageSource;

        public Client(string name, string socketListen = "tcp://127.0.0.1:5555", string socketSender = "tcp://127.0.0.1:5556")
        {
            this._name = name;
            this._socketListen = socketListen;
            this._socketSender = socketSender;
            _messageSource = new NetMQMessageSourceClient();
            _messages = new Queue<NetMessage>();
        }

        private async Task ClientListener()
        {
            using (var socketListen = new SubscriberSocket(_socketListen))
            {
                socketListen.Subscribe("All");
                socketListen.Subscribe(_name);

                while (true)
                {
                    try
                    {
                        var messageReceived = _messageSource.Receive(socketListen);

                        messageReceived.PrintGetMessageFrom();
                        await Confirm(messageReceived);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка при получении сообщения: " + ex.Message);
                    }
                }
            }
        }

        private async Task Confirm(NetMessage message)
        {
            message.Command = Command.Confirmation;
            _messages.Enqueue(message);
        }


        private void Register(DealerSocket socketSender)
        {
            var message = new NetMessage() { NickNameFrom = _name, NickNameTo = null, Text = null, Command = Command.Register};
            _messageSource.Send(message, socketSender);
            Console.WriteLine("Мы тута");
        }

        private async Task ClientSender()
        {

            using (var socketSender = new DealerSocket(_socketSender))
            {
                Register(socketSender);
                Task client = Task.Run(async() => {
                    while (true)
                    {
                        try
                        {
                            var message = await CreatingMessage();
                            _messageSource.Send(message, socketSender);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при обработке сообщения: " + ex.Message);
                        }
                    } 
                });
                Task.Run( () =>
                {
                    while (true)
                    {
                        if (_messages.Count > 0)
                        {
                            var message = _messages.Dequeue();
                            _messageSource.Send(message, socketSender);
                        }
                    }
                });
                Task.WaitAll(client);
                
            }
        }

        private async Task<NetMessage> CreatingMessage()
        {
            Console.Write("Введите  имя получателя: ");
            var nameTo = Console.ReadLine();

            if (string.IsNullOrEmpty(nameTo)) nameTo = "All";

            Console.Write("Введите сообщение и нажмите Enter: ");
            var messageText = Console.ReadLine();

            return new NetMessage() { Command = Command.Message, NickNameFrom = _name, NickNameTo = nameTo, Text = messageText };
        }

        public async Task Start()
        {
            new Thread(async () => await ClientListener()).Start();

            await ClientSender();

        }
    }

}
