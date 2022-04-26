using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using nsAPI.Entities;
using nsAPI.Methods;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace nsAPI
{
    public enum TypeMethod
    {
        // USERS
        USERS_GET = 100,
        USERS_DELETE = 101,
        USERS_AUTH = 102,
        USERS_REG = 103,
        // CLASSES
        CLASSES_GET = 200,
        // JOURNALS
        JOURNALS_GET = 300,
        // TESTS
        TESTS_GET = 400,
        // CLIENT
        CLIENT_AUTH = 700,
        // References Books
        GENDERS_GET = 600
    }


    public class API
    {
        //public int CountUsers = 10;
        //private long shift = 0;

        /// <summary>
        /// 
        /// </summary>
        public RegisteredUser MainUser = null;

        /// <summary>
        /// Путь для хранения файла данных о текущем пользователе.
        /// </summary>
        private const string pathAccessToken = "user\\ukru";

        // Ссылка для запросов.
        //private const string apiURL = "http://api.great-duet.ru/";

        // Ключ, который формирует сервер, для доступа к запросам.
        private string api_token = string.Empty; // AT
        // Идентификатор текущего пользователя.
        private string id_user = string.Empty; // 

        //
        private readonly MUsers users;
        private readonly MClassrooms classrooms;
        private readonly MRefBooks refBooks;
        /// <summary>
        /// Ключ доступа к API.
        /// </summary>
        public string Access_Token { get { return api_token; } }

        // Конструктор класса.
        public API()
        {
            // Для работы с запросами касающимися пользователей.
            users = new MUsers();
            classrooms = new MClassrooms();
            refBooks = new MRefBooks();

            // Если возможно, то загружаем данные пользователя из файла.
            try
            {
                if (File.Exists(pathAccessToken))
                {
                    LoadUserDataFromFile();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("При попытке считать из файла пользователя произошла ошибка:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Возвращает сохраненный раннее Access_Token.
        /// </summary>
        /// <returns></returns>
        private string getAccessToken()
        {
            if (!string.IsNullOrEmpty(api_token))
            {
                return Access_Token;
            }
            else
            {
                // Пробуем загрузить ключ из файла.
                LoadUserDataFromFile();
                // Рекурсивно взываем себя.
                return getAccessToken();
            }
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="user">Данные регистрации.</param>
        /// <returns>True - при успешной регистрации.</returns>
        public async Task<bool> UserRegAsync(UserForRegistration user)
        {
            // При успехе, будет содержать ключ доступа к серверу.
            var accessToken = await users.RegAsync(user);
            if (accessToken != null)
            {
                // Сохраняем токен пользователя.
                api_token = accessToken.Token;
                // Сохраняем ID пользователя.
                id_user = accessToken.UserID;
                // Сохраняем токен в файл с перезаписью существующего файла.
                SaveUserDataToFile(true);
                // Загружаем информацию о пользователе с сервера.
                MainUser = GetUserById(accessToken.UserID);
                // Успех!
                return true;
            }
            // Если полученная строка незнакома.
            return false;
        }

        
        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="user">Объект с данными для авторизации пользователя</param>
        /// <returns>True - при успешной авторизации</returns>
        public bool UserAuth(UserForAuthorization user)
        {
            // При успехе, будет содержать ключ доступа к серверу.
            AccessToken accessToken;
            if (users.TryAuth(user, out accessToken))
            {
                // Сохраняем токен пользователя.
                api_token = accessToken.Token;
                // Сохраняем ID пользователя.
                id_user = accessToken.UserID;
                // Сохраняем в файл с перезаписью существующего файла.
                SaveUserDataToFile(true);
                // Загружаем информацию о пользователе с сервера.
                MainUser = GetUserById(accessToken.UserID);
                // Успех!
                return true;
            }
            return false;
        }


        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login">Строка логина</param>
        /// <param name="pass">Строка пароля</param>
        /// <returns>true - при успешной авторизации.</returns>
        public bool UserAuth(string login, string pass)
        {
            return UserAuth(new UserForAuthorization
            {
                Login = login,
                Pass = pass
            });
        }

        /// <summary>
        /// Получает подробную информацию об указанном количестве пользователей.
        /// </summary>
        /// <param name="count">Кол-во пользователей. Макс: 50</param>
        /// <param name="shift">Смещение, относительно первого найденного.</param>
        /// <returns>Информация о пользователях.</returns>
        public List<RegisteredUser> FindUsers(string searchName = null, string searchMName = null, string searchSurname = null)
        {
            // Пробуем найти пользователей.
           return users.Find(api_token, searchName, searchMName, searchSurname);
        }

        public List<RegisteredUser> NextUsers(int count)
        {
            return null;
        }

        /// <summary>
        /// Получает подробную информацию о пользоателях с заданными ID.
        /// </summary>
        /// <param name="userIds">ID пользователей.</param>
        /// <returns>Информация о пользователях.</returns>
        public List<RegisteredUser> GetUsersById(string[] userIds)
        {
            if (userIds == null || userIds.Count()<=0)
            {
                return null;
            }
            return users.ById(api_token, userIds);
        }

        /// <summary>
        /// Получение информации о пользователе с заданным ID
        /// </summary>
        public RegisteredUser GetUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            return users.ById(api_token, userId);
        }

        /// <summary>
        /// Запрашивает у сервера список полов.
        /// </summary>
        /// <returns></returns>
        public List<Gender> GetGenders()
        {
            List<Gender> genders;
            if (null == (genders = refBooks.GetListGenders()))
            {
                Console.WriteLine("Ошибка при получении списка полов!");
            }
            return genders;
        }

        /// <summary>
        /// Сохраняет access_token и другие данные о пользователе в файл.
        /// </summary>
        public void SaveUserDataToFile(bool rewrite)
        {
            // Проверяем, что папка user существует, если нет, то создаем её.
            if (!Directory.Exists("user")) Directory.CreateDirectory("user");
            // Проверяем, отсутствует ли файл токена или указан флаг перезаписи.
            if (!File.Exists(pathAccessToken) || rewrite)
            {
                // Открываем файл токена (попутно создаем, если файла не было).
                using (var sw = new StreamWriter(pathAccessToken))
                {
                    // Шифруем токен и пишем в файл.
                    sw.WriteLine(Encryption.TripleDesHelper.EncryptString(api_token));
                    // Шифруем идентификатор и пишем в файл.
                    sw.WriteLine(Encryption.TripleDesHelper.EncryptString(id_user));
                }
            }
            // Если файл уже существует, а флаг перезаписи не был задан, то ошибка!
            else
            {
                // Ошибка!
                throw new Exception("File of UserData already exist");
            }
        }

        /// <summary>
        /// Загружает access_token и другие данные о пользователе из файла.
        /// </summary>
        /// <returns></returns>
        public bool LoadUserDataFromFile()
        {
            if (File.Exists(pathAccessToken))
            {
                using (var sw = new StreamReader(pathAccessToken))
                {
                    api_token = Encryption.TripleDesHelper.DecryptString(sw.ReadLine());
                    id_user = Encryption.TripleDesHelper.DecryptString(sw.ReadLine());
                    return true;
                }
            }
            else
            {
                throw new Exception("File of UserData not found");
                //log("File of acctok not found");
            }
            //return false;
        }

        public bool ClassReg(ClassroomForReg classroom, out RegisteredClassroom registeredClass)
        {
            // Попытка регистрации.
            return classrooms.TryReg(api_token, classroom, out registeredClass);
        }

    }

    static class helper
    {
        /// <summary>
        /// Пробует преобразовать строку в число, либо вернёт null если там оно будет.
        /// </summary>
        public static int? ToNullableInt(this string s)
        {
            if (int.TryParse(s, out int i)) return i;
            return null;
        }
    }


    public static class Serialize
    {
        public static string ToJson(this List<RegisteredUser> self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this RegisteredUser self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this Error self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this UserForRegistration self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this UserForAuthorization self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this ClassroomForReg self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this RegisteredClassroom self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
    

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
