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
    class MarkViewModel: BaseViewModel
    {
        private List<RegisteredClassroom> classroomsuser = new List<RegisteredClassroom>();
        private RegisteredClassroom cls;

        public MarkViewModel()
        {
            LoadClasssAsync();
            //EnterClick = new Command(Enter_Click);
        }


        public async void LoadClasssAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                classroomsuser =
                  await api.GetClassroomsByUserIdAsync(api.MainUser.ID,"3");
              
                //  ClasssList = new ObservableCollection<RegisteredClassroom>();

                OnPropertyChanged(nameof(Classrooms));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<RegisteredClassroom> Classrooms
        {
            get { return classroomsuser; }
            set
            {
                classroomsuser = value;
                OnPropertyChanged(nameof(Classrooms));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }

        public Command EnterClick { get; }
        private async void Enter_Click(object ob)
        {

            cls = new RegisteredClassroom();
            cls = (RegisteredClassroom)ob;
            string iclass = cls.ID;
            await api.AddStudent(api.MainUser.ID.ToString(), iclass);
            OnPropertyChanged(nameof(Classrooms));
            MessageBox.Show(api.MainUser.Name.ToString() + " в классе " + cls.Name.ToString());
            First.Base_frame.Navigate(new MenuPage());

        }



    }
}
