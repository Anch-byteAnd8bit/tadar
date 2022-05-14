using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;

namespace Tadar.ViewModels
{
    class TheoryThemeViewModel : BaseViewModel
    {
        private List <Theory> themes;
        public TheoryThemeViewModel()
        {

            //TestClick = new Command(Test_Click);
           // TextClick = new Command(Text_Click);
            LoadClasssAsync();


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
                List<string> idclass = new List<string>();
                for (int i = 0; i < classes.Count; i++)
                {
                    idclass.Add(classes[i].ID);
                    // nameclass[i] = classes[i].Name;

                }
                LoadTestsAsync(idclass);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void LoadTestsAsync(List<string> idclass)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }

                for (int i = 0; i < idclass.Count; i++)
                {
                   
                    themes = await api.GetTheoriesByClassroomIDAsync( idclass[i].ToString());
                }
                

            //    TextsList = new ObservableCollection<WorkHeader>();
            //    TestsList = new ObservableCollection<WorkHeader>();

            //    for (int i = 0; i < works.TestWorks.Count; i++)
            //    {
            //        TestsList.Add(works.TestWorks[i].WorkHeader);
            //    }

            //    for (int i = 0; i < works.TextWorks.Count; i++)
            //    {
            //        TextsList.Add(works.TextWorks[i].WorkHeader);
            //    }
            //    //TODO: сохранять works, из него получать список заголовков ВСЕХ работ
            //    OnPropertyChanged(nameof(TestsList));
            //    OnPropertyChanged(nameof(TextsList));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Вы не состоите ни в одном классе или в классе нет заданий!");
            }
        }

        public ObservableCollection<WorkHeader> TestsList { get; set; }

        //private void Test_Click(object ob)
        //{
        //    test = new TestWork();
        //    test.WorkHeader = (WorkHeader)ob;
        //    test.WorkBody = works.TestWorks.SingleOrDefault(w => w.WorkHeader == test.WorkHeader).WorkBody;
        //    First.Base_frame.Navigate(new Test14Page(test));

        //}
        //public Command TestClick
        //{
        //    get;

        //}

    }
}
