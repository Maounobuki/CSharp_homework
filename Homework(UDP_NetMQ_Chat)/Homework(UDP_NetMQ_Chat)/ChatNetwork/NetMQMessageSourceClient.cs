using ChatCommon.Abstractions;
using ChatCommon.Models;
using NetMQ;
using NetMQ.Sockets;


namespace ChatNetwork
{
    public class NetMQMessageSourceClient
    {
        public NetMessage Receive(SubscriberSocket socket)
        {
            var topic = socket.ReceiveFrameString();
            string str = socket.ReceiveFrameString();
            return NetMessage.DeserializeMessgeFromJSON(str) ?? new NetMessage();
        }

        public void Send(NetMessage message, DealerSocket socket)
        {
            socket.SendFrame(message.SerialazeMessageToJSON());
            Console.WriteLine(socket.ReceiveFrameString());
        }
    }
}
