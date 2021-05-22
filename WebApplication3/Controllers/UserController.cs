using System.Collections.Generic;
using System.Web.Http;
using WebApplication3.Repository;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class UserController : ApiController
    {
        public List<User> GetAll()
        {
            UserRepository repository = new UserRepository();
            return repository.list();
        }

        public User Get(int id)
        {
            UserRepository repository = new UserRepository();
            return repository.get(id);
        }

        public User Post(string name)
        {
            UserRepository userRepository = new UserRepository();
            RoomRepository roomRepository = new RoomRepository();

            User user = userRepository.create(new User(name));

            //Se usuário for criado, é inserido na salaPublica
            if (!user.Equals(null) 
                && roomRepository.createUserRoom(user, roomRepository.get(1))) //1 - salaPublica
                return user;
            else
                return null;
        }
    }
}
