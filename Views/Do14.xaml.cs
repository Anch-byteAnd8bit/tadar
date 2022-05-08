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
    /// Логика взаимодействия для Do14.xaml
    /// </summary>
    public partial class Do14 : Page
    {
        public Do14()
        {
            InitializeComponent();
            DataContext = new Do14ViewModel();
        
        }





        private void SmJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Paragraph p = worktext.Document
            // Blocks += "v";
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ҷ");

            //doworktext.CaretPosition = doworktext.Document.ContentEnd;
            //doworktext.Focus();
        }

        private void SmNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ң");
            //doworktext.CaretPosition = doworktext.Document.ContentEnd;
        }

        private void SmFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ғ");
            //doworktext.CaretPosition = doworktext.Document.ContentEnd;
        }

        private void SmYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ӱ");
            //doworktext.Focus();
        }

        private void SmIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("і");
            //doworktext.Focus();
        }

        private void SmOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ӧ");
            //doworktext.Focus();
        }

        private void BigOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ӧ");
            //doworktext.Focus();
        }

        private void BigIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("І");
            //doworktext.Focus();
        }

        private void BigYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ӱ");
            //doworktext.Focus();
        }

        private void BigFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ғ");
            //doworktext.Focus();

        }

        private void BigNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ң");
            //doworktext.Focus();
        }

        private void BigJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //(doworktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ҷ");
            //doworktext.Focus();
        }



    }



}
