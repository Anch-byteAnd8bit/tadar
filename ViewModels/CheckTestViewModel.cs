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
  public class CheckTestViewModel : BaseViewModel
    {

        private TestWork test;
        private TextWork text;
        private Works works;
        public CheckTestViewModel()
        {

            TestClick = new Command(Test_Click);
            TextClick = new Command(Text_Click);
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
                    await api.GetClassroomsByUserIdAsync(api.MainUser.ID,"3");
                List<string> idjourn = new List<string>();
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

                TextsList = new ObservableCollection<WorkHeader>();
                TestsList = new ObservableCollection<WorkHeader>();

                for (int i = 0; i < works.TestWorks.Count; i++)
                {
                    if (works.TestWorks[i].WorkHeader.IsNonMark == "0")
                    {
                        TestsList.Add(works.TestWorks[i].WorkHeader);
                    }
                }

                for (int i = 0; i < works.TextWorks.Count; i++)
                {
                    if (works.TextWorks[i].WorkHeader.IsNonMark == "0")
                    {
                        TextsList.Add(works.TextWorks[i].WorkHeader);
                    }
                }
                //TODO: сохранять works, из него получать список заголовков ВСЕХ работ
                OnPropertyChanged(nameof(TestsList));
                OnPropertyChanged(nameof(TextsList));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Вы не состоите ни в одном классе или в классе нет заданий!");
            }
        }
        public ObservableCollection<WorkHeader> TestsList { get; set; }

        public ObservableCollection<WorkHeader> TextsList { get; set; }


        private void Test_Click(object ob)
        {
            test = new TestWork();
            test.WorkHeader = (WorkHeader)ob;
            test.WorkBody = works.TestWorks.SingleOrDefault(w => w.WorkHeader == test.WorkHeader).WorkBody;
            First.Base_frame.Navigate(new ListStudAnsPage(test));

        }
        public Command TestClick
        {
            get;

        }
        private void Text_Click(object ob)
        {
            text = new TextWork();
            text.WorkHeader = (WorkHeader)ob;
            text.WorkBody = works.TextWorks.SingleOrDefault(w => w.WorkHeader == text.WorkHeader).WorkBody;
            First.Base_frame.Navigate(new PerfomingWorkPage(text));

        }
        public Command TextClick
        {
            get;

        }

    }
}
