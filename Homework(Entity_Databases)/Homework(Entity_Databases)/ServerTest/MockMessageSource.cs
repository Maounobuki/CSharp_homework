using System.Net;
using Homework_Entity_Databases_.Abstracts;
using Homework_Entity_Databases_.Models;
using Homework_Entity_Databases_.Services;

namespace ServerTest
{
    internal class MockMessageSource : IMessageSource
    {
        private Queue<NetMessage> _messages = new ();
        private Server _server;
        private IPEndPoint _endPoint = new IPEndPoint(IPAddress.Any, 0);

        public MockMessageSource()
        {
            _messages.Enqueue(new NetMessage { Command = Command.Register, NickNameFrom = "Коля" });
            _messages.Enqueue(new NetMessage { Command = Command.Register, NickNameFrom = "Федя" });
            _messages.Enqueue(new NetMessage { Command = Command.Message, NickNameFrom = "Федя", NickNameTo = "Коля", Text = "100 рублей на кофе" });
            _messages.Enqueue(new NetMessage { Command = Command.Message, NickNameFrom = "Коля", NickNameTo = "Федя", Text = "Благодарю!" });
        }
        public void AddServer(Server srv)
        {
            _server = srv;
        }
        public NetMessage Receive(ref IPEndPoint ep)
        {
            ep = _endPoint;

            if (_messages.Count == 0)
            {
                _server.Stop();
                return null;
            }

            var msg = _messages.Dequeue();

            return msg;
        }

        public async Task SendAsync(NetMessage message, IPEndPoint ep)
        {
        
        }
    }
}
