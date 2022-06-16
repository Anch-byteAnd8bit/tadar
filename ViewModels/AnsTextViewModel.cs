using Helpers;
using nsAPI;
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
   public class AnsTextViewModel: BaseViewModel
    {
        private TextWork work = new TextWork();
        private string idus;
        private Answers answers = new Answers();
        TextAnswer myanswer = new TextAnswer();
       // ObservableCollection<AnsAndWork> ansandwork = new ObservableCollection<AnsAndWork>();
        public AnsTextViewModel(string iduser, TextWork text)
        {
            work = text;
            // OnPropertyChanged(nameof(TasksList));
            idus = iduser;
            LoadAnsAsync(work, idus);
            SendClick = new Command(Send_Click);

        }
        public string NameTest
        {
            get { return work.WorkHeader.Name; }
        }

        public string NameTask
        {
            get { return work.WorkBody[0].TaskText; }
        }
        public string AnswerStu
        { //usr?.FullName??string.Empty;
            get { return myanswer?.AnswerBody?[0]?.AnswText??string.Empty; }
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
            answers.TextAnswers[0].AnswerHeader.Mark = Rate.ToString();
            // Отправляем оценку на работу.
            if (!await api.AddMark(answers.TextAnswers[0].AnswerHeader))
            {
                // Что-то пошло не так...
                Msg.Write("Не удалось сохранить оценку! :(");
            }

            First.Base_frame.Navigate(new MenuPage());
        }



        public async void LoadAnsAsync(TextWork work, string idus)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }

                // Получаем список ответов.
                answers = await api.GetAnswersByUser(idus, false);
                if (answers != null)
                {
                    // Получаем только мои ответы. Теперь это работает!!! (22.05.2022 00:04)
                    Answers myAnswers = answers.GetAnswersByIDUser(idus);

                    // А есть ли тут мои ответы?
                    if (myAnswers.TextAnswers.Count > 0)
                    {

                        // Я мог решить эту работу толлько один раз, значит ответ будет только
                        // один!! Берем поэтому первый элемент - это и есть мой ответ.
                        myanswer = myAnswers.TextAnswers[0];
                        OnPropertyChanged(nameof(AnswerStu));
                        //
                        var s = myanswer.AnswerHeader.Mark == "NULL" ? "0" : myanswer.AnswerHeader.Mark;
                        Rate = int.Parse(s);
                    }
                    // Моих ответов нет...
                    else
                    {
                        // Что же делать???
                    }

                    ////TODO: сохранять works, из него получать список заголовков ВСЕХ работ
                    //for (int i = 0; i < myanswer.AnswerBody.Count; i++)
                    //{
                    //    AnsAndWork anss = new AnsAndWork();

                    //    anss.Word = work.WorkBody[i].TaskText;
                    //    anss.AnsStud = myanswer.AnswerBody[i].AnswText;
                    //    ansandwork.Add(anss);
                    //    OnPropertyChanged(nameof(AnswersList));
                    //}
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


        //public ObservableCollection<AnsAndWork> AnswersList
        //{
        //    get
        //    {
        //        return ansandwork;
        //    }
        //}




    }
}
