using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network
{

    public enum TypeSend
    {
        ToAll,
        ToOne,
        defaultmes
    }
    
    public delegate TypeSend MessageHandler(Message mes, IPEndPoint ep);

    public class Server
    {
        private readonly UdpClient _udpClient;
        private IPEndPoint _iPEndPoint;
        MessageHandler? MessageHandler;
        public string Name { get => "Server-1"; }
        public Dictionary<string, IPEndPoint>? Users { get; set; }

        public Server()
        {
            _udpClient = new UdpClient(12345);
            _iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }

        public void RegMesHandler(MessageHandler del) => MessageHandler = del;


        public Message Listen()
        {
            byte[] buffer = _udpClient.Receive(ref _iPEndPoint);
            var messageText = Encoding.UTF8.GetString(buffer);
            Message message = Message.DeserializeFromJson(messageText);
            
            return message;
        }

        public void Send(TypeSend type,Message message)
        {
            byte[] reply = Encoding.UTF8.GetBytes(message.SerializeMessageToJson());

            switch (type)
            {
                case TypeSend.ToAll:
                    foreach (var ip in Users.Values)
                        _udpClient.Send(reply, reply.Length, ip);
                    break;
                case TypeSend.ToOne:
                    if(Users.TryGetValue(message.NicknameTo,out IPEndPoint ep))
                        _udpClient.Send(reply, reply.Length, ep);
                    break;
            }
        }

        public void Answer(string msg, IPEndPoint iEP)
        {
            byte[] reply = Encoding.UTF8.GetBytes(msg);
            _udpClient.Send(reply, reply.Length, iEP);
        }

        public void Start()
        {
            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (true)
            {
                var mes = Listen();
                var typesend = MessageHandler?.Invoke(mes, _iPEndPoint);

                if (typesend != null && typesend != TypeSend.defaultmes)
                {
                    ThreadPool.QueueUserWorkItem(obj =>
                    {
                        Send((TypeSend)typesend, mes);

                    });
                }
                Console.Clear();
                foreach (string u in Users.Keys)
                    Console.WriteLine(u);

            }
        }
    }
}
