﻿using Helpers;
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
    public enum TRefbooks
    {
        Genders,
        WorkTypes,
        Roles,
        States,
        TypeWords
    }

    public class API
    {
        public Dictionary<TRefbooks, List<Refbook>> Refbooks;

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
        private readonly MDict dict;
        /// <summary>
        /// Ключ доступа к API.
        /// </summary>
        public string Access_Token { get { return api_token; } }

        private static API instance;
        /// <summary>
        /// Возвращает экземпляр API. Паттерн "одиночка".
        /// </summary>
        public static API Instance
        {
            get
            {
                if (instance != null) return instance;

                // полю instance присваивается значения внуnри самого конструктора.
                _ = new API(loadRefbooks: true, OnLoadedRefbooks: null, loadMainUser: true, OnLoadedMainUser: null);
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// Будет содержать последнюю ошибку.
        /// </summary>
        public ResponseError LastException { get; set; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="loadRefbooks">Надо ли загружать все справочники.</param>
        /// <param name="OnLoadedRefbooks">Процедура, вызываемая после загрузки справочников.</param>
        /// <param name="loadMainUser">Надо ли загружать данные о текущем пользователе (MainUser)</param>
        /// <param name="OnLoadedMainUser">Процедура, вызываемая после загрузке пользователя.</param>
        public API(bool loadRefbooks = false, Action OnLoadedRefbooks = null, bool loadMainUser = true, Action OnLoadedMainUser = null)
        {
            // Для работы с запросами касающимися пользователей.
            users = new MUsers();
            // Для работы с запросами касающимися классов.
            classrooms = new MClassrooms();
            // Для работы с запросами касающимися справочников.
            refBooks = new MRefBooks();
            // Для работы с запросами касающимися работ.
            works = new MWorks();
            // Для работы с запросами касающимися теории.
            theories = new MTheories();
            // Для работы с запросами касающимися ответов на работы.
            answers = new MAnswers();
            // Для работы с запросами касающимися словарей.
            dict = new MDict();
            // Если возможно, то загружаем данные пользователя из файла.
            try
            {
                if (File.Exists(pathAccessToken))
                {
                    LoadUserDataFromFile();
                    if (loadRefbooks) LoadRefBooks(OnLoadedRefbooks);
                    if (loadMainUser) LoadMainUser(OnLoadedMainUser);
                }
                else
                {
                    Msg.Write("При попытке считать из файла пользователя произошла ошибка:\n" +
                        "Не найден файл с данными пользователя.");
                }
            }
            catch (Exception e)
            {
                Msg.Write("При попытке считать из файла пользователя произошла ошибка: \n" + e.Message);
            }
            finally
            {
                instance = this;
            }
        }

        private readonly string cond = "both";
        /// <summary>
        /// Конструктор API.
        /// </summary>
        /// <param name="loadRefbooks">Надо ли загружать справочники.</param>
        /// <param name="loadMainUser">Надо ли загружать данные пользовтаеля.</param>
        /// <param name="cond">Условия вызова процедуры окончания загрузки и/или справочника и/или 
        /// пользователя. "both" - дождаться загрузки и справочников и пользователя и потом вызвать
        /// процедуру. "first" - дождаться загрузки первого из них - справочников или пользователя.
        /// "user" - после загрузки пользователя. "refbooks" - после загрузки справочников.</param>
        /// <param name="OnLoaded">Процедура вызываемая после загрузки и/или справочника и/или 
        /// пользователя.</param>
        public API(bool loadRefbooks = false, bool loadMainUser = true, string cond = "both", Action OnLoaded = null)
        {
            cond = cond.ToLower();
            // Для работы с запросами касающимися пользователей.
            users = new MUsers();
            // Для работы с запросами касающимися классов.
            classrooms = new MClassrooms();
            // Для работы с запросами касающимися справочников.
            refBooks = new MRefBooks();
            // Для работы с запросами касающимися работ.
            works = new MWorks();
            // Для работы с запросами касающимися теории.
            theories = new MTheories();
            // Для работы с запросами касающимися ответов на работы.
            answers = new MAnswers();
            // Для работы с запросами касающимися словарей.
            dict = new MDict();
            //
            if (loadRefbooks) LoadRefBooks(OnLoaded);
            // Если возможно, то загружаем данные пользователя из файла.
            try
            {
                if (File.Exists(pathAccessToken))
                {
                    LoadUserDataFromFile();
                    if (loadMainUser) LoadMainUser(OnLoaded);
                }
                else
                {
                    Msg.Write("При попытке считать из файла пользователя произошла ошибка:\n" +
                        "Не найден файл с данными пользователя.");
                }
            }
            catch (Exception e)
            {
                Msg.Write("При попытке считать из файла пользователя произошла ошибка: \n" + e.Message);
            }
            finally
            {
                instance = this;
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
                if (action != null)
                {
                    if ((cond == "both" && (Refbooks.Count == 5)) ||
                        (cond == "first" && (Refbooks.Count < 5)) ||
                        (cond == "user"))
                        action();
                }
            }
            catch (Exception ex)
            {
                Msg.Write(ex.Message);
            }
        }
        /// <summary>
        /// Асинхронно загружает справочники.
        /// </summary>
        /// <param name="action"></param>
        private async void LoadRefBooks(Action action = null)
        {
            try
            {
                Refbooks = new Dictionary<TRefbooks, List<Refbook>>();
                var genders = await GetGendersAsync();
                var typeWorks = await GetWorkTypesAsync();
                var roles = await GetRolesAsync();
                var states = await GetStatesAsync();
                var typeWords = await GetTypeWordsAsync();

                Refbooks.Add(TRefbooks.Genders, genders);
                Refbooks.Add(TRefbooks.WorkTypes, typeWorks);
                Refbooks.Add(TRefbooks.Roles, roles);
                Refbooks.Add(TRefbooks.States, states);
                Refbooks.Add(TRefbooks.TypeWords, typeWords);
                if (action != null)
                {
                    if ((cond == "both" && (MainUser != null)) ||
                        (cond == "first" && (MainUser == null)) ||
                        (cond == "refbooks"))
                        action();
                }
            }
            catch (Exception ex)
            {
                Msg.Write(ex.Message);
            }
        }

        #region Users
        // =========== Пользователи

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="user">Данные регистрации.</param>
        /// <returns>True - при успешной регистрации.</returns>
        public async Task<bool> UserRegAsync(UserForRegistration user)
        {
            // При успехе, будет содержать ключ доступа к серверу.
            var accessToken = await users.RegAsync(user);
            //
            if (accessToken == null)
            {
                LastException = users.Response.Exception;
                return false;
            }
            return await UserAuthAsync(user.UserForAuthorization);
        }


        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="user">Объект с данными для авторизации пользователя</param>
        /// <returns>True - при успешной авторизации</returns>
        public async Task<bool> UserAuthAsync(UserForAuthorization user)
        {
            // При успехе, будет содержать ключ доступа к серверу.
            AccessToken accessToken = await users.AuthAsync(user);
            // Если не удалось авторизоваться, то сохраняем ошибку и возвращаем False.
            if (accessToken == null)
            {
                LastException = users.Response.Exception;
                return false;
            }
            // Сохраняем токен пользователя.
            api_token = accessToken.Token;
            // Сохраняем ID пользователя.
            id_user = accessToken.id_User;
            // Сохраняем в файл с перезаписью существующего файла.
            SaveUserDataToFile(true);
            // Загружаем информацию о пользователе с сервера.
            MainUser = await GetUserByIdAsync(accessToken.id_User);
            if (MainUser == null)
            {
                LastException = users.Response.Exception;
                return false;
            }
            return true;
        }


        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login">Строка логина</param>
        /// <param name="pass">Строка пароля</param>
        public async Task<bool> UserAuthAsync(string login, string pass)
        {
            return await UserAuthAsync(new UserForAuthorization
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
        public async Task<List<RegisteredUser>> FindUsersAsync(SettingsFind settingsFind = null,  string searchName = null, string searchMName = null, string searchSurname = null)
        {
            // Пробуем найти пользователей.
            var usrs = await users.FindAsync(api_token, settingsFind, searchName, searchMName, searchSurname);
            if (usrs == null)
            {
                LastException = users.Response.Exception;
            }
            return usrs;
        }

        /// <summary>
        /// Получает подробную информацию о пользоателях с заданными ID.
        /// </summary>
        /// <param name="userIds">ID пользователей.</param>
        /// <returns>Информация о пользователях.</returns>
        public async Task<List<RegisteredUser>> GetUsersByIdAsync(string[] userIds)
        {
            var regu = await users.ByIdAsync(api_token, userIds);
            if (regu == null)
            {
                LastException = users.Response.Exception;
            }
            return regu;
        }

        /// <summary>
        /// Получение информации о пользователе с заданным ID
        /// </summary>
        public async Task<RegisteredUser> GetUserByIdAsync(string userId)
        {
            var regu = await users.ByIdAsync(api_token, userId);
            if (regu == null)
            {
                LastException = users.Response.Exception;
            }
            return regu;
        }

        /// <summary>
        /// Возвращает список пользователей из указанного класса.
        /// </summary>
        /// <param name="api_token"></param>
        /// <param name="id_Class">Идентификатор класса.</param>
        /// <returns>Список пользователей из указанного класса</returns>
        public async Task<List<RegisteredUser>> GetUsersByClassIdAsync(string id_Class)
        {
            var regu = await users.ByClassIdAsync(Access_Token, id_Class);
            if (regu == null)
            {
                LastException = users.Response.Exception;
            }
            return regu;
        }
        #endregion

        #region Refbooks
        // =========== "Справочники"

        /// <summary>
        /// Запрашивает у сервера список полов.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Refbook>> GetGendersAsync()
        {
            var lg = await refBooks.GetListGendersAsync();
            if (lg==null)
            {
                LastException = refBooks.Response.Exception;
            }
            return lg;
        }
        /// <summary>
        /// Запрашивает у сервера список типов работ.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Refbook>> GetWorkTypesAsync()
        {
            var lwt = await refBooks.GetListWorkTypesAsync();
            if (lwt == null)
            {
                LastException = refBooks.Response.Exception;
            }
            return lwt;
        }
        /// <summary>
        /// Запрашивает у сервера список ролей пользователей.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Refbook>> GetRolesAsync()
        {
            var lr = await refBooks.GetListRolesAsync();
            if (lr == null)
            {
                LastException = refBooks.Response.Exception;
            }
            return lr;
        }
        /// <summary>
        /// Запрашивает у сервера список состояний аккаунтов.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Refbook>> GetStatesAsync()
        {
            var ls = await refBooks.GetListStatesAsync();
            if (ls==null)
            {
                LastException = refBooks.Response.Exception;
            }
            return ls;
        }
        /// <summary>
        /// Запрашивает у сервера список типов слов.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Refbook>> GetTypeWordsAsync()
        {
            var ltw = await refBooks.GetListTypeWordsAsync();
            if (ltw == null)
            {
                LastException = refBooks.Response.Exception;
            }
            return ltw;
        }

        /// <summary>
        /// Возвращает все данные из указанного справочника.
        /// </summary>
        /// <param name="nameRefbook">Наименование справочника маленькими буквами.</param>
        /// <returns>Все данные из указанного справочника.</returns>
        public async Task<List<Refbook>> GetDataFromRefbook(string nameRefbook)
        {
            var ad =await refBooks.GetAllDataAsync(nameRefbook);
            if (ad== null)
            {
                LastException = refBooks.Response.Exception;
            }
            return ad;
        }

        /// <summary>
        /// Добавляет данные в указанный справочник.
        /// </summary>
        /// <param name="refbook">Наименование справочника (таблицы в БД)</param>
        /// <param name="names">Данные.</param>
        /// <returns>Ничего.</returns>
        public async Task<bool> AddDataToRefbook(string refbook, string[] names)
        {
            var dtr = await refBooks.AddDataToRefbook(Access_Token, refbook, new List<string>(names));
            if (!dtr)
            {
                LastException = refBooks.Response.Exception;
            }
            return dtr;
        }

        /// <summary>
        /// Добавляет данные в указанный справочник.
        /// </summary>
        /// <param name="refbook">Наименование справочника (таблицы в БД)</param>
        /// <param name="names">Данные.</param>
        /// <returns>Ничего.</returns>
        public async Task<bool> AddDataToRefbook(string refbook, List<string> names)
        {
            var dtr = await refBooks.AddDataToRefbook(Access_Token, refbook, names);

            if (!dtr)
            {
                LastException = refBooks.Response.Exception;
            }
            return dtr;
        }
        #endregion

        #region Classes
        // =========== Классы


        /// <summary>
        /// Создание нового класа.
        /// </summary>
        /// <param name="classroom"></param>
        /// <returns></returns>
        public async Task<RegisteredClassroom> AddClassroomAsync(ClassroomForReg classroom)
        {
            var cl = await classrooms.RegAsync(api_token, classroom);
            if (cl == null)
            {
                LastException = classrooms.Response.Exception;
            }
            return cl;

        }

        /// <summary>
        /// Получение информации о классах с заданными ID
        /// </summary>
        public async Task<List<RegisteredClassroom>> GetClassroomsByIdAsync(string[] classroomIds)
        {
            var cl = await classrooms.ByIdAsync(api_token, classroomIds);
            if (cl == null)
            {
                LastException = classrooms.Response.Exception;
            }
            return cl;

        }

        /// <summary>
        /// Получение информации о классе с заданными ID
        /// </summary>
        public async Task<RegisteredClassroom> GetClassroomByIdAsync(string classroomId) {
            var cl = await classrooms.ByIdAsync(api_token, classroomId);
            if (cl == null)
            {
                LastException = classrooms.Response.Exception;
            }
            return cl;
        }

        /// <summary>
        /// Получение информации о классах в которых состоит пользователь с заданным id.
        /// </summary>
        /// <param name="userId">ИД юзера.</param>
        /// <param name="roleId">Роль юзера в этих классах</param>
        /// <returns>Список классов.</returns>
        public async Task<List<RegisteredClassroom>> GetClassroomsByUserIdAsync(
            string userId, string roleId = null) {
            var cl = await classrooms.ByUserIdAsync(api_token, userId, roleId);
            if (cl == null)
            {
                LastException = classrooms.Response.Exception;
            }
            return cl;
        }

        /// <summary>
        /// Возвращает список всех классов в соответствии с параметром поиска.
        /// </summary>
        /// <param name="settingsFind">Параметр поиска.</param>
        /// <returns></returns>
        public async Task<List<RegisteredClassroom>> GetAllClassess(SettingsFind settingsFind = null)
        {
            var cl = await classrooms.GetAllAsync(Access_Token, settingsFind);
            if (cl == null)
            {
                LastException = classrooms.Response.Exception;
            }
            return cl;
        }

        /// <summary>
        /// Добавляет пользовтаеля в класс в роли ученика.
        /// </summary>
        /// <param name="id_user">Идентификтаор пользователя.</param>
        /// <param name="id_class">Идентификтаор класса.</param>
        /// <returns>True - если добавление прошло без проблем.</returns>
        public async Task<bool> AddStudent(string id_user, string id_class)
        {
            var cl = await classrooms.AddStudent(Access_Token, id_user, id_class);
            if (!cl)
            {
                LastException = classrooms.Response.Exception;
            }
            return cl;
        }

        #endregion

        #region Works
        // =========== Тесты
        /// <summary>
        /// Возвращает список работ по заданным идентификаторам классов.
        /// </summary>
        /// <param name="classesID">Массив строк идентификаторов классов.</param>
        /// <param name="onlyHeaders">Только заголовки работ.</param>
        /// <returns>Список работ.</returns>
        public async Task<Works> GetWorksByClassesIDAsync(string[] classesID, bool onlyHeaders = true)
        {
            var w = await works.ByClassesIdsAsync(Access_Token, classesID, onlyHeaders);
            if (w == null)
            {
                LastException = works.Response.Exception;
            }
            return w;
        }

        /// <summary>
        /// Возвращает список работ по заданным классам.
        /// </summary>
        /// <param name="classes">Список классов.</param>
        /// <param name="onlyHeaders">Только заголовки работ.</param>
        /// <returns>Список работ.</returns>
        public async Task<Works> GetWorksByClassesAsync(List<RegisteredClassroom> classes, bool onlyHeaders = true)
        {
            var w = await works.ByClassesAsync(Access_Token, classes, onlyHeaders);
            if (w == null)
            {
                LastException = works.Response.Exception;
            }
            return w;
        }

        /// <summary>
        /// Возвращает работу по заданному идентификатору класса.
        /// </summary>
        /// <param name="classID">Строка идентификатора класса.</param>
        /// <param name="onlyHeaders">Только заголовок работы.</param>
        /// <returns>Работа.</returns>
        public async Task<Works> GetWorksByClassIDAsync(string classID, bool onlyHeaders = true)
        {
            var w =await works.ByClassIDAsync(Access_Token, classID, onlyHeaders);
            if (w == null)
            {
                LastException = works.Response.Exception;
            }
            return w;
        }

        /// <summary>
        /// Добавляет в БД тестовую новую работу.
        /// </summary>
        /// <param name="testWork">Работа для записи в БД.</param>
        /// <returns>Идентификатор добавленной работы в строке</returns>
        public async Task<string> AddTestWorkAsync(TestWork testWork)
        {
            var w =await works.TestWorkAddAsync(Access_Token, testWork);
            if (w == null)
            {
                LastException = works.Response.Exception;
            }
            return w;
        }

        /// <summary>
        /// Добавляет в БД новую письменную работу.
        /// </summary>
        /// <param name="testWork">Работа для записи в БД.</param>
        /// <returns>Идентификатор добавленной работы в строке</returns>
        public async Task<string> AddTextWorkAsync(TextWork textWork)
        {
            var w= await works.TextWorkAddAsync(Access_Token, textWork);
            if (w == null)
            {
                LastException = works.Response.Exception;
            }
            return w;
        }
        #endregion

        #region Answers
        // =========== Ответы

        /// <summary>
        /// Возвращает список ответов по заданным ИД работ.
        /// </summary>
        /// <param name="worksID"></param>
        /// <param name="onlyHeaders"></param>
        /// <returns></returns>
        public async Task<Answers> GetAnswersByWorks(string[] worksID, bool onlyHeaders = true)
        {
            var a = await answers.ByWorksIdsAsync(Access_Token, worksID, onlyHeaders);
            if (a == null)
            {
                LastException = answers.Response.Exception;
            }
            return a;
        }

        /// <summary>
        /// Возвращает список ответов по заданному ИД работы.
        /// </summary>
        /// <param name="workID"></param>
        /// <param name="onlyHeaders"></param>
        /// <returns></returns>
        public async Task<Answers> GetAnswersByWork(string workID, bool onlyHeaders = true)
        {
            var a =await answers.ByWorkIdAsync(Access_Token, workID, onlyHeaders);
            if (a == null)
            {
                LastException = answers.Response.Exception;
            }
            return a;
        }

        /// <summary>
        /// Добавляет ответы на тестовую работу.
        /// </summary>
        /// <param name="testAnswer"></param>
        /// <returns>Возвращает идентификатор ответа в БД</returns>
        public async Task<string> AddTestAnswerAsync(TestAnswerForAdd testAnswer)
        {
            var a =await answers.TestAnswerAddAsync(Access_Token, testAnswer);
            if (a == null)
            {
                LastException = answers.Response.Exception;
            }
            return a;
        }

        /// <summary>
        /// Добавляет ответы на текстовую работу.
        /// </summary>
        /// <param name="textAnswer"></param>
        /// <returns>Возвращает идентификатор ответа в БД (ID записи из таблицы EexecutinOfWorks)</returns>
        public async Task<string> AddTextAnswerAsync(TextAnswerForAdd textAnswer) {
            var a = await answers.TextAnswerAddAsync(Access_Token, textAnswer);
            if (a == null)
            {
                LastException = answers.Response.Exception;
            }
            return a;
        }

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="mark">Оценка в строком типе. Теоретически можно задать до 4 символов, но зачем?</param>
        /// <param name="IDExecutionOfWork">ID ответа - ID записи из таблицы ExectionOfWork</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> AddMark(string mark, string IDExecutionOfWork)
        {
            var a =await answers.SetMark(Access_Token, mark, IDExecutionOfWork);
            if (!a)
            {
                LastException = answers.Response.Exception;
            }
            return a;
        }

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="header">Заголовок ответа на работу. В нем содержатся необходимые данные для отправки оценки.</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> AddMark(AnswerHeader header)
        {
            var m = await answers.SetMark(Access_Token, header);
            if (!m)
            {
                LastException = answers.Response.Exception;
            }
            return m;
        }

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="testAnswer">Ответ на тестову работу. В нем содержатся необходимые данные для отправки оценки.</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> AddMark(TestAnswer testAnswer)
        {
            var a = await answers.SetMark(Access_Token, testAnswer);
            if (!a)
            {
                LastException = answers.Response.Exception;
            }
            return a;
        }

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="textAnswer">Ответ на письменную работу. В нем содержатся необходимые данные для отправки оценки.</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> AddMark(TextAnswer textAnswer)
        {
            var a = await answers.SetMark(Access_Token, textAnswer);
            if (!a)
            {
                LastException = answers.Response.Exception;
            }
            return a;
        }
        #endregion

        #region Theories

        /// <summary>
        /// Возвращает теорию по заданному идентификтаору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Theory> GetTheoryByIDAsync(string id)
        {
            var t = theories.ByIdAsync(Access_Token, id);
            if (t == null)
            {
                LastException = theories.Response.Exception;
            }
            return t;
        }

        /// <summary>
        /// Список теории по заданному массиву ID.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<Theory>> GetTheoriesByIDsAsync(string[] ids)
        {
            var t = theories.ByIDsAsync(Access_Token, new List<string>(ids));
            if (t == null)
            {
                LastException = theories.Response.Exception;
            }
            return t;
        }
        /// <summary>
        /// Список теории по заданному списку ID.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<Theory>> GetTheoriesByIDsAsync(List<string> ids) {
            var t = theories.ByIDsAsync(Access_Token, ids);
            if (t == null)
            {
                LastException = theories.Response.Exception;
            }
            return t;
        }

        /// <summary>
        /// Возвращает список теории по заданному ID класса.
        /// </summary>
        /// <param name="id_class"></param>
        /// <returns></returns>
        public Task<List<Theory>> GetTheoriesByClassroomIDAsync(string id_class) {
            var t = theories.ByClassIdAsync(Access_Token, id_class);
            if (t == null)
            {
                LastException = theories.Response.Exception;
            }
            return t;
        }

        /// <summary>
        /// Добавление теории в БД.
        /// </summary>
        /// <param name="theory">Теория, которую надо добавить в БД.</param>
        /// <returns>Теория с идентификаторами самой теории и заголовка</returns>
        public Task<Theory> AddTheory(Theory theory) {
            var t = theories.RegAsync(Access_Token, theory);
            if (t == null)
            {
                LastException = theories.Response.Exception;
            }
            return t;
        }
        #endregion

        #region Dict

        /// <summary>
        /// Получение пользовательского словаря.
        /// </summary>
        /// <param name="id_User">Идентификатор пользовтаеля.</param>
        /// <param name="settingsFind">Настройки отбора. Можно не задвать.</param>
        /// <returns>Список слов из пользователского словаря.</returns>
        public async Task<List<Word>> GetUserWords(string id_User, SettingsFind settingsFind = null)
        {
            var d = await dict.GetUserAsync(Access_Token, id_User, settingsFind);
            if (d == null)
            {
                LastException = dict.Response.Exception;
            }
            return d;
        }


        /// <summary>
        /// Получение общего словаря.
        /// </summary>
        /// <param name="settingsFind">Настройки отбора. Можно не задвать.</param>
        /// <returns>Список слов, не принадлежащих пользотваелям.</returns>
        public async Task<List<Word>> GetCommonWords(SettingsFind settingsFind = null)
        {
            var d = await dict.GetCommonAsync(Access_Token, settingsFind);
            if (d == null)
            {
                LastException = dict.Response.Exception;
            }
            return d;
        }

        /// <summary>
        /// Получение списка слов пользователя и слов из общего словаря.
        /// </summary>
        /// <param name="id_User">Идентификатор пользовтаеля.</param>
        /// <param name="settingsFind">Настройки отбора. Можно не задвать.</param>
        /// <returns>Список слов пользователя и слов из общего словаря.</returns>
        public async Task<List<Word>> GetCombiWords(string id_User, SettingsFind settingsFind = null) {
            var d = await dict.GetCombinedAsync(Access_Token, id_User, settingsFind);
            if (d == null)
            {
                LastException = dict.Response.Exception;
            }
            return d;
        }

        /// <summary>
        /// Добавляет слово в БД.
        /// </summary>
        /// <param name="word">Слово для добавления.</param>
        /// <returns>True - если успешно добавлено.</returns>
        public async Task<bool> AddWordAsync(Word word)
        {
            var d = await dict.AddWordAsync(Access_Token, word);
            if (!d)
            {
                LastException = dict.Response.Exception;
            }
            return d;
        }


        /// <summary>
        /// Добавляет слова в БД.
        /// </summary>
        /// <param name="word">Слова для добавления.</param>
        /// <returns>True - если успешно добавлены.</returns>
        public async Task<bool> AddWordsAsync(List<Word> words)
        {
            var d = await dict.AddWordsAsync(Access_Token, words);
            if (!d)
            {
                LastException = dict.Response.Exception;
            }
            return d;
        }

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
