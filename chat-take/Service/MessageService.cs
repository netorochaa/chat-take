using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using chat_take.Config;
using Newtonsoft.Json;

namespace chat_take.Service
{
    public class MessageService
    {
        //Envia menssagem pública ou privada
        public async Task<Message> Send(string message, int user_id, int room_id, int private_user_id)
        {
            HttpClient client            = new HttpClient();
            client.BaseAddress           = new Uri(Api.url);
            string path_uri              = Uri.UnescapeDataString("?message=" + message + "&user_id=" + user_id + "&room_id=" + room_id + "&private_user_id=" + private_user_id);
            HttpResponseMessage response = await client.PostAsync(Api.path_message + path_uri, null);

            string data             = await response.Content.ReadAsStringAsync();
            Message messageResponse = JsonConvert.DeserializeObject<Message>(data);

            return messageResponse;
        }
    }

    public class Message
    {
        public int id { get; set; }
        public string message { get; set; }
        public User user { get; set; }
        public Room room { get; set; }
    }
}
