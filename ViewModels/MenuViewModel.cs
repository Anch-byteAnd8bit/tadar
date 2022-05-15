using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private List<RegisteredClassroom> classrooms = new List<RegisteredClassroom>();
        public MenuViewModel()
        {
            //api.MainUser.Name
            MarkClick = new Command(mark_Click);
            AkkClick = new Command(Akk_Click);
            TestClick = new Command(Test_Click);
            ClassClick = new Command(Class_Click);
            GameClick = new Command(Game_Click);
            MaketestClick = new Command(make_test_Click);
            SelftestClick = new Command(selftest_Click);
            DicktClick= new Command(Dickt_Click);
            TheClick = new Command(The_Click);
            LoadClasssAsync();
        }

        public async void LoadClasssAsync()
        {
            try
            {
                api = nsAPI.API.Instance;

                classrooms = await api.GetClassroomsByUserIdAsync(api.MainUser.ID);

                //test = new TestWork();
                //test.WorkHeader = (WorkHeader)ob;
                //test.WorkBody = works.TestWorks.SingleOrDefault(w => w.WorkHeader == test.WorkHeader).WorkBody;
                //First.Base_frame.Navigate(new Test14Page(test));

                

                //  ClasssList = new ObservableCollection<RegisteredClassroom>();

                OnPropertyChanged(nameof(Classrooms));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public List<RegisteredClassroom> Classrooms
        {
            get { return classrooms; }
            set
            {
                classrooms = value;
                OnPropertyChanged(nameof(Classrooms));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
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
