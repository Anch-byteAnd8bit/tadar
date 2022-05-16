using Helpers;
using nsAPI.Entities;
using nsAPI.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
   public class Test14ViewModel: BaseViewModel
    {
       private TestWork work;
       private TestAnswerForAdd answer = new TestAnswerForAdd();

        public TestTask SelectedItem { get; set; }

        public ICommand SelectionChengedCommand { get; private set; }

        public Test14ViewModel(TestWork test)
        {
            work = test;
            answer.AnswerHeader = new AnswerHeader();
            answer.AnswerHeader.DateTimeS = ToConvert.DB_DateTimeToStringDT(DateTime.Now);
            answer.AnswerHeader.id_Student = api.MainUser.ID;
            answer.AnswerHeader.id_Work = work.WorkHeader.ID;
            //answers.AnswerHeader.id_TypeWork = work.WorkHeader.id_TypeWork;
            answer.AnswerHeader.Mark = null;
           
           // AnswerClick = new Command(Answer_Click);
            SendClick = new Command(Send_Click);
            SelectionChengedCommand = new RelayCommand<TestTask>(SelectionChenged);
            

        }
        private void SelectionChenged(TestTask item) => SelectedItem = item;


        public string NameTest
        {
            // Когда надо вернуть фамилию.
            get => work.WorkHeader.Name;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                work.WorkHeader.Name = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(NameTest));
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
                work.WorkBody= value.ToList();
                OnPropertyChanged("TasksList");
            }
        }

        

        public Command AnswerClick
        {
            get;
            set;
        }
        public Command SendClick
        {
            get;
            set;
        }
        private async void Send_Click(object ob)
        {
            answer.AnswerBody = new System.Collections.Generic.List<TestAnswerBody>();
            for (int i = 0; i < work.WorkBody.Count; i++)
            {
                answer.AnswerBody.Add(
                    new TestAnswerBody
                    {
                        id_Task = work.WorkBody[i].ID,
                        num_Answ = (work.WorkBody[i].selAnsws.IndexOf(true) + 1).ToString(),
                    });
            }
            answer.AnswerHeader.DateTimeE = ToConvert.DB_DateTimeToStringDT(DateTime.Now);
            answer.AnswerHeader.ID = await api.AddTestAnswerAsync(answer);
            //Success
            int countrightansw = 0;
            if (answer.AnswerHeader.ID != null)
            {
                    work.WorkBody.ForEach(wb =>
                    {
                        var answtask = answer.AnswerBody.FirstOrDefault(t => t.id_Task == wb.ID);
                        if (wb.RightNum == answtask.num_Answ) countrightansw++;

                    });
                Msg.Write($"Правильно отвечено: {countrightansw} вопросов из {work.WorkBody.Count}. Оценку выставит учитель!");
            }
            // Fail
            else
            {
                if (api.LastException != null)
                    Msg.Write(api.LastException.Message);
            }
          //  answer.AnswerHeader.DateTimeE = ToConvert.DB_DateTimeToStringDT(DateTime.Now);
          //answer.AnswerHeader.ID =  await api.AddTestAnswerAsync(answer);
            First.Base_frame.Navigate(new MenuPage());
            //открытие новой страницы с вводом логина и пароля 
        }



    }
}
