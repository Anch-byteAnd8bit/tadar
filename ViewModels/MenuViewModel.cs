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
