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
   public class AnsTestViewModel : BaseViewModel
    {
        private TestWork work = new TestWork();
        private string idus;
        private Answers answers = new Answers();
        Answers myanswers = new Answers();

        //private Answers answers;
        public AnsTestViewModel(string iduser,TestWork test)
        {
            work = test;
            OnPropertyChanged(nameof(TasksList));
            idus = iduser;
            LoadAnsAsync(work, idus);
            //SendClick = new Command(Send_Click);
        }

        public async void LoadAnsAsync(TestWork work, string idus)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }


                answers = await api.GetAnswersByWork(work.WorkHeader.ID, false);
                myanswers = answers.GetAnswersByIDUser(idus);
                
               
                //TODO: сохранять works, из него получать список заголовков ВСЕХ работ
                OnPropertyChanged(nameof(AnswersList));
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Вы не состоите ни в одном классе или в классе нет заданий!");
            }
        }

        public List<TestAnswer> AnswersList
        {
            get
            {
                return myanswers.TestAnswers;
            }
            set
            {
                myanswers.TestAnswers = value;
                OnPropertyChanged("AnswersList");
            }
        }
        public ObservableCollection<TestTask> TasksList
        {
            get
            {
                return new ObservableCollection<TestTask>(work.WorkBody);
            }
            set
            {
                work.WorkBody = value.ToList();
                OnPropertyChanged("TasksList");
            }
        }






        public Command SendClick
        {
            get;
            set;
        }
        private async void Send_Click(object ob)
        {
            //var wwww = work;

            //answers.AnswerBody = new System.Collections.Generic.List<TestAnswerBody>();
            //for (int i = 0; i < work.WorkBody.Count; i++)
            //{
            //    answers.AnswerBody.Add(
            //        new TestAnswerBody
            //        {
            //            id_Task = work.WorkBody[i].ID,
            //            num_Answ = (work.WorkBody[i].selAnsws.IndexOf(true) + 1).ToString(),
            //        });

            //}
            //answers.AnswerHeader.DateTimeE = ToConvert.DB_DateTimeToStringDT(DateTime.Now);
            //answers.AnswerHeader.ID = await api.AddTestAnswerAsync(answers);
            ////  First.Base_frame.Navigate(new marks());
            ////открытие новой страницы с вводом логина и пароля 
        }

    }
}
