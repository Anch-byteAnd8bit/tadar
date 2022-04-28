﻿using nsAPI;
using nsAPI.Entities;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tadar.Models;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для First_page.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        API api;
        public FirstPage()
        {
            InitializeComponent();
            api = new API();
        }

        private void ent_Click(object sender, RoutedEventArgs e)
        {
            //открытие новой страницы с вводом логина и пароля 
            First.Base_frame.Navigate(new Ent_page());
        }
        private void reg_Click(object sender, RoutedEventArgs e)
        {
            //открытие новой страницы с вводом логина и пароля 
            First.Base_frame.Navigate(new Reg());
        }
        private void menu_Click(object sender, RoutedEventArgs e)
        {
            //открытие новой страницы с вводом логина и пароля 
            //First.Base_frame.Navigate(new Menu_Page());
        }

        /// <summary>
        /// Проверка работы API.
        /// </summary>
        private async void api_ClickAsync(object sender, RoutedEventArgs e)
        {

            UserForRegistration userreg = new UserForRegistration();
            userreg.Name = "Annushka";
            userreg.Surname = "Chu";
            userreg.Email = "asd@f.ru";
            userreg.GenderID = "1";
            userreg.Login = "deeeeeeep";
            userreg.Middlename = "fffs";
            userreg.Pass = "123";
            userreg.BDate = "2000.10.10";
            //var res1 = await api.UserRegAsync(ContentGenerator.getRandUFR());
            var res1 = await api.UserRegAsync(userreg);
            if (res1)
            {
                _ = MessageBox.Show(api.MainUser.ID + ": " + api.MainUser.Login + " ("
                    + api.MainUser.Surname + " " + api.MainUser.Name + ")");
            }
            var res2 = await api.GetUsersByIdAsync(new string[] { "1", "5", "3" });
            if (res2 != null)
            {
                MessageBox.Show("Result: " + string.Join(" ", res2.Select(e=>e.Name)));
                // открытие страницы с вводом данных для регистрации
            }
            
            MessageBox.Show(string.Join(" ", (await api.GetGendersAsync()).Select(e => e.Name)));
        }
    }
}