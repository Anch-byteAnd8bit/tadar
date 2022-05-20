using Helpers;
using nsAPI.Entities;
using nsAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    /// <summary>
    /// Класс для работы со страницей прохождения тестирования.
    /// </summary>
    public class PerfomingWorkViewModel : BaseViewModel
    {
        private TextWork work;
        private TextAnswerForAdd answers;
        public PerfomingWorkViewModel(TextWork work)
        {
            this.work = work;

            answers = new TextAnswerForAdd
            {
                AnswerHeader = new AnswerHeader
                {
                    //ID
                    //id_TypeWork = "2", <- не надо заполнять.
                    id_Work = work.WorkHeader.ID, // ИД работы.
                    DateTimeS = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    //DateTimeE = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                    id_UserInClasses = api.MainUser.ID,
                    Mark = null
                },
                AnswerBody = new List<TextAnswerBody>
                {
                    new TextAnswerBody
                    {
                         // ИД задачи (хранятся в списке тела работы.
                        AnswText = "",id_Task="1"
                    }
                }
            };
            
            LoadClick = new Command(Load_Click);
            //LoadUsersAsync();
        }

        private async void Load_Click(object ob)
        {
            answers.AnswerHeader.DateTimeE = ToConvert.DB_DateTimeToStringDT(DateTime.Now);

            answers.AnswerHeader.ID = await api.AddTextAnswerAsync(answers);
            if (answers.AnswerHeader.ID != null)
            {
                Msg.Write("Ответ отправлен!");
            }
            else
            {
                Msg.Write(api.LastException.Message);
            }
            First.Base_frame.Navigate(new MenuPage());
            //открытие новой страницы с вводом логина и пароля 



        }

        public string Answer
        {
            // Когда надо вернуть фамилию.
            get => answers.AnswerBody[0].AnswText;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                answers.AnswerBody[0].AnswText = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Answer));
            }
        }


        public Command LoadClick
        {
            get;
            set;
        }
       
        //public ObservableCollection<RegisteredUser> TestList { get; set; }


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
        public string Title
        {
            // Когда надо вернуть фамилию.
            get => work.WorkBody[0].TaskTitle;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                work.WorkBody[0].TaskTitle = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Body
        {
            // Когда надо вернуть фамилию.
            get => work.WorkBody[0].TaskText;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                work.WorkBody[0].TaskText = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Body));
            }
        }



    }
}
