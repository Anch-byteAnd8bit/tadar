using Helpers;
using nsAPI;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tadar;
using Tadar.Helpers;

namespace Tadar
{
    public class RegViewModel : ViewModelBase
    {
        private UserForRegistration userreg;
        private List<Gender> genders;
        public RelayCommand RegCommand { get; set; }

        public string Surname
        {
            get => userreg.Surname;
            set
            {
                userreg.Surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string Name
        {
            get => userreg.Name;
            set
            {
                userreg.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Middlename
        {
            get => userreg.Middlename;
            set
            {
                userreg.Middlename = value;
                OnPropertyChanged(nameof(Middlename));
            }
        }
        public string Login
        {
            get => userreg.Login;
            set
            {
                userreg.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get => userreg.Pass;
            set
            {
                userreg.Pass = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Email
        {
            get => userreg.Email;
            set
            {
                userreg.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public List<Gender> Genders
        {
            get => genders;
            set
            {
                genders = value;
                OnPropertyChanged(nameof(Genders));
                SelectedGender = Genders[0];
            }
        }

        public Gender SelectedGender
        {
            get
            {
                return 
                    Genders.SingleOrDefault(el=>el.ID == userreg.GenderID);
            }
            set
            {
                Gender g = Genders.SingleOrDefault(el => el == value)??Genders[0];
                userreg.GenderID = g.ID;
                OnPropertyChanged(nameof(SelectedGender));
            }
        }

        public DateTime BDate
        {
            get => DateTime.Parse(userreg.BDate);
            set
            {
                userreg.BDate = value.ToString("d");
                OnPropertyChanged(nameof(BDate));
            }
        }


        public RegViewModel()
        {
            api = new API();
            userreg = new UserForRegistration();
            RegCommand = new RelayCommand(OnSave);

        }

        //// регистрация добавление данных в бд и переход на новую страницу
        /*userreg.Surname = f_Box.Text;
        userreg.Name = name_box.Text;
        userreg.Middlename = secname_box.Text;
        userreg.Login = logbox.Text;
        userreg.Email = mailbox.Text;
        userreg.Pass = pswbox.Password;*/

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

            if (userreg.Email.Length < 5 || !userreg.Email.Contains("@") || !userreg.Email.Contains("."))
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

        public void OnSave(object obj)
        {
            var res1 = api.UserRegAsync(userreg).Result;
            if (res1)
            {
                Log.Write(api.MainUser.ID + ": " + api.MainUser.Login + " ("
                    + api.MainUser.Surname + " " + api.MainUser.Name + ")");
            }
            First.Base_frame.Navigate(new Menu_Page());
        }
    }
}
