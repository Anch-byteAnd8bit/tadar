﻿using System;
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
            Clipboard.SetText("ҷ");
           
            work.Focus();
        }

        private void SmNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ң");
            work.Focus();
        }

        private void SmFClick(object sender, System.Windows.RoutedEventArgs e)
        {
             Clipboard.SetText("ғ");
            work.Focus();
        }

        private void SmYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ӱ");
            work.Focus();
        }

        private void SmIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("і");
            work.Focus();
        }

        private void SmOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ӧ");
            work.Focus();
        }

        private void BigOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ӧ");
            work.Focus();
        }

        private void BigIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("І");
            work.Focus();
        }

        private void BigYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ӱ");
            work.Focus();
        }

        private void BigFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ғ");
            work.Focus();

        }

        private void BigNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ң");
            work.Focus();
        }

        private void BigJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ҷ");
            work.Focus();
        }



    }
}
