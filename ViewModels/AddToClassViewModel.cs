using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Helpers;
using nsAPI.Entities;
using nsAPI.Methods;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
   public class AddToClassViewModel : BaseViewModel
    {
        private List<RegisteredClassroom> classrooms = new List<RegisteredClassroom>();
        public AddToClassViewModel()
        {
            LoadClasssAsync();
            EnterClick = new Command(Enter_Click);
            ClassClick = new Command(Class_Click);

        }
        //SettingsFind settingsFind = new SettingsFind
        //{
        //    Shift = 1, // + 1 = 8
        //    Count = 15
        //};

        public async void LoadClasssAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                classrooms =
                  await api.GetAllClassess();
              //  ClasssList = new ObservableCollection<RegisteredClassroom>();
                OnPropertyChanged(nameof(classrooms));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // public ObservableCollection<RegisteredClassroom> ClasssList { get; set; }
        public List<RegisteredClassroom> Classrooms
        {
            get { return classrooms; }
            set
            {
                classrooms = value;
                OnPropertyChanged(nameof(Classrooms));
                // Задаем новый выбранный жлемент из списка.
               // SelectedClassroom = Classrooms[0];
            }
        }
        //public ObservableCollection<RegisteredClassroom> ClasssList
        //{
        //    get
        //    {
        //        return ObservableCollection<RegisteredClassroom>(classrooms);
        //    }
        //    set
        //    {
        //        classrooms = value.ToList();
        //        OnPropertyChanged("ClasssList");
        //    }
        //}

        public Command EnterClick { get; }
        private void Enter_Click(object ob)
        {
            First.Base_frame.Navigate(new Ent_page());

        }
        private void Class_Click(object ob)
        {
            First.Base_frame.Navigate(new DoClassPage());

        }

        public Command ClassClick
        {
            get;
            set;
        }
       
    }
}
