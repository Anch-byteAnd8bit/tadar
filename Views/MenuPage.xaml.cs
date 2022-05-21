using System.Windows;
using System.Windows.Controls;
using Tadar.Models;
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для Menu_Page.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            DataContext = new MenuViewModel();
        }

        private void TextBlock_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {

        }
    }
}
