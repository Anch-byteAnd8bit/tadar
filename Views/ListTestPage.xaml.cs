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

namespace Tadar
{
    /// <summary>
    /// Логика взаимодействия для ListTest_Page.xaml
    /// </summary>
    public partial class ListTest_Page : Page
    {
        UserSecViewModel uservm;
        public ListTest_Page()
        {
            InitializeComponent();
            tascks.DataContext = uservm = new UserSecViewModel();
            //changetask.DataContext = uservm;
            //tas.DataContext = uservm = new usersecvm();

        }


        

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            {
                First.Base_frame.Navigate(new Work_Page());
                //открытие новой страницы с вводом логина и пароля 
            }
        }
    }
}
