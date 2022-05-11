using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nsAPI.Entities;
using nsAPI.Methods;
using nsAPI;
using System.Windows;

namespace nsAPI.Examples
{
    public class ExampleAPI
    {
        private readonly API api;
        private RegisteredClassroom curClassroom;

        public ExampleAPI()
        {
            api = new API();
        }

        /// <summary>
        /// Пример регистрации пользователя.
        /// </summary>
        public async Task UserRegAsync()
        {
            if (api.MainUser != null) throw new Exception("User already if reg");
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
            await api.UserRegAsync(userForReg);
            // После регистрации автоматических происходить авторизация, а данные
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
            ClassroomForReg classroomForReg = new ClassroomForReg
            {
                Name = "Класс номер 1",
                Description = "Описание класса номер 1",
                id_User = api.MainUser.ID,
                Journal = new JournalForReg
                {
                    Name = "Наименование журнала класса номер 1",
                    Description = "Описание журнала класса номер 1"
                }
            };
            // Получаем объект зарегистрированного класса.
            curClassroom = await api.AddClassroomAsync(classroomForReg);
        }

        /// <summary>
        /// Пример добавления пользователя в класс.
        /// </summary>
        public async Task AddUserInClassroom()
        {
            bool result = await api.AddStudent(api.MainUser.ID, curClassroom.ID);
            if (result) _ = MessageBox.Show("Пользователь добавлен в класс!");
            else _ = MessageBox.Show("Не удалось добавить пользователя в класс!");
        }

        /// <summary>
        /// ПРимер создания тестовой работы.
        /// </summary>
        public async Task AddTestWork()
        {
            TestWorkForAdd testWorkForAdd = new TestWorkForAdd
            {
                WorkHeader = new WorkHeader
                {
                    // в тесте всегда 1
                    IdTypeWork = "1",
                    DateTimeCreate = DateTime.Now.ToString(),
                    DateTimeStart = DateTime.Now.ToString(),
                    Description = "Описанеи тестовой работы номер 1",
                    Name = "Тестовая работа номер 1",
                    MaxDuration = "35",
                    IsNonMark = "0",
                    IdJournal = curClassroom.id_Journal,
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
            TextWorkForAdd textWorkForAdd = new TextWorkForAdd
            {
                WorkHeader = new WorkHeader
                {
                    // в тексте всегда 2
                    IdTypeWork = "2",
                    DateTimeCreate = DateTime.Now.ToString(),
                    DateTimeStart = DateTime.Now.ToString(),
                    Description = "Описанеи текстовой работы номер 1",
                    Name = "Текстовая работа номер 1",
                    MaxDuration = "35",
                    IsNonMark = "0",
                    IdJournal = curClassroom.id_Journal,
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
                    id_TypeWork = "1", // Для теста всегда 1
                    id_RecordInJ = "1", // ИД работы.
                    DateTimeS = DateTime.Now.ToString(""),
                    DateTimeE = DateTime.Now.ToString(""),
                    id_Student = api.MainUser.ID,
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
                    id_TypeWork = "1", // Для текста всегда 1
                    id_RecordInJ = "1", // ИД работы.
                    DateTimeS = DateTime.Now.ToString(""),
                    DateTimeE = DateTime.Now.ToString(""),
                    id_Student = api.MainUser.ID,
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
            MessageBox.Show(classrooms.Count.ToString());
        }

        public async Task GetClassroomByIdAsync()
        {
            RegisteredClassroom classroom = await api.GetClassroomByIdAsync("1");
            _ = MessageBox.Show(classroom.DateCreate);
        }

    }
}
