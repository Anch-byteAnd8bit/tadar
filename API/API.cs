using Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using nsAPI.Entities;
using nsAPI.Methods;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace nsAPI
{
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
        private string pathAccessToken = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Tadar\\user\\ukru");
        //private string pathAccessToken = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "Tadar\\user\\ukru");
        
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
                    LoadUserDataFromFileAsync();
                }
                else
                {
                    Log.Write("При попытке считать из файла пользователя произошла ошибка:\n" +
                        "Не найден файл с данными пользователя.");
                }
            }
            catch (Exception e)
            {
                Log.Write("При попытке считать из файла пользователя произошла ошибка: \n" + e.Message);
            }
        }

        // =========== Пользователи

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="user">Данные регистрации.</param>
        /// <returns>True - при успешной регистрации.</returns>
        public async Task UserRegAsync(UserForRegistration user)
        {
            // При успехе, будет содержать ключ доступа к серверу.
            var accessToken = await users.RegAsync(user);
            //
            await UserAuthAsync(user.UserForAuthorization);
            /*// Сохраняем токен пользователя.
            api_token = accessToken.Token;
            // Сохраняем ID пользователя.
            id_user = accessToken.UserID;
            // Сохраняем токен в файл с перезаписью существующего файла.
            SaveUserDataToFileAsync(true);
            // Загружаем информацию о пользователе с сервера.
            MainUser = await GetUserByIdAsync(accessToken.UserID);*/
            // Успех!
            //return true;
        }

        
        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="user">Объект с данными для авторизации пользователя</param>
        /// <returns>True - при успешной авторизации</returns>
        public async Task UserAuthAsync(UserForAuthorization user)
        {
            // При успехе, будет содержать ключ доступа к серверу.
            AccessToken accessToken = await users.AuthAsync(user);
            // Сохраняем токен пользователя.
            api_token = accessToken.Token;
            // Сохраняем ID пользователя.
            id_user = accessToken.UserID;
            // Сохраняем в файл с перезаписью существующего файла.
            SaveUserDataToFileAsync(true);
            // Загружаем информацию о пользователе с сервера.
            MainUser = await GetUserByIdAsync(accessToken.UserID);
        }


        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login">Строка логина</param>
        /// <param name="pass">Строка пароля</param>
        public async Task UserAuthAsync(string login, string pass)
        {
            await UserAuthAsync(new UserForAuthorization
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
        public async Task<List<RegisteredUser>> FindUsersAsync(string searchName = null, string searchMName = null, string searchSurname = null)
        {
            // Пробуем найти пользователей.
           return await users.FindAsync(api_token, null, searchName, searchMName, searchSurname);
        }

        /// <summary>
        /// Получает подробную информацию о пользоателях с заданными ID.
        /// </summary>
        /// <param name="userIds">ID пользователей.</param>
        /// <returns>Информация о пользователях.</returns>
        public async Task<List<RegisteredUser>> GetUsersByIdAsync(string[] userIds) => 
            await users.ByIdAsync(api_token, userIds);

        /// <summary>
        /// Получение информации о пользователе с заданным ID
        /// </summary>
        public async Task<RegisteredUser> GetUserByIdAsync(string userId) =>
            await users.ByIdAsync(api_token, userId);

        // =========== "Справочники"

        /// <summary>
        /// Запрашивает у сервера список полов.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Refbook>> GetGendersAsync() =>
            await refBooks.GetListGendersAsync();

        // =========== Классы

        /// <summary>
        /// Получение информации о классах с заданными ID
        /// </summary>
        public async Task<List<RegisteredClassroom>> GetClassroomsByIdAsync(string[] classroomIds) =>
            await classrooms.ByIdAsync(api_token, classroomIds);

        /// <summary>
        /// Получение информации о классе с заданными ID
        /// </summary>
        public async Task<RegisteredClassroom> GetClassroomByIdAsync(string classroomId) =>
            await classrooms.ByIdAsync(api_token, classroomId);

        /// <summary>
        /// Получение информации о классах в которых состоит пользователь с заданным id.
        /// </summary>
        public async Task<List<RegisteredClassroom>> GetClassroomsByUserIdAsync(
            string userId, string roleId = null) => await classrooms.ByUserIdAsync(
                api_token, userId, roleId);
        // =========== Тесты

        //==================================================================================
        //==================================================================================
        //==================================================================================


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
                LoadUserDataFromFileAsync();
                // Рекурсивно взываем себя.
                return getAccessToken();
            }
        }

        /// <summary>
        /// Сохраняет access_token и другие данные о пользователе в файл.
        /// </summary>
        public void SaveUserDataToFileAsync(bool rewrite)
        {
            // Проверяем, что папка user существует, если нет, то создаем её.
            if (!Directory.Exists(Path.GetDirectoryName(pathAccessToken))) Directory.CreateDirectory(Path.GetDirectoryName(pathAccessToken));
            // Проверяем, отсутствует ли файл токена или указан флаг перезаписи.
            if (!File.Exists(pathAccessToken) || rewrite)
            {
                // Открываем файл токена (попутно создаем, если файла не было).
                using (var sw = new StreamWriter(pathAccessToken))
                {
                    // Шифруем токен и идентификатор.
                    string encToken = Encryption.TripleDesHelper.EncryptString(api_token);
                    string encId = Encryption.TripleDesHelper.EncryptString(id_user);
                    // Пишем в файл.
                    sw.WriteLine(encToken);
                    sw.WriteLine(encId);
                }
            }
            // Если файл уже существует, а флаг перезаписи не был задан, то ошибка!
            else
            {
                // Ошибка!
                throw new IOException("File of UserData already exist");
            }
        }

        /// <summary>
        /// Загружает access_token и другие данные о пользователе из файла.
        /// </summary>
        /// <returns></returns>
        public bool LoadUserDataFromFileAsync()
        {
            if (File.Exists(pathAccessToken))
            {
                using (var sw = new StreamReader(pathAccessToken))
                {
                    api_token = sw.ReadLine();
                    id_user = sw.ReadLine();

                    api_token = Encryption.TripleDesHelper.DecryptString(api_token);
                    id_user =  Encryption.TripleDesHelper.DecryptString(id_user);
                    return true;
                }
            }
            else
            {
                throw new IOException("File of UserData not found");
            }
            //return false;
        }

        /// <summary>
        /// Создание нового класа.
        /// </summary>
        /// <param name="classroom"></param>
        /// <returns></returns>
        public async Task<RegisteredClassroom> ClassRegAsync(ClassroomForReg classroom) =>
            await classrooms.RegAsync(api_token, classroom);
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
