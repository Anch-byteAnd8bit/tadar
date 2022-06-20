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
    /// Логика взаимодействия для MyDictPage.xaml
    /// </summary>
    public partial class MyDictPage : Page
    {
        public MyDictPage()
        {
            InitializeComponent();
            DataContext = new MyDictViewModel();
        }
    }
}
