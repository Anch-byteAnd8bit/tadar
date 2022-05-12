using Helpers;
using nsAPI;
using nsAPI.Entities;
using nsAPI.Helpers;
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
    public class Do14ViewModel : BaseViewModel
    {
        private TestWork test =new TestWork();

        public Do14ViewModel(bool nonmark, string nametest, string desctest, RegisteredClassroom classroom)
        {
            string mark;
            if (nonmark)
            {
                mark = "1";
            }
            else mark = "0";

           
            test.WorkHeader = new WorkHeader
            {
                Description = desctest,
                IsNonMark = mark,
                Name = nametest,
                id_Class = classroom.ID,
                DateTimeCreate = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                DateTimeStart = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                MaxDuration = "35"

            };
            test.WorkBody = new List<TestTask>
            {
                new TestTask
                {

                     NumTask="1",
                     PossibleAnsw1="",
                     PossibleAnsw2="",
                     PossibleAnsw3="",
                     RightNum="",
                     PossibleAnsw4="",
                     Word=""

                },
                new TestTask
                {

                     NumTask="2",
                     PossibleAnsw1="",
                     PossibleAnsw2="",
                     PossibleAnsw3="",
                     RightNum="",
                     PossibleAnsw4="",
                     Word=""

                }

            };

            
            CreateClick = new Command(Create_Click);
            AddClick = new Command(Add_Click);
            

        }

        public ObservableCollection<TestTask> TasksList { 
            get
            {
                return new ObservableCollection<TestTask>(test.WorkBody);
            }
            set
            {
                test.WorkBody = value.ToList();
                OnPropertyChanged("TasksList");
            } 
        }
    


       
        private async void Create_Click(object ob)
        {
            // Во время любой операции с сервером может вылезти ошибка!
            try

            {
                TestWork work = new TestWork();
                work.WorkHeader = test.WorkHeader;
                work.WorkBody = test.WorkBody;
                TestWork testwork = new TestWork();
                await api.AddTestWorkAsync(work);
                MessageBox.Show(work.WorkHeader.ID + ": " + work.WorkHeader.Name + work.WorkBody[1].Word);

               

                First.Base_frame.Navigate(new marks());
            }
            // TODO: надо потом определять тип ошибки и выводить соотвествующие сообщения...
            catch (ErrorResponseException ex)
            {
                switch (ex.ErrCode)
                {
                    case CODE_ERROR.ERR_UserAlreadyReg:
                        MessageBox.Show("is registered");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }


           
            //открытие новой страницы с вводом логина и пароля 
        }
        public Command CreateClick
        {
            get;
            set;
        }
        private void Add_Click(object ob)
        {
            var t = new TestTask
            {

                NumTask = (test.WorkBody.Count + 1).ToString(),
                PossibleAnsw1 = "",
                PossibleAnsw2 = "",
                PossibleAnsw3 = "",
                RightNum = "",
                PossibleAnsw4 = "",
                Word = ""

            };
            test.WorkBody.Add(t);
            OnPropertyChanged("TasksList");

          
            

        }
        public Command AddClick
        {
            get;
            set;
        }







    }
    }

   

