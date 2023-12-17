using Homework_Entity_Databases_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Entity_Databases_
{
    public interface IMessageSource
    {
        Task SendAsync(NetMessage message , IPEndPoint ep);

        NetMessage Receive(ref IPEndPoint ep);
    }
}
