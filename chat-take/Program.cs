using chat_take.Service;
using System;
using System.Threading.Tasks;

namespace chat_take
{
    class Program
    {
        private static bool connected = false;

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        private static async Task RunAsync()
        {
            try
            {
                Console.Write("Bem vindo ao chat-take. Insira seu nickname para entrar: ");
                while (true)
                {
                    UserService userService = new UserService();
                    string value            = Console.ReadLine();

                    if (!connected && userService.ExistsOrInvalid(value))
                        Console.Write("Este nick não esta disponível. Informe outro:");
                    else
                    {
                        var user = await userService.CreateUserAsync(value);
                        Console.WriteLine(user != null 
                                            ? "Você acabou de entrar na #salaPublica, digite /ajuda para listar os comandos" 
                                            : "Algo deu errado");
                        if (user != null)
                        {
                            RoomService roomService = new RoomService();
                            connected = roomService.Connect(1, user.id); // 1 - salaPublica

                            while (connected)
                            {
                                string yourMessage = Console.ReadLine();

                                if (yourMessage == "/sair")
                                    connected = false;
                                else if (yourMessage == "/usuarios")
                                    roomService.ListUsersRoom(userService);
                                else if (yourMessage == "/ajuda")
                                    roomService.ShowMenu();

                                roomService.SendMessage(yourMessage, user.id, roomService.room.id, 0); // 0 - null private_user_id
                            }
                        }
                        Console.WriteLine("Desconectado. Até mais!!");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
