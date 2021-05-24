using System;
using System.Collections.Generic;
using WebApplication3.Models;
using WebApplication3.Controllers;
using MySql.Data.MySqlClient;

namespace WebApplication3.Repository
{
    public class MessageRepository : IMessage
    {
        public Message create(Message message)
        {
            MySqlConnection conn = Config.conn();

            int id;
            var sql = message.private_user == null
                        ? "INSERT INTO message(message, user_id, room_id) VALUES(@value1, @value2, @value3); select last_insert_id();"
                        : "INSERT INTO message(message, user_id, room_id, private_user_id) VALUES(@value1, @value2, @value3, @value4); select last_insert_id();";
            var insert = new MySqlCommand(sql, conn);

            try
            {
                conn.Open();
                insert.Parameters.AddWithValue("@value1", message.message);
                insert.Parameters.AddWithValue("@value2", message.user.id);
                insert.Parameters.AddWithValue("@value3", message.room.id);
                if(message.private_user != null) 
                    insert.Parameters.AddWithValue("@value4", message.private_user.id);

                insert.Prepare();
                id = Convert.ToInt32(insert.ExecuteScalar());
            }
            catch (MySqlException e)
            {
                conn.Close();
                return null;
            }
            conn.Close();
            return get(id);
        }

        public Message get(int id)
        {
            UserRepository userRepository = new UserRepository();
            RoomRepository roomRepository = new RoomRepository();
            Message message               = null;

            MySqlConnection conn = Config.conn();
            MySqlCommand query   = conn.CreateCommand();

            query.CommandText = "SELECT * FROM message WHERE id = @id";
            query.Parameters.AddWithValue("@id", id);

            conn.Open();
            MySqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                message              = new Message();
                message.id           = Convert.ToInt32(reader["id"]);
                message.message      = reader["message"].ToString();
                message.user         = userRepository.get(Convert.ToInt32(reader["user_id"]));
                message.private_user = reader.IsDBNull(3) //column index private_user_id
                                        ? null 
                                        : userRepository.get(Convert.ToInt32(reader["private_user_id"]));
                message.room         = roomRepository.get(Convert.ToInt32(reader["room_id"]));
            }
            conn.Close();

            return message;
        }

        public List<Message> list(int room_id, User private_user)
        {
            UserRepository userRepository = new UserRepository();
            RoomRepository roomRepository = new RoomRepository();
            List<Message> listMessages    = new List<Message>();

            MySqlConnection conn = Config.conn();
            MySqlCommand query   = conn.CreateCommand();

            query.CommandText = "SELECT * FROM message WHERE room_id = @id AND private_user_id is null OR private_user_id = @private_user_id;";
            query.Parameters.AddWithValue("@id", room_id);
            query.Parameters.AddWithValue("@private_user_id", private_user.id);

            conn.Open();
            MySqlDataReader reader = query.ExecuteReader();

            while (reader.Read()) 
            {
                Message message      = new Message();
                message.id           = Convert.ToInt32(reader["id"]);
                message.message      = reader["message"].ToString();
                message.user         = userRepository.get(Convert.ToInt32(reader["user_id"]));
                message.private_user = reader.IsDBNull(3) //column index of private_user_id
                                        ? null
                                        : userRepository.get(Convert.ToInt32(reader["private_user_id"]));
                message.room         = roomRepository.get(room_id);

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