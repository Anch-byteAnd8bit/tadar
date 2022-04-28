using System.Windows.Controls;
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для PerfomingWorkPage.xaml
    /// </summary>
    public partial class PerfomingWorkPage : Page
    {
        readonly PerfomingWorkViewModel perfomingWorkVM;
        public PerfomingWorkPage()
        {
            InitializeComponent();
            // Привязка контекста к элементам на форме.
            DataContext = perfomingWorkVM = new PerfomingWorkViewModel();
            
            /*tasks.DataContext = perfomingWorkVM = new PerfomingWorkViewModel();
            changetask.DataContext = uservm;
            task.DataContext = uservm = new UserSecViewModel();*/
        }
    }
}
