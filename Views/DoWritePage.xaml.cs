using nsAPI.Entities;
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
using Tadar.Helpers;
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для DoWritePage.xaml
    /// </summary>
    public partial class DoWritePage : Page
    {
        private HakKeys hakKeys;
        public DoWritePage(bool nonmark, string nametest, string desctest, RegisteredClassroom classroom)
        {
            InitializeComponent();
            DataContext = new DoWriteViewModel(nonmark, nametest, desctest, classroom);

            // logbox.Focus();


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
