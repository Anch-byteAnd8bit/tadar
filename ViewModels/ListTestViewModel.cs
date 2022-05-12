using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
   public class ListTestViewModel : BaseViewModel
    {
       private TestWork test;
        private Works works;
        
        private string[] nameclass = new string[] { };
        public ListTestViewModel()
        {
            //api.MainUser.Name
            TestClick = new Command(Test_Click);
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
                List<string> idjourn= new List<string>();
                for (int i = 0; i < classes.Count; i++)
                {
                    idjourn.Add(classes[i].ID);
                   // nameclass[i] = classes[i].Name;

                }
                LoadTestsAsync(idjourn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void LoadTestsAsync(List<string> idjourn)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                
                
                works = await api.GetWorksByClassesIDAsync(idjourn.ToArray(), false);
                
                TestsList = new ObservableCollection<WorkHeader>();
                for (int i = 0; i < works.TestWorks.Count; i++)
                {
                    TestsList.Add(works.TestWorks[i].WorkHeader) ;
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


        
        private void Test_Click(object ob)
        {
            test = new TestWork();
            test.WorkHeader = (WorkHeader)ob;
            test.WorkBody=works.TestWorks.SingleOrDefault(w => w.WorkHeader == test.WorkHeader).WorkBody;
            First.Base_frame.Navigate(new Test14Page(test));

        }
        public Command TestClick
        {
            get;
            
        }
        private void Test14_Click(object ob)
        {
            test = (TestWork)ob;
            First.Base_frame.Navigate(new PerfomingWorkPage(test));

        }
        public Command Test14Click
        {
            get;

        }

       // public ObservableCollection<WorkHeader> TestsList { }
        //{
        //    // Когда надо вернуть фамилию.
        //    get => TestsList;
        //    // Когда надо задать фамилию.
        //    set
        //    {
        //        // Присваиваем новое значение фамилии.
        //        TestsList = value;
        //        // Уведомляем форму, что свойство "Surname" изменилось.
        //        OnPropertyChanged(nameof(TestsList));
        //    }
        //}


        //public string Name
        //{
        //    // Когда надо вернуть фамилию.
        //    get => work.WorkHeader.Name;
        //    // Когда надо задать фамилию.
        //    set
        //    {
        //        // Присваиваем новое значение фамилии.
        //        work.WorkHeader.Name = value;
        //        // Уведомляем форму, что свойство "Surname" изменилось.
        //        OnPropertyChanged(nameof(Name));
        //    }
        //}
        //public string Desc
        //{
        //    // Получить.
        //    get => work.WorkHeader.Description;
        //    // Задать.
        //    set
        //    {
        //        work.WorkHeader.Description = value;
        //        // Уведомление.
        //        OnPropertyChanged(nameof(Desc));
        //    }
        //}

        

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
