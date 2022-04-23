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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            
            Base_frame.Navigate(new First_page());
            First.Base_frame = Base_frame;
          
   
}


        private void back_click(object sender, RoutedEventArgs e)
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
