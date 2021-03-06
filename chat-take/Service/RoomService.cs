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
        private Thread  receiveThread;
        private bool    Connected;
        public  Room    room;
        private int     user_id;

        // Nova instância de thread para receber mennsagens privadas ou da sala em background enquanto estiver conectado
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
            WebClient client      = new WebClient();
            List<string> messages = new List<string>();

            while (Connected)
            {
                string strJson      = client.DownloadString(Api.url + Api.path_message + "?room_id=" + room.id + "&private_user_id=" + user_id);
                dynamic dMessageObj = JsonConvert.DeserializeObject<dynamic>(strJson);

                for (int i = 0; i < dMessageObj.Count; i++)
                {
                    string userName        = dMessageObj[i]["user"]["name"];
                    string message         = dMessageObj[i]["message"];
                    string privateUserName = dMessageObj[i]["private_user"] != null 
                                                ? dMessageObj[i]["private_user"]["name"]
                                                : null;

                    string finalMessage = privateUserName == null 
                                            ? userName + " disse: " + message
                                            : userName + " disse no privado para " + privateUserName + ": " + message;
                    if (!messages.Contains(finalMessage))
                    {
                        messages.Add(finalMessage);
                        Console.WriteLine(finalMessage);
                    }
                }
            }
            Connected = false;
        }

        public void ShowMenu()
        {
            Console.WriteLine("MENU DE OPÇÕES");
            Console.WriteLine("/p [id do usuario] [mensagem] - Enviar uma mensagem privad \r\n" +
                              "/sair                         - Sair do chat\r\n" +
                              "/usuarios                     - Listar usuários da sala\r\n");
        }

        public void SendMessage(string message, int user_id, int room_id, int private_user_id)
        {
            MessageService msg = new MessageService();
            if(msg.ValidMessage(message))
                _ = msg.Send(message, user_id, room_id, private_user_id);
        }

        public void ListUsersRoom(UserService userService)
        {
            List<User> users = userService.GetList();
            Console.WriteLine("ID - NOME");
            foreach (var item in users)
                Console.WriteLine(item.id + " - " + item.name);
        }

        private Room GetRoom(int id)
        {
            WebClient client = new WebClient();
            string strJson   = client.DownloadString(Api.url + Api.path_room + "?id=" + id);

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