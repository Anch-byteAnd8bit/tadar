using System.Windows;
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
        public PerfomingWorkPage(nsAPI.Entities.TextWork work)
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
            Clipboard.SetText("ҷ");
            
        }

        private void SmNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ң");
           
        }

        private void SmFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ғ");
            
        }

        private void SmYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ӱ");
            
        }

        private void SmIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("і");
            
        }

        private void SmOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("ӧ");
            
        }

        private void BigOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ӧ");
            
        }

        private void BigIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("І");
            
        }

        private void BigYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ӱ");
            
        }

        private void BigFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ғ");
            

        }

        private void BigNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ң");
            
        }

        private void BigJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText("Ҷ");
           
        }

    }
}
