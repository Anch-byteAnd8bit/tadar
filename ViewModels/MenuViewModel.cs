using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.ObjectModel;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
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
            LoadUsersAsync();
        }

        public async void LoadUsersAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                UsersList = new ObservableCollection<RegisteredUser>(await api.FindUsersAsync());
                OnPropertyChanged(nameof(UsersList));
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }
        public ObservableCollection<RegisteredUser> UsersList { get; set; }



        private void mark_Click(object ob)
        {
            First.Base_frame.Navigate(new marks());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void Akk_Click(object ob)
        {
            First.Base_frame.Navigate(new Do14());
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
            First.Base_frame.Navigate(new Ent_page());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void make_test_Click(object ob)
        {
            First.Base_frame.Navigate(new DoWritePage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void selftest_Click(object ob)
        {
            First.Base_frame.Navigate(new Ent_page());
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

        public Command MarkClick
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
