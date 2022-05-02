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
using Tadar.Models;
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для ListTest_Page.xaml
    /// </summary>
    public partial class ListTest_Page : Page
    {
        
        public ListTest_Page()
        {
            InitializeComponent();
            DataContext = new ListTestViewModel();

        }

    
    }
}
