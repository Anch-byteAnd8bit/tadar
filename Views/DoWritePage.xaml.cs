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

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для DoWritePage.xaml
    /// </summary>
    public partial class DoWritePage : Page
    {
        public DoWritePage()
        {
            InitializeComponent();
            DataContext = new DoWriteViewModel();

            // logbox.Focus();

           
        }


        private void SmJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Paragraph p = worktext.Document
            // Blocks += "v";
            work.Text += "ҷ";
            work.Focus();
        }

        private void SmNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "ң";
            work.Focus();
        }

        private void SmFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "ғ";
            work.Focus();
        }

        private void SmYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "ӱ";
            work.Focus();
        }

        private void SmIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "і";
            work.Focus();
        }

        private void SmOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "ӧ";
            work.Focus();
        }

        private void BigOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ӧ";
            work.Focus();
        }

        private void BigIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "І";
            work.Focus();
        }

        private void BigYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ӱ";
            work.Focus();
        }

        private void BigFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ғ";
            work.Focus();

        }

        private void BigNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ң";
            work.Focus();
        }

        private void BigJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ҷ";
            work.Focus();
        }



    }
}
