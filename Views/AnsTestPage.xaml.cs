using nsAPI.Entities;
using System.Windows.Controls;
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для AnsTestPage.xaml
    /// </summary>
    public partial class AnsTestPage : Page
    {
        public AnsTestPage(string iduser,TestWork test)
        {
            InitializeComponent();
            DataContext = new AnsTestViewModel(iduser,test);
        }
    }
}
