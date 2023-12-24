using ChatCommon.Models;
using NetMQ.Sockets;
using NetMQ;


namespace ChatNetwork
{
    public class NetMQMessageSourceServer
    {
        public NetMessage Receive(RouterSocket ep)
        {

            var mes = ep.ReceiveMultipartMessage();
            string str = mes.Last.ConvertToString();
            var rm = new NetMQMessage();
            rm.Append(mes.First);
            rm.Append("сообщкние доставлено");
            ep.SendMultipartMessage(rm);
            return NetMessage.DeserializeMessgeFromJSON(str) ?? new NetMessage();
        }
    }
}
