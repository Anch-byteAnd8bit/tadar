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
        nsAPI.Entities.RegisteredUser work;
        public PerfomingWorkViewModel(nsAPI.Entities.RegisteredUser work)
        {


          this.work = work;
            LoadClick = new Command(Load_Click);
            LoadUsersAsync();
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
        public async void LoadUsersAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                TestList = new ObservableCollection<RegisteredUser>(await api.FindUsersAsync());
                OnPropertyChanged(nameof(TestList));
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }
        public ObservableCollection<RegisteredUser> TestList { get; set; }




        //public PerfomingWorkViewModel(Work work)
        //{
        //    //this.work = work;
        //}

        /// <summary>
        /// Ссылка на тест.
        /// </summary>
        //public Work work;

        /// <summary>
        /// Название теста.
        /// </summary>
        public string Surname
        {
            // Когда надо вернуть фамилию.
            get => work.Surname;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                work.Surname = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Surname));
            }
        }
        public string Name
        {
            // Получить.
            get => work.Name;
            // Задать.
            set
            {
                work.Name = value;
                // Уведомление.
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Дата проведения теста.
        /// </summary>
        //public DateTime Date
        //{
        //    get { return work.Date; }
        //    set
        //    {
        //        work.Date = value;
        //        OnPropertyChanged(nameof(Date));
        //    }
        //}

        ///// <summary>
        ///// Описание теста.
        ///// </summary>
        //public string Description
        //{
        //    get { return work.Description; }
        //    set
        //    {
        //        work.Description = value;
        //        OnPropertyChanged(nameof(Description));
        //    }
        //}

    }
}
