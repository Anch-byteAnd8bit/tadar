using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private List<RegisteredClassroom> classrooms = new List<RegisteredClassroom>();
        private ObservableCollection<WorkAndMark> marks = new ObservableCollection<WorkAndMark>();
        public MenuViewModel()
        {
            api = nsAPI.API.Instance;
            //api.MainUser.Name
            //Оценки.
            MarkClick = new Command(mark_Click);
            // 
            AkkClick = new Command(Akk_Click);
            //
            TestClick = new Command(Test_Click);
            //
            ClassClick = new Command(Class_Click);
            // Игра.
            GameClick = new Command(Game_Click);
            // Создать работу (открокется страница создания заголовка работы).
            MaketestClick = new Command(make_test_Click);
            // выполнить работу?
            SelftestClick = new Command(selftest_Click);
            // Словарь.
            DicktClick= new Command(Dickt_Click);
            // 
            TheClick = new Command(The_Click);
            // Ассинхронная загрузка списка классов.
            LoadClasssAsync();
        }

        public async void LoadClasssAsync()
        {
            try
            {
                classrooms = await api.GetClassroomsByUserIdAsync(api.MainUser.ID);
                if (classrooms == null)
                {
                    if (api.LastException!= null && api.LastException.TypeError == TError.DefinedError)
                    {
                        if (api.LastException.CodeAPI == CODE_ERROR.ERR_ClassNotFound)
                        {
                            // У пользователя нет классов!
                            // В таком случае вместо списка классов пишем что их пока нет.
                        }
                    }
                }

                //test = new TestWork();
                //test.WorkHeader = (WorkHeader)ob;
                //test.WorkBody = works.TestWorks.SingleOrDefault(w => w.WorkHeader == test.WorkHeader).WorkBody;
                //First.Base_frame.Navigate(new Test14Page(test));

                //  ClasssList = new ObservableCollection<RegisteredClassroom>();

                
                OnPropertyChanged(nameof(Classrooms));
                LoadMarks();
            }
            catch (Exception ex)
            {
                Msg.Write(ex.Message);
            }
        }

        public async void LoadMarks()
        {
            try
            {
                if (classrooms != null)
                {
                    // Получаем массив идентификаторов классов пользовталея.
                    var classromsIDs = classrooms.Select(c => c.ID).ToArray();
                    // Получаем список работ пользователя (только заголовки).
                    var works = await api.GetWorksByClassesIDAsync(classromsIDs, true);
                    if (works != null)
                    {
                        // Получаем массив идентификаторов классов пользователя.
                        var worksIDs = works.Headers.Select(wh => wh.ID).ToArray();
                        // Получаем список ответов на работы пользователя (только заголовки).
                        var userAnswers = await api.GetAnswersByUser(api.MainUser.ID, true);
                        if (userAnswers != null)
                        {
                            // Получаем ответы только те, у которых есть оценки.
                            var resolvedAnswers = userAnswers.GetMarkedAnswers();
                            //
                            works = works.GetResolvedWorks(resolvedAnswers);
                            // Ищем ответы на тестовые задания.
                            foreach (var testAnswer in resolvedAnswers.TestAnswers)
                            {
                                foreach (var testWork in works.TestWorks)
                                {
                                    if (testAnswer.AnswerHeader.id_Work == testWork.WorkHeader.ID)
                                    {
                                        // Добавляем ответ-задание в список оценок.
                                        marks.Add(new WorkAndMark(testWork, testAnswer));
                                        OnPropertyChanged(nameof(Marks));
                                        break;
                                    }
                                }
                            }

                            // Ищем ответы на текстовые задания.
                            foreach (var textAnswer in resolvedAnswers.TextAnswers)
                            {
                                foreach (var textWork in works.TextWorks)
                                {
                                    if (textAnswer.AnswerHeader.id_Work == textWork.WorkHeader.ID)
                                    {
                                        // Добавляем ответ-задание в список оценок.
                                        marks.Add(new WorkAndMark(textWork, textAnswer));
                                        OnPropertyChanged(nameof(Marks));
                                        break;
                                    }
                                }
                            }
                        }
                        // Нет ответов у ученика.
                        else
                        {
                            if (api.LastException != null)
                            {
                                Msg.Write(api.LastException.Message);
                            }
                        }
                    }
                    else
                    {
                        if (api.LastException != null)
                        {
                            Msg.Write(api.LastException.Message);
                        }
                    }
                }
                OnPropertyChanged(nameof(Marks));
            }
            catch (Exception ex)
            {
                Msg.Write(ex.Message);
            }
        }

        public List<RegisteredClassroom> Classrooms
        {
            get { return classrooms; }
            set
            {
                classrooms = value;
                OnPropertyChanged(nameof(Classrooms));
                // Задаем новый выбранный элемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }

        /// <summary>
        /// Список оценок пользователя.
        /// </summary>
        public ObservableCollection<WorkAndMark> Marks
        {
            get { return marks; }
            //set
            //{
            //    marks = value;
            //    OnPropertyChanged(nameof(Marks));
            //    // Задаем новый выбранный элемент из списка.
            //    // SelectedMark = marks[0];
            //}
        }

        private void mark_Click(object ob)
        {
            First.Base_frame.Navigate(new marks());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void The_Click(object ob)
        {
            First.Base_frame.Navigate(new TheoryPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void Dickt_Click(object ob)
        {
            First.Base_frame.Navigate(new DictPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void Akk_Click(object ob)
        {
           First.Base_frame.Navigate(new CheckTestPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void Test_Click(object ob)
        {
            First.Base_frame.Navigate(new ListTest_Page());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void Class_Click(object ob)
        {
            First.Base_frame.Navigate(new AddToClassPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void Game_Click(object ob)
        {
            First.Base_frame.Navigate(new Game());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void make_test_Click(object ob)
        {
            
            First.Base_frame.Navigate(new DoChoseTypePage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void selftest_Click(object ob)
        {
            First.Base_frame.Navigate(new ListNonMarkPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        //public string 
        //    fullname = api.MainUser.Surname.ToString() +
        //    api.MainUser.Name.ToString() + 
        //    api.MainUser.Middlename.ToString();
        public string Fullname
        {
            // Когда надо вернуть фамилию.
            get => api.MainUser.Surname.ToString() +" " +
            api.MainUser.Name.ToString() + " " +
            api.MainUser.Middlename.ToString();
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                Fullname = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Fullname));
            }
        }
        public Command TheClick
        {
            get;
            set;
        }
        public Command MarkClick
        {
            get;
            set;
        }
        public Command DicktClick
        {
            get;
            set;
        }
        public Command SelftestClick
        {
            get;
            set;
        }
        public Command MaketestClick
        {
            get;
            set;
        }
        public Command GameClick
        {
            get;
            set;
        }
        public Command AkkClick
        {
            get;
            set;
        }
        public Command TestClick
        {
            get;
            set;
        }
        public Command ClassClick
        {
            get;
            set;
        }
        public string Surname
        {
            // Когда надо вернуть фамилию.
            get => api.MainUser.Surname;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                api.MainUser.Surname = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Surname));
            }
        }
        public string Name
        {
            // Получить.
            get => api.MainUser.Name;
            // Задать.
            set
            {
                api.MainUser.Name = value;
                // Уведомление.
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Middlename
        {
            get => api.MainUser.Middlename;
            set
            {
                api.MainUser.Middlename = value;
                OnPropertyChanged(nameof(Middlename));
            }
        }
    }
}
