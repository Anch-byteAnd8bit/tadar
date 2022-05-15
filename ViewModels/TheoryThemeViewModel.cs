using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;

namespace Tadar.ViewModels
{
    class TheoryThemeViewModel : BaseViewModel
    {
        private List <Theory> themes;
        private string idclass;
        public TheoryThemeViewModel(string clsid)
        {
            idclass = clsid;
            //TestClick = new Command(Test_Click);
            // TextClick = new Command(Text_Click);
            LoadTestsAsync(idclass);


        }
        
        public async void LoadTestsAsync(string idclass)
        {
            try
            {
                Themes = await api.GetTheoriesByClassroomIDAsync(idclass);
                

                
                //OnPropertyChanged(nameof(Themes));
                //OnPropertyChanged(nameof(TextsList));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Вы не состоите ни в одном классе или в классе нет теории!");
            }
        }

     //   public ObservableCollection<Theory> Themes { get; set; }
        public List<Theory> Themes
        {
            get { return themes; }
            set
            {
                themes = value;
                OnPropertyChanged(nameof(Themes));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }



    }
}
