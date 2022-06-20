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
   public class MarkViewModel: BaseViewModel
    {
        private List<RegisteredClassroom> classroomsuser = new List<RegisteredClassroom>();
        private RegisteredClassroom cls;

        public MarkViewModel()
        {
            LoadClasssAsync();
            EnterClick = new Command(Enter_Click);
            BackClick = new Command(Back_Click);
        }
        private void Back_Click(object ob)
        {
            First.Base_frame.Navigate(new MenuPage());

        }
        public Command BackClick
        {
            get;
            set;
        }

        public async void LoadClasssAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                string idTeacher = api.Refbooks.GetRole(nsAPI.TRoles.Teacher).ID;
                classroomsuser =
                  await api.GetClassroomsByUserIdAsync(api.MainUser.ID, idTeacher);
              
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
        private void Enter_Click(object ob)
        {

            //cls = new RegisteredClassroom();
            cls = (RegisteredClassroom)ob;
            string iclass = cls.ID;
            
            First.Base_frame.Navigate(new StudentsPage(iclass));

        }



    }
}
