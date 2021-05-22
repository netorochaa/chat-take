using System.Collections.Generic;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    interface IRoom
    {
        List<Room> list();
        Room get(int id);
        bool create(Room room);
        void remove(int id);
    }
}
