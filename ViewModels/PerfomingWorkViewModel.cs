using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.ObjectModel;
using Tadar.Helpers;
using Tadar.Models;

namespace Tadar.ViewModels
{
    /// <summary>
    /// Класс для работы со страницей прохождения тестирования.
    /// </summary>
    class PerfomingWorkViewModel: BaseViewModel
    {
        TestWork work;
        public PerfomingWorkViewModel(TestWork work)
        {


          this.work = work;
            LoadClick = new Command(Load_Click);
            //LoadUsersAsync();
        }

        private void Load_Click(object ob)
        {
            First.Base_frame.Navigate(new Ent_page());
            //открытие новой страницы с вводом логина и пароля 



        }
        public Command LoadClick
        {
            get;
            set;
        }
       
        public ObservableCollection<RegisteredUser> TestList { get; set; }




      

    }
}
