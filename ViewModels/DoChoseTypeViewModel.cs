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
    public class DoChoseTypeViewModel: BaseViewModel
    {

        public DoChoseTypeViewModel()
        {
            //api.MainUser.Name
            WriteClick = new Command(Write_Click);
            Test14Click = new Command(Test14_Click);
            
          
        }


        private void Write_Click(object ob)
        {
            First.Base_frame.Navigate(new DoWritePage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void Test14_Click(object ob)
        {
            First.Base_frame.Navigate(new Do14HeaderPage());
            //открытие новой страницы с вводом логина и пароля 
        }
       

        public Command WriteClick
        {
            get;
            set;
        }
        public Command Test14Click
        {
            get;
            set;
        }
    

    }
}
