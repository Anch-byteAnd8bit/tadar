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
            if (birth.SelectedDate != null)
            {
                string birthday = birth.Text;
            }
            else
            {
                gender.ToolTip = "Выберите дату рождения!";
                gender.BorderBrush = Brushes.Red;
            }
            if (gender.SelectedItem!=null)
            {
                string gen = gender.SelectedItem.ToString();
            }
            else
            {
                gender.ToolTip = "Выберите пол!";
                gender.BorderBrush = Brushes.Red;
            }
           
           
           if (famil.Length<1)
            {
                f_Box.ToolTip = "Введите фамилию!";
                f_Box.BorderBrush = Brushes.Red;
            }
          else if (password.Length<6)
            {
                pswbox.ToolTip = "Слишком короткий пароль!";
                pswbox.BorderBrush = Brushes.Red;
            }
           else if (name.Length<1)
            {
                name_box.ToolTip = "Введите имя!";
                name_box.BorderBrush = Brushes.Red;
            }
            else if (secondname.Length < 1)
            {
                secname_box.ToolTip = "Введите отчество!";
                secname_box.BorderBrush = Brushes.Red;
            }
            else if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                mailbox.ToolTip = "Введите e-mail!";
                mailbox.BorderBrush = Brushes.Red;
            }
            else if (login.Length < 1 )
            {
               logbox.ToolTip = "Введите логин!";
               logbox.BorderBrush = Brushes.Red;
            }
            
            else if (gender.SelectedItem==null)
            {
                gender.ToolTip = "Выберите пол!";
                gender.BorderBrush = Brushes.Red;
            }
            else
            {
                f_Box.ToolTip = null;
                f_Box.BorderBrush = Brushes.Transparent;
                pswbox.ToolTip = null;
                pswbox.BorderBrush = Brushes.Transparent;
                name_box.ToolTip = null;
                name_box.BorderBrush = Brushes.Transparent;
                secname_box.ToolTip = null;
                secname_box.BorderBrush = Brushes.Transparent;
                mailbox.ToolTip = null;
                mailbox.BorderBrush = Brushes.Transparent;
                gender.ToolTip = null;
                gender.BorderBrush = Brushes.Red;
                birth.ToolTip = null;
               birth.BorderBrush = Brushes.Transparent;
                First.Base_frame.Navigate(new Menu_Page());
            }
           

        }

       
       


    }
}
