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

       
       


    }
}
