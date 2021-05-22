using chat_take.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace chat_take.UnitTest
{
    [TestClass]
    public class CreateUserTest
    {
        private UserService userService;
        private bool result;
        [TestInitialize]
        public void Initialize()
        {
            Arrange();
            Act();
        }

        private void Arrange()
        {
            userService = new UserService();
        }

        private void Act()
        {
            result = userService.ExistsOrInvalid(string.Empty);
        }

        [TestMethod]
        public void WhenGetBlankName_returnTrue()
        {
            Assert.IsTrue(result);
        }
    }
}
