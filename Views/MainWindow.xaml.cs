using System;
using System.Windows;
using Tadar.Models;

namespace Tadar.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Base_frame.Navigate(new FirstPage());
            First.Base_frame = Base_frame;
        }


        private void Back_click(object sender, RoutedEventArgs e)
        {
            First.Base_frame.GoBack();
        }

        private void Main_frame_content_rend(object sender, EventArgs e)
        {
            if (Base_frame.CanGoBack)
            {
                back_but.Visibility = Visibility.Visible;
            }
            else
            {
                back_but.Visibility = Visibility.Hidden;
            }
        }


    }
    
  
}
