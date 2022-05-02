using System;
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

        }

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
