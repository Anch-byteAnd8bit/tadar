using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
   public class ListTestViewModel : BaseViewModel
    {
       private TestWork work;
        
        private string[] nameclass = new string[] { };
        public ListTestViewModel()
        {
            //api.MainUser.Name
           // TestClick = new Command(Test_Click);
           // Test14Click = new Command(Test14_Click);
            LoadClasssAsync();
           // LoadTestsAsync();
           
        }
        public async void LoadClasssAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                List<RegisteredClassroom> classes =
                    await api.GetClassroomsByUserIdAsync(api.MainUser.ID);
                string[] idjourn= new string[] { };
                for (int i = 0; i < classes.Count - 1; i++)
                {
                    idjourn[i] = classes[i].ID;
                    nameclass[i] = classes[i].Name;

                }
                LoadTestsAsync(idjourn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void LoadTestsAsync(string[] idjourn)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                
                var work = await api.GetWorksByClassesIDAsync(idjourn, true);
                
                TestsList = new ObservableCollection<WorkHeader>();
                for (int i = 0; i < work.TestWorks.Count; i++)
                {
                    TestsList.Add(work.TestWorks[0].WorkHeader) ;
                   }
                //TODO: сохранять works, из него получать список заголовков ВСЕХ работ
                OnPropertyChanged(nameof(TestsList));
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }
        public ObservableCollection<WorkHeader> TestsList { get; set; }



        //private void Test_Click(object ob)
        //{
        //    work = (TestWork)ob;
        //    First.Base_frame.Navigate(new Test14Page(work));

        //}
        public Command TestClick
        {
            get;
            
        }
        private void Test14_Click(object ob)
        {
            work = (TestWork)ob;
            First.Base_frame.Navigate(new PerfomingWorkPage(work));

        }
        public Command Test14Click
        {
            get;

        }

        public string Name
        {
            // Когда надо вернуть фамилию.
            get => work.WorkHeader.Name;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                work.WorkHeader.Name = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Desc
        {
            // Получить.
            get => work.WorkHeader.Description;
            // Задать.
            set
            {
                work.WorkHeader.Description = value;
                // Уведомление.
                OnPropertyChanged(nameof(Desc));
            }
        }

        

            // public string NameCl
            //{

            //// Получить.
            //get => work.WorkHeader.id_Class
            
            //// Задать.
            //     set
            //{
            //    nameclass[i] = value;
            //    // Уведомление.
            //    OnPropertyChanged(nameof(NameCl));
            //}
            //}

    }
}
