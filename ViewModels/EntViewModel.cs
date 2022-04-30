using Helpers;
using nsAPI;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tadar.Helpers;
using Tadar.Views;

namespace Tadar.ViewModels
{
    public class EntViewModel: BaseViewModel
    {
        public EntViewModel()
        {
            api = new API();
            userent = new UserForAuthorization();
            EntCommand = new Command(OnSave);

        }
        private UserForAuthorization userent;
        public Command EntCommand { get; set; }
        public string Login
        {
            // Когда надо вернуть фамилию.
            get => userent.Login;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                userent.Login = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Login));
            }
        }
        public void OnSave(object PassElem)
        {
            // Получаем пароль.
            userent.Pass = (PassElem as PasswordBox).Password;
            // Вызываем асинхронно пароль.
            SaveAsync();
            // TODO: блокируем кнопку регистрации и все элементы ввода. Можно добавить
            // символ выполенния операции...

        }

        private async void SaveAsync()
        {
            // Во время любой операции с сервером может вылезти ошибка!
            try
            {
                if (await api.UserAuthAsync(userent))
                {
                    Log.Write(api.MainUser.ID + ": " + api.MainUser.Login + " ("
                        + api.MainUser.Surname + " " + api.MainUser.Name + ")");
                }
                Models.First.Base_frame.Navigate(new MenuPage());
            }
            // TODO: надо потом определять тип ошибки и выводить соотвествующие сообщения...
            catch (ErrorResponseException ex)
            {
                switch (ex.ErrCode)
                {
                    case 101:
                        MessageBox.Show("неправильный логин или пароль");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }
    }
}
