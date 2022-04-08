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
using System.Windows.Shapes;

namespace tadar
{
    /// <summary>
    /// Логика взаимодействия для First_Window.xaml
    /// </summary>
    public partial class First_Window : Window
    {
        public First_Window()
        {
            InitializeComponent();
            //Frame_On.Navigate(new First_page());
            //First.Frame_On = Frame_On;
        }

        //private void back_click(object sender, RoutedEventArgs e)
        //{
        //    First.Frame_On.GoBack();
        //}
        //private void Main_frame_content_rend(object sender, EventArgs e)
        //{
        //    if (Frame_On.CanGoBack)
        //    {
        //        back2_but.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        back2_but.Visibility = Visibility.Hidden;
        //    }
        //}
    }
}
