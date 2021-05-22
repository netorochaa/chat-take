using System.Collections.Generic;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    interface IUser
    {
        List<User> list();
        User get(int id);
        User create(User user);
        void remove(int id);
    }
}
