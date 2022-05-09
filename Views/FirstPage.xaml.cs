﻿using Helpers;
using nsAPI;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
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

            //First.Base_frame.Navigate(new Menu_Page());
        }
        private void list_Click(object sender, RoutedEventArgs e)
        {
            
            First.Base_frame.Navigate(new AddToClassPage());
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
            await api.UserRegAsync(userreg);
            _ = MessageBox.Show(api.MainUser.ID + ": " + api.MainUser.Login + " ("
                + api.MainUser.Surname + " " + api.MainUser.Name + ")");
            
            var res2 = await api.GetUsersByIdAsync(new string[] { "1", "5", "3" });
            if (res2 != null)
            {
                MessageBox.Show("Result: " + string.Join(" ", res2.Select(e=>e.Name)));
                // открытие страницы с вводом данных для регистрации
            }
            
            MessageBox.Show(string.Join(" ", (await api.GetGendersAsync()).Select(e => e.Name)));
        }

        private async void test_Click(object sender, RoutedEventArgs e)
        {
            UserForRegistration me = new UserForRegistration()
            {
                BDate = DateTime.Parse("31.10.1990").ToString("yyyy.MM.dd"),
                Email = "alex903_90@mail.ru",
                GenderID = "1",
                Login = "Alex",
                Middlename = "Рогачёв",
                Name = "Александр",
                Surname = "Леонидович",
                Pass = "123",
            };

            TextAnswerForAdd answer = new TextAnswerForAdd()
            {
                AnswerHeader = new AnswerHeader
                {
                    id_Student = "1",
                    id_TypeWork = "2", // text
                    id_RecordInJ = "65", // id work
                    Mark = null,
                    DateTimeS = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
                    DateTimeE = DateTime.Now.AddMinutes(30).ToString("yyyy.MM.dd HH:mm:ss"),
                },
                AnswerBody = new List<TextAnswerBody> {
                    new TextAnswerBody()
                    {
                        id_Task = "12",
                        AnswText = "Написал из клиента ответ на письменное задание. Вот..."
                    },
                }
            };

            try
            {
                //await api.UserRegAsync(me);
                if (api.MainUser==null) await api.UserAuthAsync(me.UserForAuthorization);

                Answers answers = await api.GetAnswersByWorks(new string[] { "64","65" }, false);
                _ = MessageBox.Show(answers.TestAnswers.Count.ToString() + " " + answers.TextAnswers.Count.ToString());
            }
            catch (UnknownHttpResponseException ex)
            {
                Log.Write(ex.ResponseJSON);
                _ = MessageBox.Show(ex.ResponseJSON);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                _ = MessageBox.Show(ex.Message);
            }
        }
    }
}
