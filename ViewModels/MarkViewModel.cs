using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tadar.Models;

namespace Tadar.ViewModels
{
    class MarkViewModel: BaseViewModel
    {
        public Mark mark;

        public MarkViewModel(Mark mark)
        {
            this.mark = mark;
        }

        public string Nameoftest
        {
            get { return mark.NameOfTest; }
            set
            {
                mark.NameOfTest = value;
                OnPropertyChanged("Nameoftest");
            }
        }

        public DateTime Dateoftest
        {
            get { return mark.DateOfTest; }
            set
            {
                mark.DateOfTest = value;
                OnPropertyChanged("Dateoftest");
            }
        }

        public string Markoftest
        {
            get { return mark.MarkOfTest; }
            set
            {
                mark.MarkOfTest = value;
                OnPropertyChanged("Markoftest");
            }
        }
    }
}
