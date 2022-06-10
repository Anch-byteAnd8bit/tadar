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

        public HouseViewModel()
        {
            api = API.Instance;
            //EntCommand = new Command(OnSave);
            LoadClick = new Command(ppp);
        }


        public string word;

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
                    case "house":
                        var s = await api.GetImageByAlias("house");
                        if (s != null)
                        {
                            img = Helpers.Other.StreamToImageSource(s);
                            OnPropertyChanged("House");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                        }
                        break;
                    case "windows":
                        var d = await api.GetImageByAlias("win");
                        if (d != null)
                        {
                            img = Helpers.Other.StreamToImageSource(d);
                            OnPropertyChanged("Win");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                        }
                        break;
                    case "door":
                        var t = await api.GetImageByAlias("door");
                        if (t != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t);
                            OnPropertyChanged("Door");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                        }
                        break;
                    case "stairs":
                        var tt = await api.GetImageByAlias("stairs");
                        if (tt != null)
                        {
                            img = Helpers.Other.StreamToImageSource(tt);
                            OnPropertyChanged("Stairs");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                        }
                        break;
                    case "pen":
                        var ttt = await api.GetImageByAlias("pen");
                        if (ttt != null)
                        {
                            img = Helpers.Other.StreamToImageSource(ttt);
                            OnPropertyChanged("Pen");
                            word = "";
                            OnPropertyChanged(nameof(Word));
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




    }
}
