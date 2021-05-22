using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using chat_take.Config;

namespace chat_take.Service
{
    public class UserService
    {
        //Verifica se o nome existe ou é válido
        public bool ExistsOrInvalid(string name)
        {
            if (nameIsValid((name.Trim())))
            {
                User user = new User(name);
                WebClient client = new WebClient();
                string strJson = client.DownloadString(Api.url + Api.path_user);

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
            client.BaseAddress = new Uri(Api.url);
            HttpResponseMessage response = await client.PostAsync(Api.path_user + "?name=" + name, null);

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
