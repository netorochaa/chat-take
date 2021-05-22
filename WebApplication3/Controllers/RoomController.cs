using System.Web.Http;
using WebApplication3.Repository;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class RoomController : ApiController
    {
        public Room Get(int id)
        {
            RoomRepository repository = new RoomRepository();
            return repository.get(id);
        }
    }
}
