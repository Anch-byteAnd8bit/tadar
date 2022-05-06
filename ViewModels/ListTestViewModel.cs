using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
   public class ListTestViewModel : BaseViewModel
    {
        nsAPI.Entities.RegisteredUser work;
        public ListTestViewModel()
        {
            //api.MainUser.Name
            TestClick = new Command(Test_Click);
            Test14Click = new Command(Test14_Click);
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
                TestsList = new ObservableCollection<RegisteredUser>(await api.FindUsersAsync());
                OnPropertyChanged(nameof(TestsList));
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }
        public ObservableCollection<RegisteredUser> TestsList { get; set; }


       
        private void Test_Click(object ob)
        {
            work = (RegisteredUser)ob;
            First.Base_frame.Navigate(new PerfomingWorkPage(work));
           
        }
        public Command TestClick
        {
            get;
            
        }
        private void Test14_Click(object ob)
        {
            work = (RegisteredUser)ob;
            First.Base_frame.Navigate(new Test14Page());

        }
        public Command Test14Click
        {
            get;

        }
        //void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //     First.Base_frame.Navigate(new PerfomingWorkPage(api.MainUser));


        //}

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



    }
}
