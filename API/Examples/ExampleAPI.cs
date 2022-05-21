using Helpers;
using nsAPI.Entities;
using nsAPI.Helpers;
using nsAPI.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nsAPI.Examples
{
    public class ExampleAPI
    {
        private readonly nsAPI.API api;
        private RegisteredClassroom curClassroom;


        public ExampleAPI()
        {
            api = API.Instance;
        }

        /// <summary>
        /// Пример регистрации пользователя.
        /// </summary>
        public async Task UserRegAsync()
        {
            if (api.MainUser != null) throw new Exception("Main User already is exist");
            // Создать объект класса UserForRegistration, в котором хранятся
            // регистрационные данные о пользователе.
            UserForRegistration userForReg = new UserForRegistration
            {
                Login = "Alex",
                Email = "alex903_90@mail.ru",
                Pass = "123",
                BDate = "1990.10.31",
                GenderID = "1",
                Middlename = "Леонидович",
                Name = "Александр",
                Surname = "Рогачёв"
            };
            // Вызвать метод регистрации, передав в параметрах пользователя.
            if (await api.UserRegAsync(userForReg))
            {
                Msg.Write("OK");
            }
            else
            {
                if (api.LastException.TypeError == TError.DefinedError)
                {
                    if (api.LastException.CodeAPI == CODE_ERROR.ERR_UserAlreadyReg)
                    {
                        Msg.Write("Такой пользователь уже зарегистрирован!");
                    }
                }
            }
            // После регистрации автоматических происходит авторизация, а данные
            // авторизации сохраняются на диске и потом при созданиие объекта API
            // загружаеются. Таким образом авторизация сохраняется, даже при выключении
            // компа.
        }

        /// <summary>
        /// Пример авторизации.
        /// </summary>
        public async Task UserAuthAsync()
        {
            //
            UserForAuthorization userForAuthorization = new UserForAuthorization
            {
                Pass = "123",
                Login = "Alex"
            };
            if (api.MainUser == null) await api.UserAuthAsync(userForAuthorization);
        }

        /// <summary>
        /// Пример создания класса.
        /// </summary>
        /// <returns></returns>
        public async Task CreateClass()
        {
            try
            {

                ClassroomForReg classroomForReg = new ClassroomForReg
                {
                    Name = "Класс номер 1",
                    Description = "Описание класса номер 1",
                    id_User = api.MainUser.ID
                };
                // Получаем объект зарегистрированного класса.
                curClassroom = await api.AddClassroomAsync(classroomForReg);
            }
            catch (Exception ex)
            {
                Msg.Write(ex.Message);
            }
        }

        /// <summary>
        /// Пример добавления пользователя в класс.
        /// </summary>
        public async Task AddUserInClassroom()
        {
            bool result = await api.AddStudent(api.MainUser.ID, curClassroom.ID);
            if (result) Msg.Write("Пользователь добавлен в класс!");
            else Msg.Write("Не удалось добавить пользователя в класс!");
        }

        /// <summary>
        /// ПРимер создания тестовой работы.
        /// </summary>
        public async Task AddTestWork()
        {
            TestWork testWorkForAdd = new TestWork
            {
                WorkHeader = new WorkHeader
                {
                    
                    //id_TypeWork = "1", // можно не задавать.
                    DateTimeCreate = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    DateTimeStart = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    Description = "Описанеи тестовой работы номер 1",
                    Name = "Тестовая работа номер 1",
                    MaxDuration = "35",
                    IsNonMark = "0",
                    id_Class = curClassroom.ID,
                    //Id <- не надо заполнять.
                },
                WorkBody = new List<TestTask>
                {
                    new TestTask
                    {
                        //Id <- не надо заполнять.
                        //IdTest <- не надо заполнять.
                        NumTask = "1", // Номер задачи в работе.
                        PossibleAnsw1 = "A",
                        PossibleAnsw2 = "B",
                        PossibleAnsw3 = "C",
                        PossibleAnsw4 = "D",
                        RightNum = "1",
                        Word = "SomeWord",
                    },
                    new TestTask
                    {
                        //Id <- не надо заполнять.
                        //IdTest <- не надо заполнять.
                        NumTask = "2", // Номер задачи в работе.
                        PossibleAnsw1 = "халас",
                        PossibleAnsw2 = "изен",
                        PossibleAnsw3 = "пулзен",
                        PossibleAnsw4 = "пулан",
                        RightNum = "1",
                        Word = "хлеб",
                    }
                }
            };
            // В результате получем идентификатор работы.
            string TestWorkID = await api.AddTestWorkAsync(testWorkForAdd);
        }

        /// <summary>
        /// ПРимер создания письменной работы.
        /// </summary>
        public async Task AddTextWork()
        {
            TextWork textWorkForAdd = new TextWork
            {
                WorkHeader = new WorkHeader
                {
                    //id_TypeWork = "2", <- не надо заполнять..
                    DateTimeCreate = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    DateTimeStart = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    Description = "Описанеи текстовой работы номер 1",
                    Name = "Текстовая работа номер 1",
                    MaxDuration = "35",
                    IsNonMark = "0",
                    id_Class = curClassroom.ID,
                    //Id <- не надо заполнять.
                },
                WorkBody = new List<TextTask>
                {
                    new TextTask
                    {
                        //Id <- не надо заполнять.
                        //IdTest <- не надо заполнять.
                        TaskText ="Текст задания",
                        TaskTitle ="Заголовок задания",
                    },
                }
            };
            // В результате получем идентификатор работы.
            string TextWorkID = await api.AddTextWorkAsync(textWorkForAdd);
        }

        /// <summary>
        /// Пример создания и отправки ответа на тетсовую работу.
        /// </summary>
        public async Task AddAnswerTestWork()
        {
            TestAnswerForAdd testAnswer = new TestAnswerForAdd
            {
                AnswerHeader = new AnswerHeader
                {
                    //ID
                    //id_TypeWork = "1",  <- не надо заполнять.
                    id_Work = "1", // ИД работы.
                    DateTimeS = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    DateTimeE = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    id_UserInClasses = api.MainUser.ID,
                },
                AnswerBody = new List<TestAnswerBody>
                {
                    new TestAnswerBody
                    {
                        //ID
                        //id_ExecutionOfWork
                        id_Task = "1", // ИД задачи (хранятся в списке тела работы.
                        num_Answ = "1", // Номер выбранного варианта
                    },
                    new TestAnswerBody
                    {
                        //ID
                        //id_ExecutionOfWork
                        id_Task = "2", // ИД задачи (хранятся в списке тела работы.
                        num_Answ = "4", // Номер выбранного варианта
                    },
                }
            };

            // Получаем ID ответа на тестовую работу.
            string answerTestID = await api.AddTestAnswerAsync(testAnswer);
        }


        /// <summary>
        /// Пример создания и отправки ответа на текстовую работу.
        /// </summary>
        public async Task AddAnswerTextWork()
        {
            TextAnswerForAdd textAnswer = new TextAnswerForAdd
            {
                AnswerHeader = new AnswerHeader
                {
                    //ID
                    //id_TypeWork = "2", <- не надо заполнять.
                    id_Work = "1", // ИД работы.
                    DateTimeS = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    DateTimeE = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    id_UserInClasses = api.MainUser.ID,
                },
                AnswerBody = new List<TextAnswerBody>
                {
                    new TextAnswerBody
                    {
                        //ID
                        //id_ExecutionOfWork
                        id_Task = "1", // ИД задачи (хранятся в списке тела работы.
                        AnswText = "Ответ на текстовую работу"
                    },
                }
            };

            // Получаем ID ответа на текстовую работу.
            string answerTextID = await api.AddTextAnswerAsync(textAnswer);
        }

        public async Task GetListOfClasrooms()
        {
            SettingsFind settingsFind = new SettingsFind
            {
                Shift = 7, // + 1 = 8
                Count = 8
            };
            List<RegisteredClassroom> classrooms = await api.GetAllClassess(settingsFind);
            Msg.Write(classrooms.Count.ToString());
        }

        public async Task GetClassroomByIdAsync()
        {
            RegisteredClassroom classroom = await api.GetClassroomByIdAsync("1");
            Msg.Write(classroom?.DateTimeCreate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task GetClassroomByUserIdAsync()
        {
            List<RegisteredClassroom> classrooms = await api.GetClassroomsByUserIdAsync("1");
            if (classrooms != null)
            {
                Msg.Write(classrooms?.Count.ToString());
            }
            else
            {
                var excp = api.LastException;

            }
        }

        /// <summary>
        /// Добавляет данные в справочник.
        /// </summary>
        /// <returns></returns>
        public async Task AddDataToRefBook()
        {
            await api.AddDataToRefbook("typewords", new string[]{"qwe", "asd"});
        }

        /// <summary>
        /// Получение списка работ по заданным ИД классов.
        /// </summary>
        /// <returns></returns>
        public async Task GetListOfWorksByClassesIDsAsync()
        {
            Works works = await api.GetWorksByClassesIDAsync(new string[] { "10", "16" }, false);
            Msg.Write(works.TestWorks.Count.ToString() + " and " + works.TextWorks.Count.ToString());
        }

        /// <summary>
        /// Получение списка работ в, заданном по ИД, классе.
        /// </summary>
        /// <returns></returns>
        public async Task GetListOfWorksByClassIdAsync()
        {
            Works works = await api.GetWorksByClassIDAsync("10");
            Msg.Write(works.TestWorks.Count.ToString() + " and " + works.TextWorks.Count.ToString());
        }

        /// <summary>
        /// Установка оценки за работу.
        /// </summary>
        /// <returns></returns>
        public async Task AddMark()
        {

            await api.AddMark("123","1");
        }

        public async Task AddTheory()
        {
            Theory theory = new Theory
            {
                //ID
                id_Class = curClassroom.ID,
                Source = null,
                Topic = "Заголовок теории №1",
                Content = "Теория №1",
            };

            theory = await api.AddTheoryAsync(theory);
            Msg.Write(theory.ID);
        }

        /// <summary>
        /// Получение теории по её ID.
        /// </summary>
        /// <returns></returns>
        public async Task GetTheoryByIDAsync()
        {
            Theory theory = await api.GetTheoryByIDAsync("1");
            Msg.Write(theory.ID);
        }

        /// <summary>
        /// Получение теории по ID класса, в окторой она содержится.
        /// </summary>
        /// <returns></returns>
        public async Task GetTheoriesByClassroomIDAsync()
        {
            List<Theory> theories = await api.GetTheoriesByClassroomIDAsync("38");
            Msg.Write(theories.Count.ToString());
        }

        /// <summary>
        /// Получение пользовательского словаря.
        /// </summary>
        public async Task GetListUserWords()
        {
            SettingsFind set = new SettingsFind { Shift = 0, Count = 5 };
            List<Word> words = await api.GetUserWords("1", set);
            Msg.Write(string.Join(" ", words.Select(word => word.RusWord)));
        }
         
        /// <summary>
        /// Получение общего словаря.
        /// </summary>
        public async Task GetListCommonWords()
        {
            SettingsFind set = new SettingsFind { Shift = 0, Count = 5 };
            List<Word> words = await api.GetCommonWords(set);
            Msg.Write(string.Join(" ", words.Select(word => word.RusWord)));
        }

        /// <summary>
        /// Получение комбинированного словаря.
        /// </summary>
        public async Task GetListCombiWords()
        {
            SettingsFind set = new SettingsFind { Shift = 0, Count = 5 };
            List<Word> words = await api.GetCombiWords("3", set);
            Msg.Write(string.Join(" ", words.Select(word => word.RusWord)));
        }

        /// <summary>
        /// Добавить слово привязанное к пользователю.
        /// </summary>
        /// <returns></returns>
        public async Task AddUserWord()
        {
            Word word = new Word
            {
                RusWord = "РусСлово1",
                HakWord = "ХакСлово1",
                id_TypeWord = "1", // Список можно получить через api.GetTypeWordsAsync()
                id_User = "1"
            };
            if (await api.AddWordAsync(word))
            {
                Msg.Write("OK");
            }
            else
            {
                Msg.Write("Not OK");
            }
        }

        /// <summary>
        /// Добавить слово в общий список.
        /// </summary>
        /// <returns></returns>
        public async Task AddCommonWord()
        {
            Word word = new Word
            {
                RusWord = "РусСлово2",
                HakWord = "ХакСлово2",
                id_TypeWord = "1", // Список можно получить через api.GetTypeWordsAsync()
                id_User = null // <- не надо задавать, чтобы добавить в общий список.
            };
            if (await api.AddWordAsync(word))
            {
                Msg.Write("OK");
            }
            else
            {
                Msg.Write("Not OK");
            }
        }

        public async Task GetUsersByClass()
        {
            var user = await api.GetUsersByClassIdAsync("1");
            if (user != null)
            {
                Msg.Write(user.Count.ToString());
            }
            else
            {
                Msg.Write(api.LastException.Message);
                //Msg.Write("Not OK");
            }
            
        }


        public async Task GetUsersByAnswersAsync()
        {
            var users = await api.GetUsersByStudentsIDsAsync(new List<string> { "1" });
            if (users != null)
            {
                Msg.Write(users.Count.ToString());
            }
            else
            {
                Msg.Write(api.LastException.Message);
            }

        }
        public async Task GetUserByAnswerAsync()
        {
            var user = await api.GetUserByStudentIDAsync("1");
            if (user != null)
            {
                Msg.Write(user.Surname);
            }
            else
            {
                Msg.Write(api.LastException.Message);
            }

        }
    }
}
