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
    public class StudentsViewModel : BaseViewModel
    {
        private List<RegisteredUser> classroomsusers = new List<RegisteredUser>();
        private RegisteredUser std;
        private string icl;

        public StudentsViewModel(string iclass)
        {

            LoadClassAsync(iclass);
            icl = iclass;
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

        public Command EnterClick { get; }
        private void Enter_Click(object ob)
        {

            std = new RegisteredUser();
            std = (RegisteredUser)ob;
            // string iclass = cls.ID;
            string istud = std.ID;
            First.Base_frame.Navigate(new StudMarksPage(icl, istud));

        }




    }
}
