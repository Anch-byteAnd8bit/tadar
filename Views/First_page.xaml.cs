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

namespace tadar
{
    /// <summary>
    /// Логика взаимодействия для First_page.xaml
    /// </summary>
    public partial class First_page : Page
    {
        API _ApiRequest;
        public First_page()
        {
            InitializeComponent();
            _ApiRequest = new API("forstapitext");
        }

        private void ent_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new Ent_page());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void reg_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new Reg());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void menu_Click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.Navigate(new Menu_Page());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void api_Click(object sender, RoutedEventArgs e)
        {
            var res = _ApiRequest.GetInformation("", null);
            if (res!=null)
            {
                MessageBox.Show("Result: " + string.Join("", res.First()));
                // открытие страницы с вводом данных для регистрации
            }
        }
    }
}
