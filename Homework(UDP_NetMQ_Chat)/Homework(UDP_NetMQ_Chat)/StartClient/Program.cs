using ChatApp;

namespace StartClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new Client(args[0]);
            await client.Start();
        }
    }
}
