namespace WebApplication3.Models
{
    public class Room
    {
        public int id { get; set; }
        public string name { get; set; }

        public Room(string name)
        {
            this.name = name;
        }
        public Room(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
