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
       private TestAnswerForAdd answers = new TestAnswerForAdd();

        public TestTask SelectedItem { get; set; }

        public ICommand SelectionChengedCommand { get; private set; }

        public Test14ViewModel(TestWork test)
        {
            work = test;
            //answers.AnswerHeader.DateTimeS = ToConvert.DB_DateTimeToStringDT(DateTime.Now);
            //answers.AnswerHeader.id_Student = api.MainUser.ID;
            //answers.AnswerHeader.id_Work = work.WorkHeader.ID;
            ////answers.AnswerHeader.id_TypeWork = work.WorkHeader.id_TypeWork;
            //answers.AnswerHeader.Mark = null;

            //AnswerClick = new Command(Answer_Click);
            //SendClick = new Command(Send_Click);
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

        //private async void Answer_Click(object ob)
        //{
            
        //    await api.AddTestAnswerAsync(answers);
        //    First.Base_frame.Navigate(new marks());
        //    //открытие новой страницы с вводом логина и пароля 
        //}

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
        //private async void Send_Click(object ob)
        //{
        //    answers.AnswerHeader.DateTimeE = ToConvert.DB_DateTimeToStringDT(DateTime.Now);
        //    //await api.AddTestAnswerAsync(answers);
        //    //  First.Base_frame.Navigate(new marks());
        //    //открытие новой страницы с вводом логина и пароля 
        //}



    }
}
