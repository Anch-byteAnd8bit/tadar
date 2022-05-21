﻿using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
   public class ListStudAnsViewModel : BaseViewModel
    {
        private TestWork work;
        private RegisteredUser user;
        private List<RegisteredUser> classroomsusers = new List<RegisteredUser>();
        public ListStudAnsViewModel(TestWork test)
        {
            work = test;
            LoadAnsAsync(test);
            //LoadClassAsync(iclass);

            SendClick = new Command(Send_Click);



        }


        public Command SendClick
        {
            get;
            set;
        }
        private void Send_Click(object ob)
        {
            user = new RegisteredUser();
            user = (RegisteredUser)ob;
            string iduser = user.ID;
            First.Base_frame.Navigate(new AnsTestPage(iduser, work));
        }


        public async void LoadAnsAsync(TestWork test)
        {
            try
            {
                var ansbywork = await api.GetAnswersByWork(test.WorkHeader.ID);
                List <string> usersid = new List<string> { };


                if (ansbywork != null)
                {
                    for (int i = 0; i < ansbywork.TestAnswers.Count; i++)
                    {
                        usersid.Add(ansbywork.TestAnswers[i].AnswerHeader.id_Student);

                    }
                    LoadClassAsync(usersid);
                }
                else

                    MessageBox.Show("Никто не решал этот тест");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Асинхронная загрузка списка классов указанного пользователя,
        /// чтобы получить потом его работы.
        /// </summary>
        /// <param name="iclass"></param>
        public async void LoadClassAsync(List<string> iclass)
        {
            try
            {
                classroomsusers = await api.GetUsersByIdAsync(iclass.ToArray());

                //  ClasssList = new ObservableCollection<RegisteredClassroom>();

                OnPropertyChanged(nameof(Classrooms));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<RegisteredUser> Classrooms
        {
            get { return classroomsusers; }
            set
            {
                classroomsusers = value;
                OnPropertyChanged(nameof(Classrooms));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }


    }
}
