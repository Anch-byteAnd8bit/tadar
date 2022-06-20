using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для Test14Page.xaml
    /// </summary>
    public partial class Test14Page : Page
    {
        public Test14Page(TestWork work)
        {
            InitializeComponent();
            DataContext = new Test14ViewModel(work);
        }
        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ScrollBar.LineUpCommand.Execute(null, e.OriginalSource as IInputElement);
               
            }
            if (e.Delta < 0)
            {
                 ScrollBar.LineDownCommand.Execute(null, e.OriginalSource as IInputElement);
            }
            e.Handled = true;
        }
    }
}
