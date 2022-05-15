using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tadar.Helpers;

namespace Tadar.ViewModels
{
   public class AnsTestViewModel : BaseViewModel
    {
        private TestWork work;
        //private Answers answers;
        public AnsTestViewModel(TestWork test)
        {
            work = test;
            
            
            //SendClick = new Command(Send_Click);
            


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
