using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.ObjectModel;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
  public class DictViewModel : BaseViewModel
    {
       

        public DictViewModel()
        {
            //api.MainUser.Name
            AddClick = new Command(Add_Click);
            DelClick = new Command(Del_Click);
           
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
                WordsList = new ObservableCollection<RegisteredUser>(await api.FindUsersAsync());
                OnPropertyChanged(nameof(WordsList));
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }
        public ObservableCollection<RegisteredUser> WordsList { get; set; }



        private void Add_Click(object ob)
        {
            First.Base_frame.Navigate(new marks());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void Del_Click(object ob)
        {
            First.Base_frame.Navigate(new marks());
            //открытие новой страницы с вводом логина и пароля 
        }


        public Command AddClick
        {
            get;
            set;
        }
        public Command DelClick
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


    }
}
