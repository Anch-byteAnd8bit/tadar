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
using System.Windows;

namespace nsAPI
{
    public class API
    {
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
        private string id_user = string.Empty;

        //
        private readonly MUsers users;
        private readonly MClassrooms classrooms;
        private readonly MRefBooks refBooks;
        private readonly MWorks works;
        private readonly MTheories theories;
        private readonly MAnswers answers;
        /// <summary>
        /// Ключ доступа к API.
        /// </summary>
        public string Access_Token { get { return api_token; } }

        // Конструктор класса.
        public API(Action OnLoadedMainUser = null)
        {
            // Для работы с запросами касающимися пользователей.
            users = new MUsers();
            classrooms = new MClassrooms();
            refBooks = new MRefBooks();
            works = new MWorks();
            theories = new MTheories();
            answers = new MAnswers();
            // Если возможно, то загружаем данные пользователя из файла.
            try
            {
                if (File.Exists(pathAccessToken))
                {
                    LoadUserDataFromFile();
                    LoadMainUser(OnLoadedMainUser);
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

        /// <summary>
        /// Асинхронно загружает пользователя.
        /// </summary>
        /// <param name="action">Процедура, 
        /// которую надо вызвать после успешной загрузки пользователя.</param>
        private async void LoadMainUser(Action action = null)
        {
            try
            {
                MainUser = await users.ByIdAsync(Access_Token, id_user);
                if (action != null) action();
            }
            catch (UnknownHttpResponseException ex)
            {
                _ = MessageBox.Show(ex.Message + "\n" + ex.ResponseJSON);
            }
        }

        #region Users
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
            SaveUserDataToFile(true);
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
        #endregion

        #region Refbooks
        // =========== "Справочники"

        /// <summary>
        /// Запрашивает у сервера список полов.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Refbook>> GetGendersAsync() =>
            await refBooks.GetListGendersAsync();

        /// <summary>
        /// Возвращает все данные из указанного справочника.
        /// </summary>
        /// <param name="nameRefbook">Наименование справочника маленькими буквами.</param>
        /// <returns>Все данные из указанного справочника.</returns>
        public async Task<List<Refbook>> GetDataFromRefbook(string nameRefbook) =>
            await refBooks.GetAllDataAsync(nameRefbook);

        /// <summary>
        /// Добавляет данные в указанный справочник.
        /// </summary>
        /// <param name="refbook">Наименование справочника (таблицы в БД)</param>
        /// <param name="names">Данные.</param>
        /// <returns>Ничего.</returns>
        public async Task<bool> AddDataToRefbook(string refbook, string[] names) =>
            await refBooks.AddDataToRefbook(Access_Token, refbook, new List<string>(names));

        /// <summary>
        /// Добавляет данные в указанный справочник.
        /// </summary>
        /// <param name="refbook">Наименование справочника (таблицы в БД)</param>
        /// <param name="names">Данные.</param>
        /// <returns>Ничего.</returns>
        public async Task<bool> AddDataToRefbook(string refbook, List<string> names) =>
            await refBooks.AddDataToRefbook(Access_Token, refbook, names);
        #endregion

        #region Classes
        // =========== Классы


        /// <summary>
        /// Создание нового класа.
        /// </summary>
        /// <param name="classroom"></param>
        /// <returns></returns>
        public async Task<RegisteredClassroom> AddClassroomAsync(ClassroomForReg classroom) =>
            await classrooms.RegAsync(api_token, classroom);

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

        /// <summary>
        /// Добавляет пользовтаеля в класс в роли ученика.
        /// </summary>
        /// <param name="id_user">Идентификтаор пользователя.</param>
        /// <param name="id_class">Идентификтаор класса.</param>
        /// <returns>True - если добавление прошло без проблем.</returns>
        public async Task<bool> AddStudent(string id_user, string id_class) =>
            await classrooms.AddStudent(Access_Token, id_user, id_class);

        #endregion

        #region Works
        // =========== Тесты
        /// <summary>
        /// Возвращает список работ по заданным идентификаторам журналов.
        /// </summary>
        /// <param name="journalsID">Массив строк идентификаторов журналов.</param>
        /// <param name="onlyHeaders">Только заголовки работ.</param>
        /// <returns>Список работ.</returns>
        public async Task<Works> GetWorksByJournalAsync(string[] journalsID, bool onlyHeaders = true) =>
            await works.ByJournalsIdsAsync(Access_Token, journalsID, onlyHeaders);

        /// <summary>
        /// Возвращает работу по заданному идентификатору журнала.
        /// </summary>
        /// <param name="journalID">Строка идентификатора журнала.</param>
        /// <param name="onlyHeaders">Только заголовок работы.</param>
        /// <returns>Работа.</returns>
        public async Task<Works> GetWorksByJournalAsync(string journalID, bool onlyHeaders = true) =>
            await works.ByJournalIdAsync(Access_Token, journalID, onlyHeaders);

        /// <summary>
        /// Добавляет в БД тестовую новую работу.
        /// </summary>
        /// <param name="testWork">Работа для записи в БД.</param>
        /// <returns>Идентификатор добавленной работы в строке</returns>
        public async Task<string> AddTestWorkAsync(TestWorkForAdd testWork) =>
            await works.TestWorkAddAsync(Access_Token, testWork);

        /// <summary>
        /// Добавляет в БД новую письменную работу.
        /// </summary>
        /// <param name="testWork">Работа для записи в БД.</param>
        /// <returns>Идентификатор добавленной работы в строке</returns>
        public async Task<string> AddTextWorkAsync(TextWorkForAdd textWork) =>
            await works.TextWorkAddAsync(Access_Token, textWork);
        #endregion

        #region Answers
        // =========== Ответы

        /// <summary>
        /// Возвращает список ответов по заданным ИД работ.
        /// </summary>
        /// <param name="worksID"></param>
        /// <param name="onlyHeaders"></param>
        /// <returns></returns>
        public async Task<Answers> GetAnswersByWorks(string[] worksID, bool onlyHeaders = true) =>
            await answers.ByWorksIdsAsync(Access_Token, worksID, onlyHeaders);

        /// <summary>
        /// Возвращает список ответов по заданному ИД работы.
        /// </summary>
        /// <param name="workID"></param>
        /// <param name="onlyHeaders"></param>
        /// <returns></returns>
        public async Task<Answers> GetAnswersByWork(string workID, bool onlyHeaders = true) =>
            await answers.ByWorkIdAsync(Access_Token, workID, onlyHeaders);

        /// <summary>
        /// Добавляет ответы на тестовую работу.
        /// </summary>
        /// <param name="testAnswer"></param>
        /// <returns>Возвращает идентификатор ответа в БД</returns>
        public async Task<string> AddTestAnswerAsync(TestAnswerForAdd testAnswer) =>
            await answers.TestAnswerAddAsync(Access_Token, testAnswer);

        /// <summary>
        /// Добавляет ответы на текстовую работу.
        /// </summary>
        /// <param name="textAnswer"></param>
        /// <returns>Возвращает идентификатор ответа в БД (ID записи из таблицы EexecutinOfWorks)</returns>
        public async Task<string> AddTextAnswerAsync(TextAnswerForAdd textAnswer) =>
            await answers.TextAnswerAddAsync(Access_Token, textAnswer);

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="mark">Оценка в строком типе. Теоретически можно задать до 4 символов, но зачем?</param>
        /// <param name="IDExecutionOfWork">ID ответа - ID записи из таблицы ExectionOfWork</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> AddMark(string mark, string IDExecutionOfWork) =>
            await answers.SetMark(Access_Token, mark, IDExecutionOfWork);

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="header">Заголовок ответа на работу. В нем содержатся необходимые данные для отправки оценки.</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> AddMark(AnswerHeader header) =>
            await answers.SetMark(Access_Token, header);

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="testAnswer">Ответ на тестову работу. В нем содержатся необходимые данные для отправки оценки.</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> AddMark(TestAnswer testAnswer) =>
            await answers.SetMark(Access_Token, testAnswer);

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="textAnswer">Ответ на письменную работу. В нем содержатся необходимые данные для отправки оценки.</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> AddMark(TextAnswer textAnswer) =>
            await answers.SetMark(Access_Token, textAnswer);
        #endregion

        #region Theories

        /// <summary>
        /// Возвращает теорию по заданному идентификтаору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Theory> GetTheoryByIDAsync(string id) =>
            theories.ByIdAsync(Access_Token, id);

        /// <summary>
        /// Список теории по заданному массиву ID.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<Theory>> GetTheoriesByIDsAsync(string[] ids) =>
            theories.ByIDsAsync(Access_Token, new List<string>(ids));
        /// <summary>
        /// Список теории по заданному списку ID.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<Theory>> GetTheoriesByIDsAsync(List<string> ids) =>
            theories.ByIDsAsync(Access_Token, ids);

        /// <summary>
        /// Возвращает список теории по заданному ID класса.
        /// </summary>
        /// <param name="id_class"></param>
        /// <returns></returns>
        public Task<List<Theory>> GetTheoriesByClassroomIDAsync(string id_class) =>
            theories.ByClassIdAsync(Access_Token, id_class);

        /// <summary>
        /// Добавление теории в БД.
        /// </summary>
        /// <param name="theory">Теория, которую надо добавить в БД.</param>
        /// <returns>Теория с идентификаторами самой теории и заголовка</returns>
        public Task<Theory> AddTheory(Theory theory) =>
            theories.RegAsync(Access_Token, theory);
        #endregion
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
                LoadUserDataFromFile();
                // Рекурсивно взываем себя.
                return getAccessToken();
            }
        }

        /// <summary>
        /// Сохраняет access_token и другие данные о пользователе в файл.
        /// </summary>
        public void SaveUserDataToFile(bool rewrite)
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
        public bool LoadUserDataFromFile()
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
        public static string ToJson(this TestWorkForAdd self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this TextWorkForAdd self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this TestAnswerForAdd self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this TextAnswerForAdd self) => JsonConvert.SerializeObject(self, Converter.Settings);
        
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
