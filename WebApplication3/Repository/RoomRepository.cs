using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WebApplication3.Controllers;
using WebApplication3.Models;

namespace WebApplication3.Repository
{
    public class RoomRepository : IRoom
    {
        public bool create(Room room)
        {
            throw new NotImplementedException();
        }

        public Room get(int id)
        {
            Room room = null;
            MySqlConnection conn = Config.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT * FROM room WHERE id = @id";
            query.Parameters.AddWithValue("@id", id);

            conn.Open();
            MySqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
                room = new Room(Convert.ToInt32(reader["id"]), 
                                reader["name"].ToString());

            conn.Close();

            return room;
        }

        public List<Room> list()
        {
            throw new NotImplementedException();
        }

        public void remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool createUserRoom(User user, Room room)
        {
            MySqlConnection conn = Config.conn();

            var sql = "INSERT INTO user_has_room VALUES(@value1, @value2)";
            var insert = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                insert.Parameters.AddWithValue("@value1", user.id);
                insert.Parameters.AddWithValue("@value2", room.id);
                insert.Prepare();

                insert.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }
    }
}