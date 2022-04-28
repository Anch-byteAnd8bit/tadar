using System;
using Tadar.Models;

namespace Tadar.ViewModels
{
    /// <summary>
    /// Класс для работы со страницей прохождения тестирования.
    /// </summary>
    class PerfomingWorkViewModel: BaseViewModel
    {
        public PerfomingWorkViewModel()
        {

        }

        public PerfomingWorkViewModel(Work work)
        {
            this.work = work;
        }

        /// <summary>
        /// Ссылка на тест.
        /// </summary>
        public Work work;

        /// <summary>
        /// Название теста.
        /// </summary>
        public string Name
        {
            get { return work.Name; }
            set
            {
                work.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Дата проведения теста.
        /// </summary>
        public DateTime Date
        {
            get { return work.Date; }
            set
            {
                work.Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        /// <summary>
        /// Описание теста.
        /// </summary>
        public string Description
        {
            get { return work.Description; }
            set
            {
                work.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

    }
}
