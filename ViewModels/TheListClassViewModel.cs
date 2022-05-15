using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    class TheListClassViewModel : BaseViewModel
    {
        private List<RegisteredClassroom> classrooms = new List<RegisteredClassroom>();
        private RegisteredClassroom cls;

        public TheListClassViewModel()
        {
            LoadClasssAsync();
            EnterClick = new Command(Enter_Click);
            

        }

        private void Enter_Click(object ob)
        {
            cls = (RegisteredClassroom)ob;
            First.Base_frame.Navigate(new TheoryThemePage(cls.ID.ToString()));

        }
        public Command EnterClick
        {
            get;

        }

        public async void LoadClasssAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                classrooms =
                  await api.GetAllClassess();
              

                //  ClasssList = new ObservableCollection<RegisteredClassroom>();

                OnPropertyChanged(nameof(Classrooms));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы не состоите ни в одном классе!");
            }
        }

        public List<RegisteredClassroom> Classrooms
        {
            get { return classrooms; }
            set
            {
                classrooms = value;
                OnPropertyChanged(nameof(Classrooms));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }



    }
}
