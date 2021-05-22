using System.Collections.Generic;
using System.Web.Http;
using WebApplication3.Models;
using WebApplication3.Repository;

namespace WebApplication3.Controllers
{
    public class MessageController : ApiController
    {
        public List<Message> Get(int id)
        {
            MessageRepository repository = new MessageRepository();
            return repository.list(id); // room_id
        }

        public bool Post(string message, int user_id, int room_id)
        {
            UserRepository userRepository = new UserRepository();
            RoomRepository roomRepository = new RoomRepository();

            User user = userRepository.get(user_id);
            Room room = roomRepository.get(room_id);

            //Valida mensagem
            if (!message.Equals(null) || !message.Equals(string.Empty))
            {
                MessageRepository messageRepository = new MessageRepository();
                Message messageRoom = new Message(message, user, room);

                return messageRepository.create(messageRoom);
            }
            else return false;
        }
    }
}
