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
    /// Логика взаимодействия для AddWordPage.xaml
    /// </summary>
    public partial class AddWordPage : Page
    {
        public AddWordPage()
        {
            InitializeComponent();
            DataContext = new AddWordViewModel();
            this.KeyDown += new KeyEventHandler(this_KeyDown);
            this.KeyUp += new KeyEventHandler(this_KeyUp);
        }

        private bool shiftPressed = false;

        // Вызывается главынм окном при отпускании клавиши в программе.
        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shiftPressed = false;
            }
        }
        // Вызывается главынм окном при нажатии клавиши в программе.
        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shiftPressed = true;
            }
        }

        /// <summary>
        /// Хранит поле, которое стало под фокусом.
        /// </summary>
        private TextBox FocusedTextBox;

        /// <summary>
        /// Вызывается когда поле для ввода получило фокус.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SomeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Запоминаем это поле.
            FocusedTextBox = sender as TextBox;
        }

        /// <summary>
        /// Добавляет строку "c" к элементу под фокусом.
        /// </summary>
        /// <param name="c"></param>
        private void AddStrToFocusedText(string c)
        {
            // Если ни одно поле не было выбрано, то ничего никуда не печатаем.
            if (FocusedTextBox == null) return;
            // Добавляем новый хакаский символ.
            FocusedTextBox.Text += c;
            // Перемещаем курсор в конце текста.
            FocusedTextBox.SelectionStart = FocusedTextBox.Text.Length;
        }

        private void SmJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("ҷ");

            // Если нажата клавиша "Шифт", то печатай большую букву.
            if (shiftPressed)
                AddStrToFocusedText("Ӌ");
            // Иначе маленькую.
            else
                AddStrToFocusedText("ҷ");

        }

        private void SmNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("ң");
            
            AddStrToFocusedText("ң");
        }

        private void SmFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("ғ");

            AddStrToFocusedText("ғ");
        }

        private void SmYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("ӱ");

            AddStrToFocusedText("ӱ");

        }

        private void SmIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("і");

            AddStrToFocusedText("і");

        }

        private void SmOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("ӧ");

            AddStrToFocusedText("ӧ");

        }

        private void BigOClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("Ӧ");
            AddStrToFocusedText("Ӧ");

        }

        private void BigIClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("І");
            AddStrToFocusedText("І");

        }

        private void BigYClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("Ӱ");
            AddStrToFocusedText("Ӱ");

        }

        private void BigFClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("Ғ");
            AddStrToFocusedText("Ғ");


        }

        private void BigNClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("Ң");
            AddStrToFocusedText("Ң");

        }

        private void BigJClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //Clipboard.SetText("Ҷ");
            AddStrToFocusedText("Ҷ");

        }
    }
}
