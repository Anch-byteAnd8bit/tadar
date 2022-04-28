using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tadar
{
    class UserViewModel : ViewModelBase
    {
        public UserForRegistration user;

        public UserViewModel(UserForRegistration user)
        {
            this.user = user;
        }

        public string Name
        {
            get { return user.Name; }
            set
            {
                user.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Surname
        {
            get { return user.Surname; }
            set
            {
                user.Surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public string BDate
        {
            get { return user.BDate; }
            set
            {
                user.BDate = value;
                OnPropertyChanged("BDate");
            }
        }
        public string Middlename
        {
            get { return user.Middlename; }
            set
            {
                user.Middlename = value;
                OnPropertyChanged("Middlename");
            }
        }
        public string Login
        {
            get { return user.Login; }
            set
            {
                user.Login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Email
        {
            get { return user.Email; }
            set
            {
                user.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Pass
        {
            get { return user.Pass; }
            set
            {
                user.Pass = value;
                OnPropertyChanged("Pass");
            }
        }

        public string Gender
        {
            get { return user.GenderID; }
            set
            {
                user.GenderID = value;
                OnPropertyChanged("GenderID");
            }
        }



    }
}
