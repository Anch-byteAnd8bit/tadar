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
    /// Логика взаимодействия для DoTheoryPage.xaml
    /// </summary>
    /// 
    public partial class DoTheoryPage : Page
    {
        public DoTheoryPage()
        {
            InitializeComponent();
            DataContext = new DoTheoryViewModel();
        }


        private void SmJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Paragraph p = worktext.Document
            // Blocks += "v";
            Clipboard.SetText("ҷ");


        }

        private void SmNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ң");

        }

        private void SmFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ғ");

        }

        private void SmYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ӱ");

        }

        private void SmIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("і");

        }

        private void SmOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ӧ");

        }

        private void BigOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ӧ");

        }

        private void BigIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("І");

        }

        private void BigYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ӱ");

        }

        private void BigFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ғ");


        }

        private void BigNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ң");

        }

        private void BigJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ҷ");

        }



    }
}
