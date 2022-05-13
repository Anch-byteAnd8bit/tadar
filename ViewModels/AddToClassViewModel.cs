﻿using System;
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
        private RegisteredClassroom cls;
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
       

        public Command EnterClick { get; }
        private async void Enter_Click(object ob)
        {
            //test = new TestWork();
            //test.WorkHeader = (WorkHeader)ob;
            //test.WorkBody = works.TestWorks.SingleOrDefault(w => w.WorkHeader == test.WorkHeader).WorkBody;
            //First.Base_frame.Navigate(new Test14Page(test));
            cls = new RegisteredClassroom();
            cls = (RegisteredClassroom)ob;
            string iclass = cls.ID;
            await api.AddStudent(api.MainUser.ID.ToString(), iclass);
            MessageBox.Show(api.MainUser.Name.ToString() + "в классе " + cls.Name.ToString());
            First.Base_frame.Navigate(new MenuPage());

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
