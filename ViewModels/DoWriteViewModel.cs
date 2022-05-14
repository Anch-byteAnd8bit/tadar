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
    public class DoWriteViewModel : BaseViewModel
    {
        private TextWork textwork = new TextWork();
        public DoWriteViewModel(bool nonmark, string nametest, string desctest, RegisteredClassroom classroom)
        {

            string mark;
            if (nonmark)
            {
                mark = "1";
            }
            else mark = "0";


            textwork.WorkHeader = new WorkHeader
            {
                Description = desctest,
                IsNonMark = mark,
                Name = nametest,
                id_Class = classroom.ID,
                DateTimeCreate = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                DateTimeStart = ToConvert.DB_DateTimeToStringDT(DateTime.Now),
                MaxDuration = "35"

            };
            textwork.WorkBody = new List<TextTask>
            {
                new TextTask
                {

                    TaskTitle="",
                    TaskText=""

                }
                

            };


            CreateClick = new Command(Create_Click);

        }

        public ObservableCollection<TextTask> TasksList
        {
            get
            {
                return new ObservableCollection<TextTask>(textwork.WorkBody);
            }
            set
            {
                textwork.WorkBody = value.ToList();
                OnPropertyChanged("TasksList");
            }
        }

        private async void Create_Click(object ob)
        {
            // Во время любой операции с сервером может вылезти ошибка!
            try

            {
                TextWork work = new TextWork();
                work.WorkHeader = textwork.WorkHeader;
                work.WorkBody = textwork.WorkBody;
                //string testwork;
                work.WorkHeader.ID = await api.AddTextWorkAsync(work);
                MessageBox.Show(work.WorkHeader.ID + ": " + work.WorkHeader.Name + work.WorkBody[0].TaskTitle);



                First.Base_frame.Navigate(new MenuPage());
            }
            // TODO: надо потом определять тип ошибки и выводить соотвествующие сообщения...
            catch (Exception ex)
            {
                Msg.Write(ex);
            }

        }
        public Command CreateClick
        {
            get;
            set;
        }

        //  public Command EntCommand { get; set; }
        //public string Name
        //{
        //    // Получить.
        //    get => api.MainUser.Name;
        //    // Задать.
        //    set
        //    {
        //        api.MainUser.Name = value;
        //        // Уведомление.
        //        OnPropertyChanged(nameof(Name));
        //    }
        //}
        //public string Workname
        //{
        //    // Получить.
        //    get => api.MainUser.Name;
        //    // Задать.
        //    set
        //    {
        //        api.MainUser.Name = value;
        //        // Уведомление.
        //        OnPropertyChanged(nameof(Name));
        //    }
        //}

    }
}
