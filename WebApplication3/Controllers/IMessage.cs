using System.Collections.Generic;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    interface IMessage
    {
        List<Message> list(int room_id);
        Message get(int id);
        bool create(Message message);
        void remove(int id);
    }
}
