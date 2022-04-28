using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tadar.ViewModels;

namespace Tadar
{
    /// <summary>
    /// Логика взаимодействия для Menu_Page.xaml
    /// </summary>
    public partial class Menu_Page : Page
    {
        MenuViewModel menuViewModel;
        public Menu_Page()
        {
            InitializeComponent();
            fullname.DataContext = menuViewModel  = new MenuViewModel();
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
        private void mark_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new marks());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void make_test_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new DoWork_Page());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void class_Click(object sender, RoutedEventArgs e)
        {
            //First.Base_frame.Navigate(new wizard());
            //открытие новой страницы с вводом логина и пароля 
        }
        
    }
}
