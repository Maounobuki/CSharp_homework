using Homework_Entity_Databases_.Models;
using System.Net;

namespace Homework_Entity_Databases_.Abstracts
{
    public interface IMessageSource
    {
        Task SendAsync(NetMessage message , IPEndPoint ep);

        NetMessage Receive(ref IPEndPoint ep);
    }
}
