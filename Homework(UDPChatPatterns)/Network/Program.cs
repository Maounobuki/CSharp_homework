namespace Network
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Server server = new Server();
            Manager manager = new Manager(server);
            server.Start();
        }
    }
}