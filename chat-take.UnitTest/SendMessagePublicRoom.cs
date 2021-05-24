using chat_take.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace chat_take.UnitTest
{
    [TestClass]
    public class SendMessagePublicRoom
    {
        private MessageService messageService;
        private Message message;
        private Random random;

        [TestInitialize]
        public void Initialize()
        {
            Arrange();
            Act().GetAwaiter().GetResult();
        }

        private void Arrange()
        {
            messageService = new MessageService();
            random         = new Random();
        }

        private async Task Act()
        {
            string validMessage = RandomString(10);
            message             = await messageService.Send(validMessage, 1, 1, 1); // 1 - admin, 1 - salaPublica, 0 - admin
        }

        [TestMethod]
        public void WhenSendMessagePublicRoom_returnMessage()
        {
            Assert.IsNotNull(message);
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&*()";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
