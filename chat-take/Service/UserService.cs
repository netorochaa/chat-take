using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace chat_take.Service
{
    public class UserService
    {
        private const string api = "https://localhost:44363/";
        private const string path = "api/User";

        //Verifica se o nome existe ou é válido
        public bool Exists(string name)
        {
            if (nameIsValid((name.Trim())))
            {
                User user = new User(name);
                WebClient client = new WebClient();
                string strJson = client.DownloadString(api + path);

                dynamic dObj = JsonConvert.DeserializeObject<dynamic>(strJson);

                for (int i = 0; i < dObj.Count; i++)
                {
                    string nameInList = dObj[i]["name"];
                    if (nameInList.Equals(user.name)) return true;
                }
                return false;
            }
            else return true;
        }

        //Cria usuário
        public async Task<User> CreateUserAsync(string name)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(api);
            HttpResponseMessage response = await client.PostAsync(path + "?name=" + name, null);

            string data = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(data);

            return user;
        }

        public bool nameIsValid(string name)
        {
            if (!string.IsNullOrEmpty(name)) 
                return true;
            else 
                return false;
        }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }

        public User(string name)
        {
            this.name = name;
        }

    }
}
