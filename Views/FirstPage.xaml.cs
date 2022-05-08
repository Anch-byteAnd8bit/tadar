using Helpers;
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
            try
            {
                //await api.UserRegAsync(me);
                if (api.MainUser==null) await api.UserAuthAsync(me.UserForAuthorization);
                /*var twfa = new TestWorkForAdd
                {
                    WorkHeader = new WorkHeader
                    {
                        IdJournal = "1",
                        DateTimeCreate = DateTime.Now.ToString("d"),
                        DateTimeStart = DateTime.Now.ToString("d"),
                        Description = "Эта ТЕСТОВАЯ работа создана из клиента!",
                        IdTypeWork = "1",
                        IsNonMark = "0",
                        MaxDuration = "60",
                        Name = "Тестовая работа, созданная в клиенте"
                    },
                    WorkBody = new List<TestTask>()
                    {
                        new TestTask {
                            IdTest = "1",
                            NumTask = "1",
                            PossibleAnsw1 = "A",
                            PossibleAnsw2 = "B",
                            PossibleAnsw3 = "C",
                            PossibleAnsw4 = "D",
                            RightNum = "1",
                            Word = "World",
                        }
                    }
                };
                
                var id = await api.AddTestWorkAsync(twfa);*/
                var textWork = new TextWorkForAdd
                {
                    WorkHeader = new WorkHeader
                    {
                        IdJournal = "1",
                        DateTimeCreate = DateTime.Now.ToString("d"),
                        DateTimeStart = DateTime.Now.ToString("d"),
                        Description = "Эта ТЕКСТОВАЯ работа создана из клиента!",
                        IdTypeWork = "2",
                        IsNonMark = "1",
                        MaxDuration = "25",
                        Name = "ПРосто ТеКстовая работа"
                    },
                    WorkBody = new List<TextTask>()
                    {
                        new TextTask {
                            IdTest = "1",
                            TaskTitle = "Заголовок текстовой работы",
                            TaskText = "Содержвние текстовой работы",
                        }
                    }
                };
                var id = await api.AddTextWorkAsync(textWork);

                _ = MessageBox.Show(id);
                Works works = await api.GetWorksByJournal("1", false);
                _ = MessageBox.Show("TestWorks: " + 
                    string.Join(", ", works.TestWorks.Select(w => w.WorkHeader.Name)) +
                    "; \nTextWorks: " +
                    string.Join(", ", works.TextWorks.Select(w => w.WorkHeader.Name)));
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
