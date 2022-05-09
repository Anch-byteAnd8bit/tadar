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

        //private void TextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F)
        //    {
        //        f_Box.Select(f_Box.CaretIndex, 0);
        //        f_Box.Text+= "ң";
        //        f_Box.CaretIndex = f_Box.Text.Length;
        //    }
        //    else if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt && e.Key == Key.F)
        //    { 
        //        f_Box.Text += "Ң";
        //    } 
        //}



       



    }



}
