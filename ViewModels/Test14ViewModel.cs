using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.ObjectModel;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
   public class Test14ViewModel: BaseViewModel
    {
        public Test14ViewModel()
        {
            //api.MainUser.Name
            MarkClick = new Command(mark_Click);
            //AkkClick = new Command(Akk_Click);
            //TestClick = new Command(Test_Click);
            //ClassClick = new Command(Class_Click);
            //GameClick = new Command(Game_Click);
          
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


        private void mark_Click(object ob)
        {
            First.Base_frame.Navigate(new marks());
            //открытие новой страницы с вводом логина и пароля 
        }

        public Command MarkClick
        {
            get;
            set;
        }



    }
}
