using System.Collections.Generic;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    interface IMessage
    {
        List<Message> list(int room_id, User private_user);
        Message get(int id);
        Message create(Message message);
        void remove(int id);
    }
}
