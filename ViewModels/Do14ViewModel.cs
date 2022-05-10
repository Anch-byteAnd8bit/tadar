using Helpers;
using nsAPI;
using nsAPI.Entities;
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
        public Do14ViewModel()
        {
            test = new TestWorkForAdd();

            //userent = new UserForAuthorization();
            // EntCommand = new Command(OnSave);
            CreateClick = new Command(Create_Click);
            AddClick = new Command(Add_Click);
            LoadUsersAsync();
            
        }

        public async void AddTaskAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
              //  TasksList = new ObservableCollection<TestWorkForAdd>(await api.AddTestWorkAsync(test));
                OnPropertyChanged(nameof(TasksList));
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }

        }

        public async void LoadUsersAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                TasksList = new ObservableCollection<RegisteredUser>(await api.FindUsersAsync());
                OnPropertyChanged(nameof(TasksList));
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }
        public ObservableCollection<RegisteredUser> TasksList { get; set; }
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
            First.Base_frame.Navigate(new marks());
            //открытие новой страницы с вводом логина и пароля 
        }
        public Command AddClick
        {
            get;
            set;
        }
        //private UserForAuthorization userent;
        //  public Command EntCommand { get; set; }

        public string Name
        {
            // Когда надо вернуть фамилию.
            get => test.WorkHeader.Name;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                test.WorkHeader.Name = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Testname
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
