using chat_take.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace chat_take.UnitTest
{
    [TestClass]
    public class NoSendMessageCmdsMenuTest
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
            result = messageService.ValidMessage("/");
        }

        [TestMethod]
        public void WhenSendCommandMenu_returnFalse()
        {
            Assert.IsFalse(result);
        }
    }
}
