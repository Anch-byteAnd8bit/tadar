using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tadar.ViewModels
{
    public class ViewTheoryViewModel : BaseViewModel
    {
        Theory theo;
        public ViewTheoryViewModel(Theory theory)
        {
            theo = theory;
           // LoadTestsAsync(theo.ID);

        }

        public string Topic
        {
            get { return theo.Topic; }
            set
            {
                theo.Topic = value;
                OnPropertyChanged(nameof(Topic));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }
        public string Source
        {
            get { return theo.Source; }
            set
            {
                theo.Source = value;
                OnPropertyChanged(nameof(Source));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }

        public string Content
        {
            get { return theo.Content; }
            set
            {
                theo.Content = value;
                OnPropertyChanged(nameof(Content));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }


    }
}
