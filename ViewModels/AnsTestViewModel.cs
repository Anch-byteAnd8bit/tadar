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
        ObservableCollection<AnsAndWork> ansandwork = new ObservableCollection<AnsAndWork>();
        private int countrightansw = 0;

        //private Answers answers;
        public AnsTestViewModel(string iduser,TestWork test)
        {
            work = test;
           // OnPropertyChanged(nameof(TasksList));
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

                        
                        work.WorkBody.ForEach(wb =>
                            {
                                var answtask = myAnswers.TestAnswers[0].AnswerBody.FirstOrDefault(t => t.id_Task == wb.ID);
                                if (wb.RightNum == answtask.num_Answ) countrightansw++;


                            });
                        OnPropertyChanged(nameof(Countb));
                        // Msg.Write($"Правильно отвечено: {countrightansw} вопросов из {work.WorkBody.Count}. Оценку выставит учитель!");




                        // Я мог решить эту работу толлько один раз, значит ответ будет только
                        // один!! Берем поэтому первый элемент - это и есть мой ответ.
                        myanswer = myAnswers.TestAnswers[0];
                        //
                        var s = myanswer.AnswerHeader.Mark == "NULL" ? "0": myanswer.AnswerHeader.Mark;
                        Rate = int.Parse(s);
                    }
                    // Моих ответов нет...
                    else
                    {
                        // Что же делать???
                    }

                    //TODO: сохранять works, из него получать список заголовков ВСЕХ работ
                    for (int i = 0; i < myanswer.AnswerBody.Count; i++)
                    {
                        AnsAndWork anss = new AnsAndWork();

                        anss.NumTask = work.WorkBody[i].NumTask;
                        anss.Word = work.WorkBody[i].Word;
                        anss.AnsRightid = work.WorkBody[i].RightNum;
                        switch (anss.AnsRightid)
                        {
                            case "1":
                                anss.AnsRight = work.WorkBody[i].PossibleAnsw1;
                                    break;
                            case "2":
                                anss.AnsRight = work.WorkBody[i].PossibleAnsw2;
                                break;
                            case "3":
                                anss.AnsRight = work.WorkBody[i].PossibleAnsw3;
                                break;
                            case "4":
                                anss.AnsRight = work.WorkBody[i].PossibleAnsw4;
                                break;
                            default:
                                break;
                        }

                        anss.AnsStudid = myanswer.AnswerBody[i].num_Answ;
                        switch (anss.AnsStudid)
                        {
                            case "0":
                                anss.AnsStud = null;
                                break;
                            case "1":
                                anss.AnsStud = work.WorkBody[i].PossibleAnsw1;
                                break;
                            case "2":
                                anss.AnsStud = work.WorkBody[i].PossibleAnsw2;
                                break;
                            case "3":
                                anss.AnsStud = work.WorkBody[i].PossibleAnsw3;
                                break;
                            case "4":
                                anss.AnsStud = work.WorkBody[i].PossibleAnsw4;
                                break;
                            default:
                                break;
                        }
                        ansandwork.Add(anss);
                        OnPropertyChanged(nameof(AnswersList));
                    }
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


        public ObservableCollection<AnsAndWork> AnswersList
        {
            get
            {
                return ansandwork;
            }
        }

        //public List<TestAnswerBody> AnswersList
        //{
        //    get
        //    {
        //        return myanswer.AnswerBody;
        //    }
        //}
        //public ObservableCollection<TestTask> TasksList
        //{
        //    get
        //    {
        //        return new ObservableCollection<TestTask>(work.WorkBody);
        //    }
        //}

        /// <summary>
        /// Название работы.
        /// </summary>
        public string NameTest
        {
            get { return work.WorkHeader.Name; }
        }

        public string Countb
        {
            get { return countrightansw.ToString(); }
        }
        public string CountAll
        {
            get { return work.WorkBody.Count.ToString(); }
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
