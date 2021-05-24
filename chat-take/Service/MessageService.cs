using System;
using System.Linq;
using System.Net.Http;
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
            //Se validada, envia mensagem privada
            string[] messageSplit = message.Split(' ');

            if (messageSplit.Length > 2 && messageSplit[0] == "/p")
            {
                private_user_id = Convert.ToInt32(messageSplit[1]);
                message         = RemoveCommandPrivateMessage(messageSplit);
            }

            HttpClient client            = new HttpClient();
            client.BaseAddress           = new Uri(Api.url);
            string path_uri              = Uri.UnescapeDataString("?message=" + message + "&user_id=" + user_id + "&room_id=" + room_id + "&private_user_id=" + private_user_id);
            HttpResponseMessage response = await client.PostAsync(Api.path_message + path_uri, null);

            string data             = await response.Content.ReadAsStringAsync();
            Message messageResponse = JsonConvert.DeserializeObject<Message>(data);

            return messageResponse;
        }

        public bool ValidMessage(string message)
        {
            if (!string.IsNullOrEmpty(message) && ValidCommandPrivateMsg(message))
                return true;
            else 
                return false;
        }

        public bool ValidCommandPrivateMsg(string message)
        {
            message                     = message.TrimEnd();
            string[] messageSplit       = message.Split(' ');
            string caracterInitCmdMenu  = message.Substring(0, 1);
            string caracterCmdMenu      = message.Length > 2 
                                            ? message.Substring(1, 2)
                                            : string.Empty;

            if (messageSplit.Length > 2)
            {
                if (caracterInitCmdMenu == "/" && caracterCmdMenu == "p")
                    return true;
            }
            else
            {
                if (caracterInitCmdMenu == "/")
                    return false;
            }
            return true;
        }

        private string RemoveCommandPrivateMessage(string[] messageSplit)
        {
            var lista    = messageSplit.ToList();
            lista.RemoveAt(0);
            lista.RemoveAt(0);

            messageSplit = lista.ToArray();
            string msg   = string.Join(" ", messageSplit);

            return msg;
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
