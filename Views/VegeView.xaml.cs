using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tadar.ViewModels;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для VegeView.xaml
    /// </summary>
    public partial class VegeView : Page
    {
       // public int count = 0;
        public VegeView()
        {
            InitializeComponent();
            DataContext = new VegeViewModel();
        }



        private void lbl1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lbl = (Label)sender;
            DragDrop.DoDragDrop(lbl, lbl, DragDropEffects.Move);
            DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);
        }

        void hyperlink_Click(object sender, RoutedEventArgs e)
        {
            //  Process.Start("https://www.flaticon.com/ru/"); //открытие ссылки в браузере
            Process.Start(new ProcessStartInfo("https://www.flaticon.com/ru/") { UseShellExecute = true });
        }

        //private void btn_Click(object sender, RoutedEventArgs e)
        //{

        //    DoubleAnimation wh = new DoubleAnimation();
        //    DoubleAnimation wh2 = new DoubleAnimation();
        //    DoubleAnimation hh = new DoubleAnimation();
        //    DoubleAnimation hh2 = new DoubleAnimation();
        //    string word = txtbox.Text;
        //    txtbox.Clear();
        //    switch (word)
        //    {
        //        //case "хур":
        //        //    lbl16.Visibility = Visibility.Hidden;
        //        //    wh.From = 0;
        //        //    wh.To = 87;
        //        //    wh.Duration = TimeSpan.FromSeconds(1);
        //        //    hh.From = 0;
        //        //    hh.To = 15;
        //        //    hh.Duration = TimeSpan.FromSeconds(1);
        //        //    hur.Visibility = Visibility.Visible;
        //        //    hur.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    hur.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");
        //        //    break;
        //        //case "харах1":
        //        //    lbl9.Visibility = Visibility.Hidden;
        //        //    yey.Visibility = Visibility.Visible;
        //        //    yey1.Visibility = Visibility.Visible;
        //        //    wh.From = 0;
        //        //    wh.To = 20;
        //        //    wh.Duration = TimeSpan.FromSeconds(1);
        //        //    yey.BeginAnimation(Ellipse.WidthProperty, wh);
        //        //    yey1.BeginAnimation(Ellipse.WidthProperty, wh);
        //        //    hh.From = 0;
        //        //    hh.To = 17;
        //        //    hh.Duration = TimeSpan.FromSeconds(1);
        //        //    yey.BeginAnimation(Ellipse.HeightProperty, hh);
        //        //    yey1.BeginAnimation(Ellipse.HeightProperty, hh);
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");
        //        //    break;
        //        //case "мойын1":
        //        //    lbl10.Visibility = Visibility.Hidden;
        //        //    wh.From = 0;
        //        //    wh.To = 30;
        //        //    wh.Duration = TimeSpan.FromSeconds(1);
        //        //    hh.From = 0;
        //        //    hh.To = 30;
        //        //    hh.Duration = TimeSpan.FromSeconds(1);
        //        //    unhe.Visibility = Visibility.Visible;
        //        //    unhe.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    unhe.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");
        //        //    break;
        //        //case "кöгiс1":
        //        //    lbl4.Visibility = Visibility.Hidden;
        //        //    wh.From = 0;
        //        //    wh.To = 90;
        //        //    wh.Duration = TimeSpan.FromSeconds(1);
        //        //    hh.From = 0;
        //        //    hh.To = 110;
        //        //    hh.Duration = TimeSpan.FromSeconds(1);
        //        //    body.Visibility = Visibility.Visible;
        //        //    body.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    body.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");
        //        //    break;
        //        //case "ахсы1":
        //        //    lbl15.Visibility = Visibility.Hidden;
        //        //    mot.Visibility = Visibility.Visible;
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");

        //        //    break;
        //        //case "пурун1":
        //        //    lbl14.Visibility = Visibility.Hidden;
        //        //    nos.Visibility = Visibility.Visible;
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");

        //        //    break;
        //        /////// шляпа
        //        //case "сiлепе":
        //        //    lbl7.Visibility = Visibility.Hidden;
        //        //    hat1.Visibility = Visibility.Visible;
        //        //    hat2.Visibility = Visibility.Visible;
        //        //    wh.From = 0;
        //        //    wh.To = 90;
        //        //    wh.Duration = TimeSpan.FromSeconds(1);
        //        //    wh2.From = 0;
        //        //    wh2.To = 48;
        //        //    wh2.Duration = TimeSpan.FromSeconds(1);
        //        //    hat1.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    hat2.BeginAnimation(Rectangle.WidthProperty, wh2);
        //        //    hh.From = 0;
        //        //    hh.To = 15;
        //        //    hh2.To = 40;
        //        //    hh.Duration = TimeSpan.FromSeconds(1);
        //        //    hh2.Duration = TimeSpan.FromSeconds(1);
        //        //    hat1.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    hat2.BeginAnimation(Rectangle.HeightProperty, hh2);
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");
        //        //    break;



        //        //////
        //        //case "ӧдiк":
        //        //    lbl11.Visibility = Visibility.Hidden;
        //        //    sho1.Visibility = Visibility.Visible;
        //        //    sho2.Visibility = Visibility.Visible;
        //        //    wh.From = 0;
        //        //    wh.To = 35;
        //        //    wh.Duration = TimeSpan.FromSeconds(1);
        //        //    sho1.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    sho2.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    hh.From = 0;
        //        //    hh.To = 55;
        //        //    hh.Duration = TimeSpan.FromSeconds(1);
        //        //    sho1.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    sho2.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");
        //        //    break;


        //        ////////
        //        //case "кöгенек":
        //        //    lbl3.Visibility = Visibility.Hidden;
        //        //    dress4.Visibility = Visibility.Visible;

        //        //    dress1.Visibility = Visibility.Visible;
        //        //    dress2.Visibility = Visibility.Visible;
        //        //    dress3.Visibility = Visibility.Visible;

        //        //    wh.From = 0;
        //        //    wh.To = 25;
        //        //    wh.Duration = TimeSpan.FromSeconds(1);
        //        //    wh2.From = 0;
        //        //    wh2.To = 85;
        //        //    wh2.Duration = TimeSpan.FromSeconds(1);

        //        //    dress1.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    dress2.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    dress3.BeginAnimation(Rectangle.WidthProperty, wh2);
        //        //    hh.From = 0;
        //        //    hh.To = 55;
        //        //    hh.Duration = TimeSpan.FromSeconds(1);
        //        //    dress1.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    dress2.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    dress3.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");
        //        //    break;



        //        //case "хулах1":
        //        //    lbl16.Visibility = Visibility.Hidden;
        //        //    uh1.Visibility = Visibility.Visible;
        //        //    uh2.Visibility = Visibility.Visible;
        //        //    wh.From = 0;
        //        //    wh.To = 10;
        //        //    wh.Duration = TimeSpan.FromSeconds(1);
        //        //    uh1.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    uh2.BeginAnimation(Rectangle.WidthProperty, wh);
        //        //    hh.From = 0;
        //        //    hh.To = 17;
        //        //    hh.Duration = TimeSpan.FromSeconds(1);
        //        //    uh1.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    uh2.BeginAnimation(Rectangle.HeightProperty, hh);
        //        //    count++;
        //        //    if (count == 10) MessageBox.Show("Вы одели человека!");
        //        //    break;

        //        default:
        //            MessageBox.Show("Неверное слово!");
        //            break;
        //    }
        //}



    }
}
