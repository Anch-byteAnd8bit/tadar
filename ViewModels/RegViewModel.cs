﻿using Helpers;
using nsAPI;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Tadar.Helpers;
using Tadar.Views;

namespace Tadar.ViewModels
{
    public class RegViewModel : BaseViewModel
    {
        public RegViewModel()
        {
            api = new API();
            userreg = new UserForRegistration()
            {
                // Это надо делать, т.к. это свойство влияет на элемент интерфейса
                // и если оно не задано, то элементу будет задаваться значение null
                // что приведет к ошибке.
                BDate = DateTime.Now.ToString("d")
            };
            // Создаем команду для кнопки. Выполняться при нажатии будет
            // OnSave, а проверять доступна ли кнопка для нажатия,
            // будет метод ValidateSave
            RegCommand = new RelayCommand(OnSave, ValidateSave);
            // Получаем список полов от сервера.
            GettingGenders();
        }

        /// <summary>
        /// Асинхронный метод получения списка полов от сервера.
        /// </summary>
        private async void GettingGenders()
        {
            Genders = await api.GetGendersAsync();
        }

        /// <summary>
        /// Модель данных - пользотваель длля регистрации.
        /// </summary>
        private UserForRegistration userreg;
        /// <summary>
        /// Список полов.
        /// </summary>
        private List<Gender> genders;
        /// <summary>
        /// Команда для кнопки регистрации.
        /// </summary>
        public RelayCommand RegCommand { get; set; }

