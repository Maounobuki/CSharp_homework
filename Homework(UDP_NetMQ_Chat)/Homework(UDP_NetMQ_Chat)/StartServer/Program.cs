using ChatApp;
using ChatNetwork;
using System.Net;

    
namespace StartServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serv = new Server();
            new Thread(async () => await serv.Start()).Start();
        }
    }
}