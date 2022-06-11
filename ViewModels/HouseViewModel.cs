using nsAPI;
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
   public class HouseViewModel: BaseViewModel
    {

        private int count = 0;
        private Visibility visi;
        public HouseViewModel()
        {
            api = API.Instance;
            //EntCommand = new Command(OnSave);
            //Vis = true;
            LoadClick = new Command(ppp);
        }

        
        public string word;

        public Visibility Vis
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Vis");
            }
        }
        
        public Visibility Peni
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Peni");
            }
        }
        public Visibility Sta
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Sta");
            }
        }


        public Visibility Doori
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Doori");
            }
        }
        public Visibility Wind
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Wind");
            }
        }

        public Visibility Curt
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Curt");
            }
        }
        public Visibility Hyr
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Hyr");
            }
        }

        public Visibility Fenc
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Fenc");
            }
        }

        public Visibility Gat
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Fenc");
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
                switch (word)
                {
                    case "тура":
                        var s = await api.GetImageByAlias("house");
                        if (s != null)
                        {
                            img = Helpers.Other.StreamToImageSource(s);
                            OnPropertyChanged("House");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Vis));
                            count++;
                            if (count == 9) MessageBox.Show("Вы собрали дом!");
                        }
                        break;
                    case "кӧзенек":
                        var d = await api.GetImageByAlias("win");
                        if (d != null)
                        {
                            img = Helpers.Other.StreamToImageSource(d);
                            OnPropertyChanged("Win");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Wind));
                            count++;
                            if (count == 9) MessageBox.Show("Вы собрали дом!");
                        }
                        break;
                    case "iзiк":
                        var t = await api.GetImageByAlias("door");
                        if (t != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t);
                            OnPropertyChanged("Door");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Doori));
                            count++;
                            if (count == 9) MessageBox.Show("Вы собрали дом!");
                        }
                        break;
                    case "пасхыс":
                        var tt = await api.GetImageByAlias("stairs");
                        if (tt != null)
                        {
                            img = Helpers.Other.StreamToImageSource(tt);
                            OnPropertyChanged("Stairs");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Sta));
                            count++;
                            if (count == 9) MessageBox.Show("Вы собрали дом!");
                        }
                        break;
                    case "iзiк тудазы":
                        var ttt = await api.GetImageByAlias("pen");
                        if (ttt != null)
                        {
                            img = Helpers.Other.StreamToImageSource(ttt);
                            OnPropertyChanged("Pen");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Peni));
                            count++;
                            if (count == 9) MessageBox.Show("Вы собрали дом!");
                        }
                        break;
                    case "кӧзеңе":
                        var w1 = await api.GetImageByAlias("curtain");
                        if (w1 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(w1);
                            OnPropertyChanged("Curti");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Curt));
                            count++;
                            if (count == 9) MessageBox.Show("Вы собрали дом!");
                        }
                        break;
                    case "хыр":
                        var w33 = await api.GetImageByAlias("hyr");
                        if (w33 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(w33);
                            OnPropertyChanged("Hyrr");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Hyr));
                            count++;
                            if (count == 9) MessageBox.Show("Вы собрали дом!");
                        }
                        break;
                    case "сиден":
                        var w4 = await api.GetImageByAlias("fence");
                        if (w4 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(w4);
                            OnPropertyChanged("Fence");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Fenc));
                            count++;
                            if (count == 9) MessageBox.Show("Вы собрали дом!");
                        }
                        break;
                    case "хаалха":
                        var w44 = await api.GetImageByAlias("gate");
                        if (w44 != null)
                        {
                            img = Helpers.Other.StreamToImageSource(w44);
                            OnPropertyChanged("Gate");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                            visi = Visibility.Hidden;
                            OnPropertyChanged(nameof(Gat));
                            count++;
                            if (count == 9) MessageBox.Show("Вы собрали дом!");
                        }
                        break;



                    default:
                        MessageBox.Show("Неверное слово!");
                        word = "";
                        OnPropertyChanged(nameof(Word));
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

        public ImageSource House
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Win
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Curti
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Door
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Stairs
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Pen
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Hyrr
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Fence
        {
            get { return img; }
            set { img = value; }
        }
        public ImageSource Gate
        {
            get { return img; }
            set { img = value; }
        }




    }
}