        /// <summary>
        /// Свойство фамилия.
        /// </summary>
        public string Surname
        {
            // Когда надо вернуть фамилию.
            get => userreg.Surname;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                userreg.Surname = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Surname));
            }
        }

        /// <summary>
        /// Свойство имя.
        /// </summary>
        public string Name
        {
            // Получить.
            get => userreg.Name;
            // Задать.
            set
            {
                userreg.Name = value;
                // Уведомление.
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Свойство отчество.
        /// </summary>
        public string Middlename
        {
            get => userreg.Middlename;
            set
            {
                userreg.Middlename = value;
                OnPropertyChanged(nameof(Middlename));
            }
        }

        /// <summary>
        /// Свойство логин.
        /// </summary>
        public string Login
        {
            get => userreg.Login;
            set
            {
                userreg.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        /// <summary>
        /// Свойства пароль.
        /// </summary>
        public string Password
        {
            get => userreg.Pass;
            set
            {
                userreg.Pass = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        /// <summary>
        /// Свойство email.
        /// </summary>
        public string Email
        {
            get => userreg.Email;
            set
            {
                userreg.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        /// <summary>
        /// Свойство список полов.
        /// </summary>
        public List<Gender> Genders
        {
            get => genders;
            // Задать новый список.
            set
            {
                // Получаем ноый список.
                genders = value;
                // Уведомляем форму о новом списке.
                OnPropertyChanged(nameof(Genders));
                // Задаем новый выбранный жлемент из списка.
                SelectedGender = Genders[0];
            }
        }
        /// <summary>
        /// Свойства выбранный элемент из списка полов "Genders"
        /// </summary>
        public Gender SelectedGender
        {
            get
            {
                // Ищем в списке полов, объект, у которого свойства ID совпдает с
                // со свйоством GenderID у регистрируемого пользователя.
                // Если такой элемент в списке не найден, то возвращаем первый элемент
                // из списка.
                return
                    Genders.SingleOrDefault(el=>el.ID == userreg.GenderID) ?? Genders[0];
            }
            set
            {
                // Если есть элемент в списке полов, который равен задавемому элементу...
                if (Genders.Exists(el => el == value))
                {
                    // ...присваиваем его ID полу регистрируемого пользователя.
                    userreg.GenderID = value.ID;
                }
                // Иначе, полу регистрируемого пользователя
                // присваиваем ID первого элемент списка.
                else userreg.GenderID = Genders[0].ID;
                // Уведомляем интфрейс о том, что это свйоство было изменено.
                OnPropertyChanged(nameof(SelectedGender));
            }
        }

        /// <summary>
        /// Свойства дата рождения.
        /// </summary>
        public DateTime BDate
        {
            get => DateTime.Parse(userreg.BDate);
            set
            {
                // Задавемое значение конвертируем в формат DateTime.
                userreg.BDate = value.ToString("d");
                // Уведомление.
                OnPropertyChanged(nameof(BDate));
            }
        }

        //// регистрация добавление данных в бд и переход на новую страницу
        /*userreg.Surname = f_Box.Text;
        userreg.Name = name_box.Text;
        userreg.Middlename = secname_box.Text;
        userreg.Login = logbox.Text;
        userreg.Email = mailbox.Text;
        userreg.Pass = pswbox.Password;*/

        /// <summary>
        /// Валидация - проверка на правильность введенных данных.
        /// </summary>
        /// <returns>true - если данные ввдедены правлиьно</returns>
        private bool ValidateSave()
        {
            bool isGood = true;
            if (string.IsNullOrWhiteSpace(userreg.BDate))
            {
                //birth.ToolTip = "Выберите дату рождения!";
                //birth.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else
            {
                //birth.ToolTip = null;
                //birth.BorderBrush = Brushes.Transparent;
            }

            if (string.IsNullOrWhiteSpace(userreg.Surname))
            {
                //f_Box.ToolTip = "Введите фамилию!";
                //f_Box.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else
            {
                //f_Box.ToolTip = null;
                //f_Box.BorderBrush = Brushes.Transparent;
            }

            if (string.IsNullOrWhiteSpace(userreg.Pass))
            {
                //pswbox.ToolTip = "Введите пароль!";
                //pswbox.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else
            {
                //pswbox.ToolTip = null;
                //pswbox.BorderBrush = Brushes.Transparent;
            }

            if (string.IsNullOrWhiteSpace(userreg.Pass))
            {
                //pswbox.ToolTip = "Введите пароль!";
                //pswbox.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else if (userreg.Pass.Length < 6)
            {
                //pswbox.ToolTip = "Слишком короткий пароль!";
                //pswbox.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else
            {
                //pswbox.ToolTip = null;
                //pswbox.BorderBrush = Brushes.Transparent;
            }

            if (string.IsNullOrWhiteSpace(userreg.Name))
            {
                //name_box.ToolTip = "Введите имя!";
                //name_box.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else
            {
                //name_box.ToolTip = null;
                //name_box.BorderBrush = Brushes.Transparent;
            }

            if (string.IsNullOrWhiteSpace(userreg.Middlename))
            {
                //secname_box.ToolTip = "Введите отчество!";
                //secname_box.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else
            {
                //secname_box.ToolTip = null;
                //secname_box.BorderBrush = Brushes.Transparent;
            }

            if (string.IsNullOrWhiteSpace(userreg.Email) || userreg.Email.Length < 5 || !userreg.Email.Contains("@") || !userreg.Email.Contains("."))
            {
                //mailbox.ToolTip = "Введите e-mail!";
                //mailbox.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else
            {
                //mailbox.ToolTip = null;
                //mailbox.BorderBrush = Brushes.Transparent;
            }
            if (string.IsNullOrWhiteSpace(userreg.Login))
            {
                //logbox.ToolTip = "Введите логин!";
                //logbox.BorderBrush = Brushes.Red;
            }
            else
            {
                //logbox.ToolTip = null;
                //logbox.BorderBrush = Brushes.Transparent;

            }
            return isGood;
        }

        public void OnSave()
        {
            var res1 = api.UserRegAsync(userreg).Result;
            if (res1)
            {
                Log.Write(api.MainUser.ID + ": " + api.MainUser.Login + " ("
                    + api.MainUser.Surname + " " + api.MainUser.Name + ")");
            }
            Models.First.Base_frame.Navigate(new MenuPage());
        }
    }
}