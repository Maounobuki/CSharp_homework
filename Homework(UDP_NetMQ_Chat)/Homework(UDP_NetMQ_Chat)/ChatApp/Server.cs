using ChatNetwork;
using ChatCommon.Models;
using ChatDB;
using NetMQ;
using NetMQ.Sockets;



namespace ChatApp

{
    public class Server 
    {
        private readonly string _socketListen;
        private readonly string _socketSender;
        private readonly NetMQMessageSourceServer _messageSource;
        private List<string> _clients;
        private Queue<NetMessage> _messages;

        public Server(string socketListen = "tcp://127.0.0.1:5556", string socketSender = "tcp://127.0.0.1:5555")
        {
            _socketListen = socketListen;
            _socketSender = socketSender;

            _clients = new List<string>();
            _messages = new Queue<NetMessage>();
            _messageSource = new NetMQMessageSourceServer();
        }

        bool work = true;
        public void Stop()
        {
            work = false;
        }

        private async Task Register(NetMessage message)
        {
            Console.WriteLine($" Message Register name = {message.NickNameFrom}");

            if (!_clients.Contains(message.NickNameFrom))
            {
                _clients.Add(message.NickNameFrom);
                using (ChatContext context = new ChatContext())
                {
                    context.Users.Add(new User() { FullName = message.NickNameFrom });
                    await context.SaveChangesAsync();
                }

            }

        }
        private async Task RelyMessage(NetMessage message)
        {
            if (_clients.Contains(message.NickNameTo) || message.NickNameTo.Equals("All"))
            {
                int? id = 0;
                using (var ctx = new ChatContext())
                {
                    var fromUser = ctx.Users.First(x => x.FullName == message.NickNameFrom);
                    var toUser = ctx.Users.First(x => x.FullName == message.NickNameTo);
                    var msg = new Message { UserFrom = fromUser, UserTo = toUser, IsSent = false, Text = message.Text };
                    ctx.Messages.Add(msg);

                    ctx.SaveChanges();

                    id = msg.MessageId;
                }
                message.Id = id;

                _messages.Enqueue(message);

                Console.WriteLine($"Message Relied, from = {message.NickNameFrom} to = {message.NickNameTo}");
            }
            else
            {
                Console.WriteLine("Пользователь не найден.");
            }

        }

        async Task ConfirmMessageReceived(int? id)
        {
            Console.WriteLine("Message confirmation id=" + id);

            using (var ctx = new ChatContext())
            {
                var msg = ctx.Messages.FirstOrDefault(x => x.MessageId == id);

                if (msg != null)
                {
                    msg.IsSent = true;
                    await ctx.SaveChangesAsync();
                }
            }
        }

        private async Task ProcessMessage(NetMessage message)
        {
            switch (message.Command)
            {
                case Command.Register: await Register(message); break;
                case Command.Message: await RelyMessage(message); break;
                case Command.Confirmation: await ConfirmMessageReceived(message.Id); break;
            }
        }

        private async Task Publisher()
        {
            using (var server = new PublisherSocket(_socketSender))
            {
                while (work)
                {
                    if (_messages.Count > 0)
                    {
                        var msg = _messages.Dequeue();
                        server.SendMoreFrame(msg.NickNameTo).SendFrame(msg.SerialazeMessageToJSON());
                    }
                }

            }
        }

        private async Task Lisener()
        {
            Console.WriteLine("Сервер ожидает сообщения ");
            using (var server = new RouterSocket(_socketListen))
            {
                while (work)
                {
                    try
                    {
                        var message = _messageSource.Receive(server);
                        Console.WriteLine(message.ToString());
                        await ProcessMessage(message);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }
            }
        }

        public async Task Start()
        {
            new Thread(async () => await Lisener()).Start();

            await Publisher();
        }
    }
}
