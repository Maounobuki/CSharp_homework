using Homework_Entity_Databases_.Models;
using Homework_Entity_Databases_.Services;

namespace ServerTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            using (var ctx = new ChatContext())
            {
                ctx.Messages.RemoveRange(ctx.Messages);
                ctx.Users.RemoveRange(ctx.Users);
                ctx.SaveChanges();

            }
        }

        [TearDown]
        public void Teardown()
        {
            using (var ctx = new ChatContext())
            {
                ctx.Messages.RemoveRange(ctx.Messages);
                ctx.Users.RemoveRange(ctx.Users);
                ctx.SaveChanges();
            }
        }

        [Test]
        public async Task Test1()
        {
            var mock = new MockMessageSource();
            var srv = new Server(mock);
            mock.AddServer(srv);
            await srv.Start();

            using (var ctx = new ChatContext())
            {
                Assert.IsTrue(ctx.Users.Count() == 2, "������������ �� �������");

                var user1 = ctx.Users.FirstOrDefault(x => x.FullName == "����");
                var user2 = ctx.Users.FirstOrDefault(x => x.FullName == "���");

                Assert.IsNotNull(user1, "������������ 1 �� ������");
                Assert.IsNotNull(user2, "������������ 2 �� ������");

                Assert.IsTrue(user1.MessagesFrom.Count == 1);
                Assert.IsTrue(user2.MessagesFrom.Count == 1);

                Assert.IsTrue(user1.MessagesTo.Count == 1);
                Assert.IsTrue(user2.MessagesTo.Count == 1);

                var msg1 = ctx.Messages.FirstOrDefault(x => x.UserFrom == user1 && x.UserTo == user2);
                var msg2 = ctx.Messages.FirstOrDefault(x => x.UserFrom == user2 && x.UserTo == user1);

                Assert.AreEqual("100 ������ �� ����", msg2.Text);
                Assert.AreEqual("���������!", msg1.Text);
            }
        }
    }
}
