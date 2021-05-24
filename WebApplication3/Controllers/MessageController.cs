using System.Collections.Generic;
using System.Web.Http;
using WebApplication3.Models;
using WebApplication3.Repository;

namespace WebApplication3.Controllers
{
    public class MessageController : ApiController
    {
        public List<Message> Get(int room_id, int private_user_id)
        {
            MessageRepository repository  = new MessageRepository();
            UserRepository userRepository = new UserRepository();
            User user                     = null;

            if (private_user_id != 0)
                user = userRepository.get(private_user_id);

            return repository.list(room_id, user);
        }

        public Message Post(string message, int user_id, int room_id, int private_user_id)
        {
            UserRepository userRepository       = new UserRepository();
            RoomRepository roomRepository       = new RoomRepository();
            MessageRepository messageRepository = new MessageRepository();
            Message messageRoom                 = null;

            User user         = userRepository.get(user_id);
            Room room         = roomRepository.get(room_id);
            User private_user = private_user_id != 0 
                                ? userRepository.get(private_user_id) 
                                : null;

            //Valida mensagem
            if (!message.Equals(null) || !message.Equals(string.Empty))
                messageRoom = private_user == null 
                    ? new Message(message, user, room) 
                    : new Message(message, user, private_user, room);
            
            return messageRepository.create(messageRoom); ;
        }
    }
}
