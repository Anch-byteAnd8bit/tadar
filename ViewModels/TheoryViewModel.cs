using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    class TheoryViewModel
    {
        public TheoryViewModel()
        {
            //api.MainUser.Name
            CreateClick = new Command(Create_Click);
            ListClick = new Command(List_Click);
            BackClick = new Command(Back_Click);

        }
        private void Back_Click(object ob)
        {
            First.Base_frame.Navigate(new MenuPage());

        }

        public Command BackClick
        {
            get;
            set;
        }
        private void Create_Click(object ob)
        {
            First.Base_frame.Navigate(new DoTheoryPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void List_Click(object ob)
        {
            First.Base_frame.Navigate(new TheListClassPage());
            //открытие новой страницы с вводом логина и пароля 
        }


        public Command CreateClick
        {
            get;
            set;
        }
        public Command ListClick
        {
            get;
            set;
        }


    }
}
