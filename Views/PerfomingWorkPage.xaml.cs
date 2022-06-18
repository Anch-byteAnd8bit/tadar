using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Tadar.Helpers;
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для PerfomingWorkPage.xaml
    /// </summary>
    public partial class PerfomingWorkPage : Page
    {
        private HakKeys hakKeys;
        readonly PerfomingWorkViewModel perfomingWorkVM;
        public PerfomingWorkPage(nsAPI.Entities.TextWork work)
        {
            InitializeComponent();
            // Привязка контекста к элементам на форме.
            DataContext = perfomingWorkVM = new PerfomingWorkViewModel(work);

            hakKeys = new HakKeys();
            // Если надо, чтобы при зажатом Шифт, всегда печаталась большая буква.
            hakKeys.ShiftIsLarge = true;
            this.KeyDown += new KeyEventHandler(hakKeys.this_KeyDown);
            this.KeyUp += new KeyEventHandler(hakKeys.this_KeyUp);
            hakKeys.AddCharButton(btno); hakKeys.AddCharButton(btnO);
            hakKeys.AddCharButton(btni); hakKeys.AddCharButton(btnI);
            hakKeys.AddCharButton(btny); hakKeys.AddCharButton(btnY);
            hakKeys.AddCharButton(btnf); hakKeys.AddCharButton(btnF);
            hakKeys.AddCharButton(btnn); hakKeys.AddCharButton(btnN);
            hakKeys.AddCharButton(btnj); hakKeys.AddCharButton(btnJ);
        }

        private void SomeTextBox_GotFocus(object sender, RoutedEventArgs e) =>
            hakKeys.SomeTextBox_GotFocus(sender, e);

        private void SomeTextBox_LostFocus(object sender, RoutedEventArgs e) =>
            hakKeys.SomeTextBox_LostFocus(sender, e);

    }
}
