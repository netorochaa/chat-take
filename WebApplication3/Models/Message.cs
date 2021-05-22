namespace WebApplication3.Models
{
    public class Message
    {
        public int id { get; set; }
        public string message { get; set; }
        public User user { get; set; }
        public Room room { get; set; }

        public Message(string message, User user, Room room)
        {
            this.message = message;
            this.user = user;
            this.room = room;
        }

        public Message(){ }

    }
}
