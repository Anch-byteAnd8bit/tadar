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

namespace Tadar
{
    /// <summary>
    /// Логика взаимодействия для Base_page.xaml
    /// </summary>
    public partial class Ent_page : Page
    {
        public Ent_page()
        {
            InitializeComponent();
        }
       private void enter_Click(object sender, RoutedEventArgs e)
        {
            //функция для входа в акк---проверка лоигна пароля сверка с бд и переход на другую страницу
            string login = logbox.Text;
            string password = pswbox.Password;
        //    if (studr.IsChecked==true)
        //    {
        //        studr.;
        }

        
        //} private void studrole(object sender, RoutedEventArgs e)
        //{
        //    if (studr.IsPressed)
        //    {

        //    }
        //}
    }
}
