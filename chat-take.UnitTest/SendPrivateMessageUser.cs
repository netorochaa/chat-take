using chat_take.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace chat_take.UnitTest
{
    [TestClass]
    public class SendPrivateMessageUser
    {
        private MessageService messageService;
        private bool result;

        [TestInitialize]
        public void Initialize()
        {
            Arrange();
            Act();
        }
        private void Arrange()
        {
            messageService = new MessageService();
        }

        private void Act()
        {
            result = messageService.ValidCommandPrivateMsg("/p 1 Teste");
        }

        [TestMethod]
        public void WhenSendPrivateMessage_returnTrue()
        {
            Assert.IsTrue(result);
        }
    }

    
}
