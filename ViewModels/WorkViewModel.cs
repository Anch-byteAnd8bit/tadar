using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tadar.Models;

namespace Tadar.ViewModels
{
    class WorkViewModel: ViewModelBase
    {
        public Work work;

        public WorkViewModel(Work work)
        {
            this.work = work;
        }

        public string Name
        {
            get { return work.Name; }
            set
            {
                work.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public DateTime Date
        {
            get { return work.Date; }
            set
            {
                work.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public string Description
        {
            get { return work.Description; }
            set
            {
                work.Description = value;
                OnPropertyChanged("Description");
            }
        }

    }
}
