using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;

namespace Tadar.ViewModels
{
    public class StudentsViewModel : BaseViewModel
    {
        private List<RegisteredUser> classroomsusers = new List<RegisteredUser>();
        private RegisteredClassroom cls;

        public StudentsViewModel(string iclass)
        {

            LoadClassAsync(iclass);
            //EnterClick = new Command(Enter_Click);
        }


        public async void LoadClassAsync(string iclass)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                classroomsusers =
                  await api.GetUsersByClassIdAsync(iclass);

                //  ClasssList = new ObservableCollection<RegisteredClassroom>();

                OnPropertyChanged(nameof(Classrooms));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<RegisteredUser> Classrooms
        {
            get { return classroomsusers; }
            set
            {
                classroomsusers = value;
                OnPropertyChanged(nameof(Classrooms));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }

        //public Command EnterClick { get; }
        //private async void Enter_Click(object ob)
        //{

        //    cls = new RegisteredClassroom();
        //    cls = (RegisteredClassroom)ob;
        //    string iclass = cls.ID;
        //    await api.AddStudent(api.MainUser.ID.ToString(), iclass);
        //    OnPropertyChanged(nameof(Classrooms));
        //    MessageBox.Show(api.MainUser.Name.ToString() + " в классе " + cls.Name.ToString());
        //    First.Base_frame.Navigate(new MenuPage());

        //}




    }
}
