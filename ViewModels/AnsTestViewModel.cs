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
        TestAnswer myanswer = new TestAnswer();

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

                // Получаем список ответов.
                answers = await api.GetAnswersByWork(work.WorkHeader.ID, false);
                if (answers != null)
                {

                    // Получаем только мои ответы.
                    Answers myAnswers = answers.GetAnswersByIDUser(idus);
                    // А есть ли тут мои ответы?
                    if (myAnswers.TestAnswers.Count > 0)
                    {
                        // Я мог решить эту работу толлько один раз, значит ответ будет только
                        // один!! Берем поэтому первый элемент - это и есть мой ответ.
                        myanswer = myAnswers.TestAnswers[0];
                    }
                    // Моих ответов нет...
                    else
                    {
                        // Чтоже делать???
                    }

                    //TODO: сохранять works, из него получать список заголовков ВСЕХ работ
                    OnPropertyChanged(nameof(AnswersList));
                }
                else
                {
                    // Нет ответов - нет проблем... или есть??.... хм....
                }
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Вы не состоите ни в одном классе или в классе нет заданий!");
            }
        }

        public List<TestAnswerBody> AnswersList
        {
            get
            {
                return myanswer.AnswerBody;
            }
            //set
            //{
            //    myanswer.TestAnswers. = value;
            //    OnPropertyChanged("AnswersList");
            //}
        }
        public ObservableCollection<TestTask> TasksList
        {
            get
            {
                return new ObservableCollection<TestTask>(work.WorkBody);
            }
            //set
            //{
            //    work.WorkBody = value.ToList();
            //    OnPropertyChanged("TasksList");
            //}
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
