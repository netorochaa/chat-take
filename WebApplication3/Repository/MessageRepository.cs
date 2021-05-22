using System;
using System.Collections.Generic;
using WebApplication3.Models;
using WebApplication3.Controllers;
using MySql.Data.MySqlClient;

namespace WebApplication3.Repository
{
    public class MessageRepository : IMessage
    {
        public bool create(Message message)
        {
            MySqlConnection conn = Config.conn();

            var sql = "INSERT INTO message(message, user_id, room_id) VALUES(@value1, @value2, @value3)";
            var insert = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                insert.Parameters.AddWithValue("@value1", message.message);
                insert.Parameters.AddWithValue("@value2", message.user.id);
                insert.Parameters.AddWithValue("@value3", message.room.id);
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

        public Message get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Message> list(int room_id)
        {
            UserRepository userRepository = new UserRepository();
            RoomRepository roomRepository = new RoomRepository();
            List<Message> listMessages = new List<Message>();

            MySqlConnection conn = Config.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT * FROM message WHERE room_id = @id";
            query.Parameters.AddWithValue("@id", room_id);

            conn.Open();
            MySqlDataReader reader = query.ExecuteReader();

            while (reader.Read()) 
            {
                Message message = new Message();
                message.id      = Convert.ToInt32(reader["id"]);
                message.message = reader["message"].ToString();
                message.user    = userRepository.get(Convert.ToInt32(reader["user_id"]));
                message.room    = roomRepository.get(room_id);

                listMessages.Add(message);
            }

            conn.Close();
            return listMessages;
        }

        public void remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}