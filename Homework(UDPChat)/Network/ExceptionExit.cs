using System.Runtime.Serialization;

namespace Network
{
    [Serializable]
    internal class ExceptionExit : Exception
    {
        public ExceptionExit() { }
        public ExceptionExit(string? message) : base(message) { }
    }
}