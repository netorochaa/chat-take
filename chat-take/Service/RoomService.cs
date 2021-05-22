using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using chat_take.Config;

namespace chat_take.Service
{
    public class RoomService
    {
        private Thread receiveThread;
        private bool Connected;
        private int room_id;

        // Intancia nova thread para receber mennsagens da sala
        public bool Connect(int room_id)
        {
            this.room_id = room_id;
            try
            {
                receiveThread              = new Thread(new ThreadStart(Receive));
                receiveThread.IsBackground = true;
                receiveThread.Start();

                Connected = true;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Connected = false;
                return false;
            }
        }

        public void Receive()
        {
            WebClient client        = new WebClient();
            List<string> messages   = new List<string>();

            while (Connected)
            {
                string strJson      = client.DownloadString(Api.url + Api.path_message + "/" + room_id);
                dynamic dMessageObj = JsonConvert.DeserializeObject<dynamic>(strJson);

                for (int i = 0; i < dMessageObj.Count; i++)
                {
                    string userName     = dMessageObj[i]["user"]["name"];
                    string message      = dMessageObj[i]["message"];
                    string finalMessage = userName + " disse: " + message;
                    if (!messages.Contains(finalMessage))
                    {
                        messages.Add(finalMessage);
                        Console.WriteLine(finalMessage);
                    }
                }
            }
            Connected = false;
        }

        //Envia menssagem para a sala
        public async void Send(string message, int user_id, int room_id)
        {
            HttpClient client   = new HttpClient();
            client.BaseAddress  = new Uri(Api.url);
            string path_uri     = Uri.UnescapeDataString("?message=" + message + "&user_id=" + user_id + "&room_id=" + room_id);
            _ = await client.PostAsync(Api.path_message + path_uri, null);
        }
    }
}
