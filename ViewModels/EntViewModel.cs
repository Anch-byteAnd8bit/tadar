using Helpers;
using nsAPI;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            ppp();
        }

        private async void ppp()
        {
            var s = await api.GetImageByAlias("carrot");
            if (s != null)
            {
                img = Other.StreamToImageSource(s);
                OnPropertyChanged("Img");
            }
            else
            {
                Msg.Write(api.LastException.Message);
            }
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

        private ImageSource img;

        public ImageSource Img
        {
            get { return img; }
            set { img = value; }
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
                        switch (api.LastException.CodeAPI)
                        {
                            case CODE_ERROR.ERR_NotEnoughInf:
                                // Пользователь сумел отправить одно из полей пустым.
                                Msg.Write("Пользователь сумел отправить одно из полей пустым.");
                                break;
                            case CODE_ERROR.ERR_UserNotFound:
                                // Такой пользователь не найден в БД - либо неправильно ввели логин-пароль,
                                // либо еще не регистрировались.
                                Msg.Write("Такой пользователь не найден в БД.");
                                break;
                            case CODE_ERROR.ERR_IsTooLong:
                                // Слишком длинный пароль или логин
                                Msg.Write("Слишком длинный пароль или логин.");
                                break;
                        }                        
                    }
                    else if (api.LastException.TypeError == TError.ConnectionError)
                    {// Проблемы с интерентом, либо с сервером. КОроче не удалось связаться с сервером.
                        switch (api.LastException.SocketError)
                        {
                            case System.Net.Sockets.SocketError.NetworkDown:
                                // Проблемы с сетью на этом компе.
                                Msg.Write("Проблемы с сетью на этом компе.");
                                break;
                            case System.Net.Sockets.SocketError.ConnectionRefused:
                                // СБрошено соединение - серверный комп работает, но сервер не запущен.
                                Msg.Write("Веб-сервер не работает, но сервер не запущен.");
                                break;
                            case System.Net.Sockets.SocketError.HostNotFound:
                                // Не нашел хост, что также может говорить об отсутствии интернета,
                                Msg.Write("Не найден сервер. Проверьте настройки интернета!");
                                // но это не точно.
                                break;
                        }
                    }
                    else
                    {
                        Msg.Write(api.LastException.Message);
                    }
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
