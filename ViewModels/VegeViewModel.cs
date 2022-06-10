using Helpers;
using nsAPI;
using nsAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Tadar.Helpers;

namespace Tadar.ViewModels
{
    public class VegeViewModel : BaseViewModel
    {
        private int count=0;
        private Visibility visi;
        public VegeViewModel()
        {
            api = API.Instance;
            //EntCommand = new Command(OnSave);
            LoadClick= new Command(ppp);
        }

        public string word;
        public Visibility Toma
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Toma");
            }
        }

        public Visibility Pota
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Pota");
            }
        }
        public Visibility Carr
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Carr");
            }
        }
        public Visibility Cucu
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Cucu");
            }
        }
        public Visibility Oni
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Oni");
            }
        }
        public Visibility Goro
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Goro");
            }
        }

        public Visibility Appl
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Appl");
            }
        }
        public Visibility Oduv
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Oduv");
            }
        }
        public Visibility Ryab
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Ryab");
            }
        }
        public Visibility Red
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Red");
            }
        }



        public string Word
        {
            // Когда надо вернуть фамилию.
            get => word;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
               word = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Word));
            }
        }
        
        private async void ppp(object ob)
        {
           

            {
                switch(word)
                {
                    case "моркоп":
                    var s = await api.GetImageByAlias("carrot");
                        if (s != null)
                        {
                            img = Helpers.Other.StreamToImageSource(s);
                            OnPropertyChanged(nameof(Carrot));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Carr));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;
                    case "яблах":
                        var d = await api.GetImageByAlias("potato");
                        if (d != null)
                        {
                            img = Helpers.Other.StreamToImageSource(d);
                            OnPropertyChanged(nameof(Potato));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Pota));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;
                    case "помидор":
                        var t = await api.GetImageByAlias("tomato");
                        if (t != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t);
                            OnPropertyChanged(nameof(Tomato));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Toma));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;
                    case "ӱгӱрсӱ":
                        var t111111 = await api.GetImageByAlias("cucum");
                        if (t111111 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t111111);
                            OnPropertyChanged(nameof(Cucum));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Cucu));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;
                    case "муксун":
                        var t11 = await api.GetImageByAlias("onion");
                        if (t11 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t11);
                            OnPropertyChanged(nameof(Onion));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Oni));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;
                    case "карох":
                        var t111 = await api.GetImageByAlias("gorok");
                        if (t111 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t111);
                            OnPropertyChanged(nameof(Gorok));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Goro));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;
                        
                        case "яблоко ағазы":
                        var t2 = await api.GetImageByAlias("apple");
                        if (t2 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t2);
                            OnPropertyChanged(nameof(Apple));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Appl));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;
                    case "мӱндӱргес":
                        var t22 = await api.GetImageByAlias("ryab");
                        if (t22 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t22);
                            OnPropertyChanged(nameof(Ryabi));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Ryab));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;
                    case "пача":
                        var t223 = await api.GetImageByAlias("oduv");
                        if (t223 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t223);
                            OnPropertyChanged(nameof(Oduvan));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Oduv));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;
                    case "cалғынах":
                        var t4 = await api.GetImageByAlias("redis");
                        if (t4 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t4);
                            OnPropertyChanged(nameof(Redis));
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Red));
                            count++;
                            if (count == 10) MessageBox.Show("Вы собрали огород!");
                        }
                        break;



                    default:
                        word = "";
                        OnPropertyChanged(nameof(Word));
                        MessageBox.Show("Неверное слово!");
                          break;

                }

                
            }
            //else
            //{
            //    Msg.Write(api.LastException.Message);
            //}
        }

        public Command LoadClick
        {
            get;
            set;
        }

        private ImageSource img;

        public ImageSource Carrot
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Potato
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Tomato
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Cucum
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Onion
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Gorok
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Apple
        {
            get { return img; }
            set { img = value; }
        }

        public ImageSource Oduvan
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Ryabi
        {
            get { return img; }
            set { img = value; }
        }

        public ImageSource Redis
        {
            get { return img; }
            set { img = value; }
        }



    }
}
