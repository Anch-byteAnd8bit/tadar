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
using Tadar.Views;

namespace Tadar
{
    /// <summary>
    /// Логика взаимодействия для Base_page.xaml
    /// </summary>
    public partial class Ent_page : Page
    {
        public Ent_page()
        {
            InitializeComponent();
            DataContext = new EntViewModel();
        }
       //private void enter_Click(object sender, RoutedEventArgs e)
       // {
       //     Models.First.Base_frame.Navigate(new MenuPage());
       // }

        
        //} private void studrole(object sender, RoutedEventArgs e)
        //{
        //    if (studr.IsPressed)
        //    {

        //    }
        //}
    }
}
