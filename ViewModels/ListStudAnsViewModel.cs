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
   public class ListStudAnsViewModel : BaseViewModel
    {
        private TestWork work;
        private RegisteredUser user;
        private List<RegisteredUser> classroomsusers = new List<RegisteredUser>();
        public ListStudAnsViewModel(TestWork test)
        {
            work = test;
            LoadAnsAsync(test);
            //LoadClassAsync(iclass);

            SendClick = new Command(Send_Click);



        }


        public Command SendClick
        {
            get;
            set;
        }
        private void Send_Click(object ob)
        {
            user = new RegisteredUser();
            user = (RegisteredUser)ob;
            string iduser = user.ID;
            First.Base_frame.Navigate(new AnsTestPage(iduser, work));
        }


        public async void LoadAnsAsync(TestWork test)
        {
            try
            {
                var ansbywork = await api.GetAnswersByWork(test.WorkHeader.ID);
                List <string> usersid = new List<string> { };


                if (ansbywork != null)
                {
                    for (int i = 0; i < ansbywork.TestAnswers.Count; i++)
                    {
                        if ((ansbywork.TestAnswers[i].AnswerHeader.Mark=="NULL") || (ansbywork.TestAnswers[i].AnswerHeader.Mark == null))
                        {
                            usersid.Add(ansbywork.TestAnswers[i].AnswerHeader.id_UserInClasses);
                        }
                    }
                    LoadStudentsAsync(usersid);
                }
                else

                    MessageBox.Show("Никто не решал этот тест");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Асинхронная загрузка списка учеников
        /// </summary>
        /// <param name="iclass"></param>
        public async void LoadStudentsAsync(List<string> userInClassIDs)
        {
            try
            {
                classroomsusers = await api.GetUsersByAnswersAsync(userInClassIDs);

                //  ClasssList = new ObservableCollection<RegisteredClassroom>();

                OnPropertyChanged(nameof(Students));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<RegisteredUser> Students
        {
            get { return classroomsusers; }
            set
            {
                classroomsusers = value;
                OnPropertyChanged(nameof(Students));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }


    }
}
