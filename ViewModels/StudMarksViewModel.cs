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
    public class StudMarksViewModel : BaseViewModel
    {
        private ObservableCollection<WorkAndMark> marks = new ObservableCollection<WorkAndMark>();
        private RegisteredUser usr = new RegisteredUser();
        // private RegisteredClassroom cls;
        public StudMarksViewModel(string iclass, string istud)
        {
            LoadMarks(iclass, istud);
            LoadUser(istud);
            //EnterClick = new Command(Enter_Click);
            BackClick = new Command(Back_Click);

        }

        private void Back_Click(object ob)
        {
            First.Base_frame.Navigate(new MenuPage());

        }

        public Command BackClick
        {
            get;
            set;
        }
        public async void LoadUser(string istud)
        {
            try
            {
                usr = await api.GetUserByIdAsync(istud);
                OnPropertyChanged(nameof(FullName));
            }
            catch (Exception ex)
            {
                Msg.Write(ex.Message);
            }
           
        }


        public async void LoadMarks(string iclass, string istud)
        {
            try
            {
                    // Получаем список работ пользователя (только заголовки).
                    var works = await api.GetWorksByClassIDAsync(iclass, true);
                    if (works != null)
                    {
                        // Получаем массив идентификаторов классов пользователя.
                        var worksIDs = works.Headers.Select(wh => wh.ID).ToArray();
                        // Получаем список ответов на работы пользователя (только заголовки).
                        var userAnswers = await api.GetAnswersByUser(istud, true);
                        if (userAnswers != null)
                        {
                            // Получаем ответы только те, у которых есть оценки.
                            var resolvedAnswers = userAnswers.GetMarkedAnswers();
                            //
                            works = works.GetResolvedWorks(resolvedAnswers);
                            // Ищем ответы на тестовые задания.
                            foreach (var testAnswer in resolvedAnswers.TestAnswers)
                            {
                                foreach (var testWork in works.TestWorks)
                                {
                                    if (testAnswer.AnswerHeader.id_Work == testWork.WorkHeader.ID)
                                    {
                                        // Добавляем ответ-задание в список оценок.
                                        marks.Add(new WorkAndMark(testWork, testAnswer));
                                        OnPropertyChanged(nameof(Marks));
                                        break;
                                    }
                                }
                            }

                            // Ищем ответы на текстовые задания.
                            foreach (var textAnswer in resolvedAnswers.TextAnswers)
                            {
                                foreach (var textWork in works.TextWorks)
                                {
                                    if (textAnswer.AnswerHeader.id_Work == textWork.WorkHeader.ID)
                                    {
                                        // Добавляем ответ-задание в список оценок.
                                        marks.Add(new WorkAndMark(textWork, textAnswer));
                                        OnPropertyChanged(nameof(Marks));
                                        break;
                                    }
                                }
                            }
                        }
                        // Нет ответов у ученика.
                        else
                        {
                            if (api.LastException != null)
                            {
                                //Msg.Write(api.LastException.Message);
                                marks = new ObservableCollection<WorkAndMark>();
                                OnPropertyChanged(nameof(Marks));
                            }
                        }
                    }
                    else
                    {
                        if (api.LastException != null)
                        {
                            Msg.Write(api.LastException.Message);
                        }
                    }
                
                OnPropertyChanged(nameof(Marks));
            }
            catch (Exception ex)
            {
                Msg.Write(ex.Message);
            }
        }

        private string fullName;
        public string FullName
        {
            // Когда надо вернуть фамилию.
            get => usr?.FullName??string.Empty;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                fullName = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(FullName));
            }
        }


        public ObservableCollection<WorkAndMark> Marks
        {
            get { return marks; }
            //set
            //{
            //    marks = value;
            //    OnPropertyChanged(nameof(Marks));
            //    // Задаем новый выбранный элемент из списка.
            //    // SelectedMark = marks[0];
            //}
        }

        //public List<RegisteredUser> Classrooms
        //{
        //    get { return classroomsusers; }
        //    set
        //    {
        //        classroomsusers = value;
        //        OnPropertyChanged(nameof(Classrooms));
        //        // Задаем новый выбранный жлемент из списка.
        //        // SelectedClassroom = Classrooms[0];
        //    }
        //}


    }
}
