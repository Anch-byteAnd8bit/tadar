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

namespace Tadar
{
    /// <summary>
    /// Логика взаимодействия для Work_Page.xaml
    /// </summary>
    public partial class Work_Page : Page
    {
        UserSecViewModel uservm;
        public Work_Page()
        {
            InitializeComponent();
            tascks.DataContext = uservm = new UserSecViewModel();
            changetask.DataContext = uservm;
            tas.DataContext = uservm = new UserSecViewModel();
        }
    }
}
