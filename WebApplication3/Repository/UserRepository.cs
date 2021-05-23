using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WebApplication3.Models;
using WebApplication3.Controllers;

namespace WebApplication3.Repository
{
    public class UserRepository : IUser
    {
        public User create(User user)
        {
            MySqlConnection conn = Config.conn();

            int id;
            var sql = "INSERT INTO user(name) VALUES(@value); select last_insert_id();";
            var insert = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                insert.Parameters.AddWithValue("@value", user.name);
                insert.Prepare();

                id = Convert.ToInt32(insert.ExecuteScalar());
            }
            catch (MySqlException)
            {
                conn.Close();
                return null;
            }
            conn.Close();
            return get(id);
        }

        public User get(int id)
        {
            User user = null;
            MySqlConnection conn = Config.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT * FROM user WHERE id = @id";
            query.Parameters.AddWithValue("@id", id);

            conn.Open();
            MySqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
                user = new User(Convert.ToInt32(reader["id"]),
                                reader["name"].ToString());

            conn.Close();

            return user;
        }

        public List<User> list()
        {
            List<User> listUsers = new List<User>();
            MySqlConnection conn = Config.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT * FROM user"; //WHERE id = @id";

            conn.Open();
            MySqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
                listUsers.Add(new User( Convert.ToInt32(reader["id"]), 
                                        reader["name"].ToString()));

            conn.Close();
            return listUsers;
        }

        public void remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}