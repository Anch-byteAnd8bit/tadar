using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadar
{
    class UserViewModel : ViewModelBase
    {
        public User user;

        public UserViewModel(User user)
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

        public int Age
        {
            get { return user.Age; }
            set
            {
                user.Age = value;
                OnPropertyChanged("Age");
            }
        }
        //public string Marks
        //{
        //    get { return user.Marks; }
        //    set
        //    {
        //        user.Marks.Add(value);
        //        OnPropertyChanged("Age");
        //    }
        //}


    }
}
