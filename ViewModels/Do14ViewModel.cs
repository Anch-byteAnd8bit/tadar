using nsAPI;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tadar.ViewModels
{
  public class Do14ViewModel : BaseViewModel
    {
        public Do14ViewModel()
        {
            
            //userent = new UserForAuthorization();
            // EntCommand = new Command(OnSave);

        }
        //private UserForAuthorization userent;
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
