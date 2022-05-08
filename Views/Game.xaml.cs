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
            string word = txtbox.Text;

            switch (word)
            {
                case "голова":
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
                case "глаз":
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
                case "шея":
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
                case "тело":
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

                default:
                    MessageBox.Show("Неверное слово!");
                    break;
                    }
        }

    }

   
        
    

}
