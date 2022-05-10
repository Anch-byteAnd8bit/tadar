using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для PerfomingWorkPage.xaml
    /// </summary>
    public partial class PerfomingWorkPage : Page
    {
        readonly PerfomingWorkViewModel perfomingWorkVM;
        public PerfomingWorkPage(nsAPI.Entities.RegisteredUser work)
        {
            InitializeComponent();
            // Привязка контекста к элементам на форме.
            DataContext = perfomingWorkVM = new PerfomingWorkViewModel(work);

            /*tasks.DataContext = perfomingWorkVM = new PerfomingWorkViewModel();
            changetask.DataContext = uservm;
            task.DataContext = uservm = new UserSecViewModel();*/

          
        }
        //public void newtext()
        // {
        //     //Paragraph paragraph = new Paragraph();
        //     //paragraph.Inlines.Add("2");
        //     (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("fu");//.Add(paragraph);

        // }
        private void SmJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Paragraph p = worktext.Document
            // Blocks += "v";
            work.Text += "ҷ";
            work.Focus();
        }

        private void SmNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "ң";
            work.Focus();
        }

        private void SmFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "ғ";
            work.Focus();
        }

        private void SmYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "ӱ";
            work.Focus();
        }

        private void SmIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "і";
            work.Focus();
        }

        private void SmOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "ӧ";
            work.Focus();
        }

        private void BigOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ӧ";
            work.Focus();
        }

        private void BigIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "І";
            work.Focus();
        }

        private void BigYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ӱ";
            work.Focus();
        }

        private void BigFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ғ";
            work.Focus();

        }

        private void BigNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ң";
            work.Focus();
        }

        private void BigJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            work.Text += "Ҷ";
            work.Focus();
        }

    }
}
