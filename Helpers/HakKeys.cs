using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tadar.Helpers
{
    /// <summary>
    /// Вспомогательный класс, для печати хакасских букв в текстовых полях под фокусом.
    /// </summary>
    public class HakKeys
    {
        //private readonly string altJSm = "ӌ";
        //private readonly string altJLg = "Ӌ";
        //private readonly string altILg = "І";
        //private readonly string altISm = "і";

        private readonly string CharsLg = "АБВГҒДЕЁЖЗИЙIКЛМНҢОӦПРСТУӰФХЦЧҶШЩЪЫЬЭЮЯ";
        private readonly string CharsSm = "абвгғдеёжзийiклмнңоӧпрстуӱфхцчҷшщъыьэюя";
        /// <summary>
        /// Список кнопок с буквами.
        /// </summary>
        private List<Button> charButtons;
        /// <summary>
        /// Флаг, указывающий на то, что шифт зажата.
        /// </summary>
        bool shiftPressed = false;
        /// <summary>
        /// Если true - то при зажатой клавише Шифт будут печататься только большие буквы.
        /// </summary>
        public bool ShiftIsLarge = false;
        /// <summary>
        /// Хранит поле, которое стало под фокусом.
        /// </summary>
        private TextBox FocusedTextBox;

        /// <summary>
        /// Создать экземпляр объекта с доп.клавишами хакасских букв.
        /// </summary>
        public HakKeys()
        {
            charButtons = new List<Button>();
        }

        /// <summary>
        /// Вызывается главным окном при отпускании клавиши в программе.
        /// </summary>
        public void this_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shiftPressed = false;
            }
        }
        /// <summary>
        /// Вызывается главным окном при нажатии клавиши в программе.
        /// </summary>
        public void this_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shiftPressed = true;
            }
        }

        /// <summary>
        /// Текстовое поле обрело фокус.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SomeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is not TextBox txtb) return;
            // Запоминаем это поле.
            FocusedTextBox = txtb;
            charButtons.ForEach(b => b.IsEnabled = true);
        }

        /// <summary>
        /// Текстовое поле потеряло фокус.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SomeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FocusedTextBox == sender as TextBox)
            {
                FocusedTextBox = null;
                charButtons.ForEach(b => b.IsEnabled = false);
            }
        }

        /// <summary>
        /// Добавляет строку "s" к элементу под фокусом.
        /// </summary>
        /// <param name="c"></param>
        private void AddStrToFocusedText(string s)
        {
            // Если ни одно поле не было выбрано, то ничего никуда не печатаем.
            if (FocusedTextBox == null) return;
            // Добавляем новый хакаский символ.
            FocusedTextBox.Text += s;
            // Перемещаем курсор в конце текста.
            FocusedTextBox.SelectionStart = FocusedTextBox.Text.Length;
        }

        /// <summary>
        /// Вызывается, когда нажали кнопку с хакасской буквой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CharButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is not Button btn) return;
            if (btn.Content is not string btnText) return;

            char ch = btnText[0];
            if (ShiftIsLarge && shiftPressed)
            {
                var cc = CharsSm.IndexOf(ch);
                if (cc!=-1)
                {
                    ch = CharsLg[cc];
                }
            }
            //if (!CharsSm.Contains(ch) && !CharsLg.Contains(ch) && !altJLg.Contains(ch) && !altJSm.Contains(ch)) return;

            AddStrToFocusedText(ch.ToString());
        }

        /// <summary>
        /// Для запоминания кнопки с хакасской буквой.
        /// </summary>
        /// <param name="btn"></param>
        public void AddCharButton(Button btn)
        {
            charButtons.Add(btn);
            btn.Click += CharButton_Click;
            btn.Focusable = false;
        }
    }
}
