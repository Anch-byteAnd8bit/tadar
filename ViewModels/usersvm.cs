﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadar
{
    class usersvm: ViewModelBase
    {
        public users user;
        public usersvm (users user)
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


    }
}
