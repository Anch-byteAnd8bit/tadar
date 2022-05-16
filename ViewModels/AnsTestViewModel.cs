using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

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
            SendClick = new Command(Send_Click);

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
                        //
                        Rate = int.Parse(myanswer.AnswerHeader.Mark);
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
            catch (Exception)
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
        }
        public ObservableCollection<TestTask> TasksList
        {
            get
            {
                return new ObservableCollection<TestTask>(work.WorkBody);
            }
        }

        /// <summary>
        /// Название работы.
        /// </summary>
        public string NameTest
        {
            get { return work.WorkHeader.Name; }
        }


        private int rate;
        /// <summary>
        /// Оценка работе.
        /// </summary>
        public int Rate
        {
            get { return rate; }
            set { rate = value; OnPropertyChanged(nameof(Rate)); }
        }

        public Command SendClick
        {
            get;
            set;
        }
        private async void Send_Click(object ob)
        {
            // Получаем оценку.
            answers.TestAnswers[0].AnswerHeader.Mark = Rate.ToString();
            // Отправляем оценку на работу.
            if (!await api.AddMark(answers.TestAnswers[0].AnswerHeader))
            {
                // Что-то пошло не так...
                Msg.Write("Не удалось сохранить оценку! :(");
            }

            First.Base_frame.Navigate(new MenuPage());
        }

    }
}
