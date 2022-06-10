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
        public VegeViewModel()
        {
            api = API.Instance;
            //EntCommand = new Command(OnSave);
            LoadClick= new Command(ppp);
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
                switch(word)
                {
                    case "carrot":
                    var s = await api.GetImageByAlias("carrot");
                        if (s != null)
                        {
                            img = Helpers.Other.StreamToImageSource(s);
                            OnPropertyChanged("Carrot");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                        }
                        break;
                    case "яблах":
                        var d = await api.GetImageByAlias("potato");
                        if (d != null)
                        {
                            img = Helpers.Other.StreamToImageSource(d);
                            OnPropertyChanged("Potato");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                        }
                        break;
                    case "tomato":
                        var t = await api.GetImageByAlias("tomato");
                        if (t != null)
                        {
                            img = Helpers.Other.StreamToImageSource(t);
                            OnPropertyChanged("Tomato");
                            word = "";
                            OnPropertyChanged(nameof(Word));
                        }
                        break;
                    case "21":

                        break;
                    default:
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
        


    }
}
