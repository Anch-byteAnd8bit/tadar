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

            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add("1");
            document.Blocks.Add(paragraph);
            worktext.Document = document;
            worktext.Focus();
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
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ҷ");
            
            worktext.CaretPosition = worktext.Document.ContentEnd;
            worktext.Focus();
        }

        private void SmNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ң");
            worktext.CaretPosition = worktext.Document.ContentEnd;
        }

        private void SmFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ғ");
            worktext.CaretPosition = worktext.Document.ContentEnd;
        }

        private void SmYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ӱ");
            worktext.Focus();
        }

        private void SmIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("і");
            worktext.Focus();
        }

        private void SmOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("ӧ");
            worktext.Focus();
        }

        private void BigOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ӧ");
            worktext.Focus();
        }

        private void BigIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("І");
            worktext.Focus();
        }

        private void BigYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ӱ");
            worktext.Focus();
        }

        private void BigFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ғ");
            worktext.Focus();

        }

        private void BigNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ң");
            worktext.Focus();
        }

        private void BigJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (worktext.Document.Blocks.LastBlock as Paragraph).Inlines.Add("Ҷ");
            worktext.Focus();
        }
    }
}
