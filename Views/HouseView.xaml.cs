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
    /// Логика взаимодействия для HouseView.xaml
    /// </summary>
    public partial class HouseView : Page
    {
        public HouseView()
        {
            InitializeComponent();
            DataContext = new HouseViewModel();

        }

        private void lbl1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lbl = (Label)sender;
            DragDrop.DoDragDrop(lbl, lbl, DragDropEffects.Move);
            DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);
            //if (lbl.Content.ToString()=="windows"|| lbl.Content.ToString() == "door" || lbl.Content.ToString() == "house" || lbl.Content.ToString() == "pen")
            //{
            //    lbl.Visibility = Visibility.Hidden; }
        }
    }
}
