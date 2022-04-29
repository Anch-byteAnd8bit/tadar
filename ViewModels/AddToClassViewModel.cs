using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using nsAPI.Entities;

namespace Tadar.ViewModels
{
   public class AddToClassViewModel : BaseViewModel
    {

        public AddToClassViewModel()
        {
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
        
        //public string Surname
        //{
        //    // Когда надо вернуть фамилию.
        //    get => api.MainUser.Surname;
        //    // Когда надо задать фамилию.
        //    set
        //    {
        //        // Присваиваем новое значение фамилии.
        //        api.MainUser.Surname = value;
        //        // Уведомляем форму, что свойство "Surname" изменилось.
        //        OnPropertyChanged(nameof(Surname));
        //    }
        //}
        //public string Name
        //{
        //    // Получить.
        //    get => api.MainUser.Name;
        //    // Задать.
        //    set
        //    {
        //        api.MainUser.Name = value;
        //        // Уведомление.
        //        OnPropertyChanged(nameof(Name));
        //    }
        //}
        //public string Middlename
        //{
        //    get => api.MainUser.Middlename;
        //    set
        //    {
        //        api.MainUser.Middlename = value;
        //        OnPropertyChanged(nameof(Middlename));
        //    }
        //}
        //public string BDate
        //{
        //    get => api.MainUser.BDate.ToString("d");
        //    set
        //    {
        //        // Задавемое значение конвертируем в формат DateTime.
        //        api.MainUser.BDate = DateTimeOffset.Parse(value);
        //        // Уведомление.
        //        OnPropertyChanged(nameof(BDate));
        //    }
        //}
    }
}
