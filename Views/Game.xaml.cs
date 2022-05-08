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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tadar.Views
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public Game()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            
            DoubleAnimation wh = new DoubleAnimation();
            DoubleAnimation hh = new DoubleAnimation();
            DoubleAnimation hh2 = new DoubleAnimation();
            string word = txtbox.Text;
            txtbox.Clear();
            switch (word)
            {
                case "пас":
                    lbl1.Visibility = Visibility.Hidden;
                    wh.From = 0;
                    wh.To = 80;
                    wh.Duration = TimeSpan.FromSeconds(1);
                    hh.From = 0;
                    hh.To = 80;
                    hh.Duration = TimeSpan.FromSeconds(1);
                    head.Visibility=Visibility.Visible;
                    head.BeginAnimation(Ellipse.WidthProperty, wh);
                    head.BeginAnimation(Ellipse.HeightProperty, hh);
                    break;
                case "харах":
                    yey.Visibility = Visibility.Visible;
                    yey1.Visibility = Visibility.Visible;
                    wh.From = 0;
                    wh.To = 20;
                    wh.Duration = TimeSpan.FromSeconds(1);
                    yey.BeginAnimation(Ellipse.WidthProperty, wh);
                    yey1.BeginAnimation(Ellipse.WidthProperty, wh);
                    hh.From = 0;
                    hh.To = 17;
                    hh.Duration = TimeSpan.FromSeconds(1);
                    yey.BeginAnimation(Ellipse.HeightProperty, hh);
                    yey1.BeginAnimation(Ellipse.HeightProperty, hh);
                    break;
                case "мойын":
                    lbl2.Visibility = Visibility.Hidden;
                    wh.From = 0;
                    wh.To = 30;
                    wh.Duration = TimeSpan.FromSeconds(1);
                    hh.From = 0;
                    hh.To = 30;
                    hh.Duration = TimeSpan.FromSeconds(1);
                    unhe.Visibility = Visibility.Visible;
                    unhe.BeginAnimation(Rectangle.WidthProperty, wh);
                    unhe.BeginAnimation(Rectangle.HeightProperty, hh);
                    break;
                case "кöгiс":
                    wh.From = 0;
                    wh.To = 90;
                    wh.Duration = TimeSpan.FromSeconds(1);
                    hh.From = 0;
                    hh.To = 110;
                    hh.Duration = TimeSpan.FromSeconds(1);
                    body.Visibility = Visibility.Visible;
                    body.BeginAnimation(Rectangle.WidthProperty, wh);
                    body.BeginAnimation(Rectangle.HeightProperty, hh);
                    break;
                case "ахсы":
                    mot.Visibility = Visibility.Visible;
                    
                    break;
                case "пурун":
                    nos.Visibility = Visibility.Visible;

                    break;
                case "сас":
                    hair.Visibility = Visibility.Visible;
                    hair2.Visibility = Visibility.Visible;
                    wh.From = 0;
                    wh.To = 60;
                    wh.Duration = TimeSpan.FromSeconds(1);
                    hair.BeginAnimation(Rectangle.WidthProperty, wh);
                    hair2.BeginAnimation(Rectangle.WidthProperty, wh);
                    hh.From = 0;
                    hh.To = 25;
                    hh2.To = 45;
                    hh.Duration = TimeSpan.FromSeconds(1);
                    hh2.Duration = TimeSpan.FromSeconds(1);
                    hair.BeginAnimation(Rectangle.HeightProperty, hh);
                    hair2.BeginAnimation(Rectangle.HeightProperty, hh2);
                    break;

                default:
                    MessageBox.Show("Неверное слово!");
                    break;
                    }
        }


        private void lbl1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lbl = (Label)sender;
            DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);
        }

        //private void txtTarget_Drop(object sender, DragEventArgs e)
        //{
        //    ((TextBlock)sender).Text = (string)e.Data.GetData(DataFormats.Text);
        //}






    }





}
