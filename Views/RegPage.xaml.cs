using nsAPI;
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
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();
            DataContext = new RegViewModel();
        }

        /*private async void reg_ClickAsync(object sender, RoutedEventArgs e)
        {
            UserForRegistration userreg = new UserForRegistration();
            //// регистрация добавление данных в бд и переход на новую страницу
            userreg.Surname = f_Box.Text;
            userreg.Name = name_box.Text;
            userreg.Middlename = secname_box.Text;
            userreg.Login = logbox.Text;
            userreg.Email = mailbox.Text;
            userreg.Pass = pswbox.Password;
            if (birth.SelectedDate != null)
            {
                userreg.BDate = birth.Text;
            }
            
            else
            {
                birth.ToolTip = "Выберите дату рождения!";
                birth.BorderBrush = Brushes.Red;
            }
            if (gender.SelectedItem!=null)
            {
                if (gender.SelectedItem.ToString() == "Женский")
                {
                    userreg.GenderID = "1";
                }
                else
                {
                    userreg.GenderID = "2";
                }
                
            }
            else
            {
                gender.ToolTip = "Выберите пол!";
                gender.BorderBrush = Brushes.Red;
            }


            if (userreg.Surname.Length < 1)
            {
                f_Box.ToolTip = "Введите фамилию!";
                f_Box.BorderBrush = Brushes.Red;
            }
          else if (userreg.Pass.Length<6)
            {
                pswbox.ToolTip = "Слишком короткий пароль!";
                pswbox.BorderBrush = Brushes.Red;
            }
           else if (userreg.Name.Length<1)
            {
                name_box.ToolTip = "Введите имя!";
                name_box.BorderBrush = Brushes.Red;
            }
            else if (userreg.Middlename.Length < 1)
            {
                secname_box.ToolTip = "Введите отчество!";
                secname_box.BorderBrush = Brushes.Red;
            }
            else if (userreg.Email.Length < 5 || !userreg.Email.Contains("@") || !userreg.Email.Contains("."))
            {
                mailbox.ToolTip = "Введите e-mail!";
                mailbox.BorderBrush = Brushes.Red;
            }
            else if (userreg.Login.Length < 1 )
            {
               logbox.ToolTip = "Введите логин!";
               logbox.BorderBrush = Brushes.Red;
            }
            
            //else if (userreg.GenderID.SelectedItem==null)
            //{
            //    gender.ToolTip = "Выберите пол!";
            //    gender.BorderBrush = Brushes.Red;
            //}
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
               
                var res1 = await api.UserRegAsync(userreg);
                if (res1)
                {
                    _ = MessageBox.Show(api.MainUser.ID + ": " + api.MainUser.Login + " ("
                        + api.MainUser.Surname + " " + api.MainUser.Name + ")");
                }
                First.Base_frame.Navigate(new Menu_Page(userreg));
            }
        }*/
    }
}
