using System.Windows;
using System.Windows.Controls;
using Tadar.Models;
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для Menu_Page.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            DataContext = new MenuViewModel();
        }



    private void akk_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new Ent_page());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void test_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new ListTest_Page());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void selftest_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new Ent_page());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void game_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new Ent_page());
            //открытие новой страницы с вводом логина и пароля 
        }
        //private void mark_Click(object sender, RoutedEventArgs e)
        //{
        //    First.Base_frame.Navigate(new marks());
        //    //открытие новой страницы с вводом логина и пароля 
        //}
        private void make_test_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new DoWorkPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void class_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new AddToClassPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        
    }
}
