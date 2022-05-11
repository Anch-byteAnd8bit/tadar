using Helpers;
using nsAPI;
using nsAPI.Entities;
using nsAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    public class Do14ViewModel : BaseViewModel
    {
        private TestWorkForAdd test;
       // private List<RegisteredClassroom> classrooms;
     //   private RegisteredClassroom selectedclassroom;

        //public List<RegisteredClassroom> Classrooms
        //{
        //    get { return classrooms; }
        //    set {
        //        classrooms = value;
        //        OnPropertyChanged(nameof(Classrooms));
        //        // Задаем новый выбранный жлемент из списка.
        //        SelectedClassroom = Classrooms[0];
        //    }
        //}

        //public RegisteredClassroom SelectedClassroom
        //{
        //    get
        //    {
        //        // Ищем в списке полов, объект, у которого свойства ID совпдает с
        //        // со свйоством GenderID у регистрируемого пользователя.
        //        // Если такой элемент в списке не найден, то возвращаем первый элемент
        //        // из списка.
        //        return
        //             classrooms.SingleOrDefault(el => el == selectedclassroom) ?? classrooms[0];

        //    }
        //    set
        //    {
        //        // Если есть элемент в списке полов, который равен задавемому элементу...
        //        if (Classrooms.Exists(el => el == value))
        //        {
        //            // ...присваиваем его ID полу регистрируемого пользователя.
        //            selectedclassroom = value;
        //        }
        //        // Иначе, полу регистрируемого пользователя
        //        // присваиваем ID первого элемент списка.
        //        else selectedclassroom = Classrooms[0];
        //        // Уведомляем интфрейс о том, что это свйоство было изменено.
        //        OnPropertyChanged(nameof(Classrooms));
        //    }
        //}

        public Do14ViewModel(bool nonmark, string nametest, string desctest, RegisteredClassroom classroom)
        {
            string mark;
            if (nonmark)
            {
                mark = "1";
            }
            else mark = "0";

            test = new TestWorkForAdd();
            test.WorkHeader = new WorkHeader
            {
                Description = desctest,
                IsNonMark = mark,
                Name = nametest,
                id_Class = classroom.ID,
                //id_TypeWork = "1",
                DateTimeCreate = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                DateTimeStart = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                MaxDuration = "35"

            };
            test.WorkBody = new List<TestTask>
            {
                new TestTask
                {

                     NumTask="1",
                     PossibleAnsw1="",
                     PossibleAnsw2="",
                     PossibleAnsw3="",
                     RightNum="",
                     PossibleAnsw4="",
                     Word=""

                },
                new TestTask
                {

                     NumTask="1",
                     PossibleAnsw1="",
                     PossibleAnsw2="",
                     PossibleAnsw3="",
                     RightNum="",
                     PossibleAnsw4="",
                     Word=""

                }

            };

            //userent = new UserForAuthorization();
            // EntCommand = new Command(OnSave);
            //GettingClassrooms();
            //


            CreateClick = new Command(Create_Click);
            AddClick = new Command(Add_Click);
            

        }

        public List<TestTask> TasksList { 
            get
            {
                return test.WorkBody;
            }
            set
            {
                test.WorkBody = value;
                OnPropertyChanged("TasksList");
            } 
        }
        public string Taskname
        {
            // Когда надо вернуть фамилию.
            get => test.WorkBody[1].Word;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                test.WorkBody[1].Word = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Taskname));
            }
        }
        public string Ans1
        {
            // Когда надо вернуть фамилию.
            get => test.WorkBody[1].PossibleAnsw1;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                test.WorkBody[1].PossibleAnsw1 = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Ans1));
            }
        }
        public string Ans2
        {
            // Когда надо вернуть фамилию.
            get => test.WorkBody[1].PossibleAnsw2;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                test.WorkBody[1].PossibleAnsw2 = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Ans2));
            }
        }
        public string Ans3
        {
            // Когда надо вернуть фамилию.
            get => test.WorkBody[1].PossibleAnsw3;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                test.WorkBody[1].PossibleAnsw3 = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Ans3));
            }
        }
        public string Ans4
        {
            // Когда надо вернуть фамилию.
            get => test.WorkBody[1].PossibleAnsw4;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                test.WorkBody[1].PossibleAnsw4 = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Ans4));
            }
        }
        public string Right
        {
            // Когда надо вернуть фамилию.
            get => test.WorkBody[1].RightNum;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                test.WorkBody[1].RightNum = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Right));
            }
        }





        //public async void AddTaskAsync()
        //{
        //    try
        //    {
        //        if (api == null)
        //        {
        //            throw new Exception("api не создан!!!");
        //        }

        //       // string TestWorkID = await api.AddTestWorkAsync(test);
        //       // TasksList = new ObservableCollection<TestWorkForAdd>(await api.AddTestWorkAsync(TestWorkID));
        //        OnPropertyChanged(nameof(TasksList));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex.Message);
        //    }

        //}


        //public async Task AddTestWork()
        //{
        //    TestWorkForAdd testWorkForAdd = new TestWorkForAdd
        //    {
        //        WorkHeader = new WorkHeader
        //        {
        //            // в тесте всегда 1
        //            IdTypeWork = "1",
        //            DateTimeCreate = DateTime.Now.ToString(),
        //            DateTimeStart = DateTime.Now.ToString(),
        //            Description = "Описанеи тестовой работы номер 1",
        //            Name = "Тестовая работа номер 1",
        //            MaxDuration = "35",
        //            IsNonMark = "0",
        //            IdJournal = curClassroom.id_Journal,
        //            //Id <- не надо заполнять.
        //        },
        //        WorkBody = new List<TestTask>
        //        {
        //            new TestTask
        //            {
        //                //Id <- не надо заполнять.
        //                //IdTest <- не надо заполнять.
        //                NumTask = "1", // Номер задачи в работе.
        //                PossibleAnsw1 = "A",
        //                PossibleAnsw2 = "B",
        //                PossibleAnsw3 = "C",
        //                PossibleAnsw4 = "D",
        //                RightNum = "1",
        //                Word = "SomeWord",
        //            },
        //            new TestTask
        //            {
        //                //Id <- не надо заполнять.
        //                //IdTest <- не надо заполнять.
        //                NumTask = "2", // Номер задачи в работе.
        //                PossibleAnsw1 = "халас",
        //                PossibleAnsw2 = "изен",
        //                PossibleAnsw3 = "пулзен",
        //                PossibleAnsw4 = "пулан",
        //                RightNum = "1",
        //                Word = "хлеб",
        //            }
        //        }
        //    };
        //    В результате получем идентификатор работы.
        //     string TestWorkID = await api.AddTestWorkAsync(testWorkForAdd);
        //}


        ////public ObservableCollection<TestWorkForAdd> TasksList { get; set; }
        private void Create_Click(object ob)
        {
            First.Base_frame.Navigate(new marks());
            //открытие новой страницы с вводом логина и пароля 
        }
        public Command CreateClick
        {
            get;
            set;
        }
        private void Add_Click(object ob)
        {
            TasksList.Add(
                new TestTask
                {

                    NumTask = (test.WorkBody.Count + 1).ToString(),
                    PossibleAnsw1 = "",
                    PossibleAnsw2 = "",
                    PossibleAnsw3 = "",
                    RightNum = "",
                    PossibleAnsw4 = "",
                    Word = ""

                }
            );

        }
        public Command AddClick
        {
            get;
            set;
        }







    }
    }

   

