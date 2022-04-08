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

namespace tadar
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {

            //// регистрация добавление данных в бд и переход на новую страницу
            string famil = f_Box.Text;
            string name = name_box.Text;
            string secondname = secname_box.Text;
            string login = logbox.Text;
            string email = mailbox.Text;
            string password = pswbox.Password;
            //DateTime birthday = birth.SelectedDate();



        }

        void f_BoxTextChanged(object sender, TextChangedEventArgs e)
        {

            /*if (f_Box.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(@"D:/ХГУ/4 курс/проектдиплос/tadar/pic/famil.jpg", UriKind.Relative)
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.UniformToFill;
                // Use the brush to paint the button's background.
                f_Box.Background = textImageBrush;
            }
            else
            {

                f_Box.Background = Brushes.White;
                f_Box.Foreground = Brushes.Black;
            }*/
        }

        void name_boxTextChanged(object sender, TextChangedEventArgs e)
        {

            if (name_box.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(@"D:/ХГУ/4 курс/проектдиплос/tadar/pic/name101.jpg", UriKind.Relative)
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.UniformToFill;
                // Use the brush to paint the button's background.
                name_box.Background = textImageBrush;
            }
            else
            {

                name_box.Background = Brushes.White;
                name_box.Foreground = Brushes.Black;
            }




        }

        void secnameTextChanged(object sender, TextChangedEventArgs e)
        {

            if (secname_box.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(@"D:/ХГУ/4 курс/проектдиплос/tadar/pic/otches.jpg", UriKind.Relative)
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.UniformToFill;
                // Use the brush to paint the button's background.
                secname_box.Background = textImageBrush;
            }
            else
            {

                secname_box.Background = Brushes.White;
                secname_box.Foreground = Brushes.Black;
            }




        }
        void mailboxTextChanged(object sender, TextChangedEventArgs e)
        {

            if (mailbox.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(@"D:/ХГУ/4 курс/проектдиплос/tadar/pic/mail101.jpg", UriKind.Relative)
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.UniformToFill;
                // Use the brush to paint the button's background.
                mailbox.Background = textImageBrush;
            }
            else
            {

                mailbox.Background = Brushes.White;
                mailbox.Foreground = Brushes.Black;
            }




        }
        void logboxTextChanged(object sender, TextChangedEventArgs e)
        {

            if (logbox.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(@"D:/ХГУ/4 курс/проектдиплос/tadar/pic/login.jpg", UriKind.Relative)
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.UniformToFill;
                // Use the brush to paint the button's background.
                logbox.Background = textImageBrush;
            }
            else
            {

                logbox.Background = Brushes.White;
                logbox.Foreground = Brushes.Black;
            }




        }

        void pas_BoxTextChanged(Object sender, RoutedEventArgs args)
        {

            if (pswbox.Password == "")
            {
                    // Create an ImageBrush.
                    ImageBrush textImageBrush = new ImageBrush();
                    textImageBrush.ImageSource =
                        new BitmapImage(
                            new Uri(@"D:/ХГУ/4 курс/проектдиплос/tadar/pic/pass.jpg", UriKind.Relative)
                        );
                    textImageBrush.AlignmentX = AlignmentX.Left;
                    textImageBrush.Stretch = Stretch.UniformToFill;
                // Use the brush to paint the button's background.
                pswbox.Background = textImageBrush;
                }
                else
                {

                pswbox.Background = Brushes.White;
                pswbox.Foreground = Brushes.Black;
                }
            
        }

        private void f_Box_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = ""; 
        }

        private void f_Box_LostFocus(object sender, RoutedEventArgs e)
        {

            (sender as TextBox).Text = "123";
        }
    }
}
