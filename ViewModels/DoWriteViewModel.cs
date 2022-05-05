using nsAPI;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tadar.ViewModels
{
    public class DoWriteViewModel : BaseViewModel
    {
        public DoWriteViewModel()
        {
          
           // EntCommand = new Command(OnSave);

        }

        //  public Command EntCommand { get; set; }
        public string Name
        {
            // Получить.
            get => api.MainUser.Name;
            // Задать.
            set
            {
                api.MainUser.Name = value;
                // Уведомление.
                OnPropertyChanged(nameof(Name));
            }
        }

    }
}
