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
            api = API.Instance;
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
                    // Переходим к странице "Меню".
                    Models.First.Base_frame.Navigate(new MenuPage());
                }
                else
                {
                    if (api.LastException != null && api.LastException.TypeError == TError.DefinedError)
                    {
                        switch (api.LastException.Code)
                        {
                            case CODE_ERROR.ERR_NotEnoughInf:
                                // Пользователь сумел отправить одно из полей пустым.
                                break;
                            case CODE_ERROR.ERR_UserNotFound:
                                // Такой пользователь не найден в БД - либо неправильно ввели логин-пароль,
                                // либо еще не регистрировались.
                                break;
                            case CODE_ERROR.ERR_IsTooLong:
                                // Слишком длинный пароль или логин
                                break;
                        }                        
                    }
                    else if (api.LastException.TypeError == TError.ConnectError)
                    {
                        // Проблемы с интерентом, либо с сервером. КОроче не удалось связаться с сервером.
                    }
                    else
                        Msg.Write(api.LastException.Message);
                }
            }
            // TODO: надо потом определять тип ошибки и выводить соотвествующие сообщения...
            catch (Exception ex)
            {
                Msg.Write(ex.Message);
            }
        }
    }
}
