using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tadar.ViewModels
{
    class MarkViewModel: ViewModelBase
    {
        public Mark mark;

        public MarkViewModel(Mark mark)
        {
            this.mark = mark;
        }

        public string Nameoftest
        {
            get { return mark.Nameoftest; }
            set
            {
                mark.Nameoftest = value;
                OnPropertyChanged("Nameoftest");
            }
        }

        public DateTime Dateoftest
        {
            get { return mark.Dateoftest; }
            set
            {
                mark.Dateoftest = value;
                OnPropertyChanged("Dateoftest");
            }
        }

        public string Markoftest
        {
            get { return mark.Markoftest; }
            set
            {
                mark.Markoftest = value;
                OnPropertyChanged("Markoftest");
            }
        }
    }
}
