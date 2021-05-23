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
        public Room room;
        private int user_id;

        // Intancia nova thread para receber mennsagens da sala
        public bool Connect(int room_id, int user_id)
        {
            Room room    = GetRoom(room_id);
            this.room    = room;
            this.user_id = user_id;

            try
            {
                receiveThread = new Thread(new ThreadStart(ReceiveMessagesRoom));
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

        public void ReceiveMessagesRoom()
        {
            WebClient client = new WebClient();
            List<string> messages = new List<string>();

            while (Connected)
            {
                string strJson = client.DownloadString(Api.url + Api.path_message + "?room_id=" + room.id + "&private_user_id=" + user_id);
                dynamic dMessageObj = JsonConvert.DeserializeObject<dynamic>(strJson);

                for (int i = 0; i < dMessageObj.Count; i++)
                {
                    string userName = dMessageObj[i]["user"]["name"];
                    string message = dMessageObj[i]["message"];
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

        public void SendMessage(string message, int user_id, int room_id, int private_user_id)
        {
            MessageService msg = new MessageService();
            _ = msg.Send(message, user_id, room_id, private_user_id);
        }


        private Room GetRoom(int id)
        {
            WebClient client = new WebClient();
            string strJson = client.DownloadString(Api.url + Api.path_room + "?id=" + id);

            Room room = JsonConvert.DeserializeObject<Room>(strJson);

            return room;
        }
    }

    public class Room
    {
        public int id { get; set; }
        public string name { get; set; }

    }
}