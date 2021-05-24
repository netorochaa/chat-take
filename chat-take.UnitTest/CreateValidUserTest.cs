using chat_take.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace chat_take.UnitTest
{
    [TestClass]
    public class CreateValidUserTest
    {
        private UserService userService;
        private User user;
        private Random random;

        [TestInitialize]
        public void Initialize()
        {
            Arrange();
            Act().GetAwaiter().GetResult();
        }

        private void Arrange()
        {
            userService = new UserService();
            random      = new Random();
        }

        private async Task Act()
        {
            string nameValidUser = RandomString(5);
            user                 = await userService.CreateUserAsync(nameValidUser);
        }

        [TestMethod]
        public void WhenValidUser_returnUser()
        {
            Assert.IsNotNull(user);
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
