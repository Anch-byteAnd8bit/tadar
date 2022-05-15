using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tadar.ViewModels
{
   public class ListStudAnsViewModel : BaseViewModel
    {
        private TestWork work;
        private List<RegisteredUser> classroomsusers = new List<RegisteredUser>();
        public ListStudAnsViewModel(TestWork test)
        {
            work = test;
            LoadAnsAsync(test);
            //LoadClassAsync(iclass);

            //SendClick = new Command(Send_Click);



        }


        public async void LoadAnsAsync(TestWork test)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                var ansbywork =
                  await api.GetAnswersByWork(test.WorkHeader.ID);
                string[] usersid = new string[] { };
                for (int i = 0; i < ansbywork.TestAnswers.Count; i++)
                {
                   usersid[i] = ansbywork.TestAnswers[i].AnswerHeader.id_Student;

                }
                LoadClassAsync(usersid);


               // OnPropertyChanged(nameof(Classrooms));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public async void LoadClassAsync(string[] iclass)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                classroomsusers =
                  await api.GetUsersByIdAsync(iclass);

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


    }
}
