using System.Net.Sockets;
using System.Net;
using Moq;
using Homework_Entity_Databases_.Models;
using Homework_Entity_Databases_.Services;
using Homework_Entity_Databases_.Abstracts;

namespace ClientTest
{
    
    public class Tests
    {
        Client _client;
        IPEndPoint _remoteEndPoint;
        Mock<IMessageSource> _messageSourceMock;

        [SetUp]
        public void Setup()
        {
            var name = "TestClient";
            var address = "127.0.0.1";
            var port = 1234;

            _messageSourceMock = new Mock<IMessageSource>();
            var udpClientMock = new Mock<UdpClient>();

            _client = new Client(name, address, port)
            {
                _messageSouce = _messageSourceMock.Object,
                udpClientClient = udpClientMock.Object
            };

            _remoteEndPoint = _client.remoteEndPoint;
        }

        [Test]
        public async Task ClientListener_ReceivesMessage_CallsConfirm()
        {
            var netMessage = new NetMessage { NickNameFrom = "Sender", Text = "Hello", Command = Command.Message };

            _messageSourceMock.Setup(m => m.Receive(ref _remoteEndPoint)).Returns(netMessage);
            _messageSourceMock.Setup(m => m.SendAsync(It.IsAny<NetMessage>(), It.IsAny<IPEndPoint>())).Returns(Task.CompletedTask);

            await _client.ClientListener();

            _messageSourceMock.Verify(m => m.Receive(ref _remoteEndPoint), Times.Once);
            _messageSourceMock.Verify(m => m.SendAsync(It.IsAny<NetMessage>(), _remoteEndPoint), Times.Once);
        }

        [Test]
        public void Register_CallsMessageSourceSendAsync()
        {
            _client.Register(_remoteEndPoint);

            _messageSourceMock.Verify(m => m.SendAsync(It.IsAny<NetMessage>(), _remoteEndPoint), Times.Once);
        }
    }
}
