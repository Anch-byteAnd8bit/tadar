using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    class AddWordViewModel : BaseViewModel
    {
        Word word = new Word();
        public AddWordViewModel()
        {
            AddClick = new Command(Add_Click);

            //LoadDictAsync();

            Parts = new List<Refbook>()
            {
                new Refbook{ ID = "1", Name = "Существительное"},
                new Refbook{ ID = "2", Name = "Прилагательное"},
                new Refbook{ ID = "3", Name = "Глагол"},
            };
            // Получаем список типов слов от сервера.
            GettingTypesWords();
        }

        private List<Refbook> parts;

        public List<Refbook> Parts
        {
            get => parts;
            // Задать новый список.
            set
            {
                // Получаем ноый список.
                parts = value;
                // Уведомляем форму о новом списке.
                OnPropertyChanged(nameof(Parts));
                // Задаем новый выбранный жлемент из списка.
                SelectedPart = Parts[0];
            }
        }

        public Refbook SelectedPart
        {
            get
            {
                // Ищем в списке типов слов, объект, у которого свойства ID совпдает с
                // со свйоством id_TypeWord.
                // Если такой элемент в списке не найден, то возвращаем первый элемент
                // из списка.
                return
                    Parts.SingleOrDefault(el => el.ID == word.id_TypeWord) ?? Parts[0];
            }
            set
            {
                // Если есть элемент в списке, который равен задавемому элементу...
                if (Parts.Exists(el => el == value))
                {
                    // ...присваиваем его ID .
                    word.id_TypeWord = value.ID;
                }
                // Иначе, 
                // присваиваем ID первого элемент списка.
                else word.id_TypeWord = Parts[0].ID;
                // Уведомляем интфрейс о том, что это свйоство было изменено.
                OnPropertyChanged(nameof(SelectedPart));
            }
        }

        private async void GettingTypesWords()
        {
            Parts = await api.GetTypeWordsAsync();
        }

        public async void Add_Click(object ob)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }

                // добавлять ID текущего пользователя в слово word
                word.id_User = api.MainUser.ID;
                bool wordadd = await api.AddWordAsync(word);

                // OnPropertyChanged(nameof(Word));
                First.Base_frame.Navigate(new DictPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Command AddClick
        {
            get;
            set;
        }
        public string Word
        {
            // Получить.
            get => word.HakWord;
            // Задать.
            set
            {
                word.HakWord = value;
                // Уведомление.
                OnPropertyChanged(nameof(Word));
            }
        }

        public string Rus
        {
            // Получить.
            get => word.RusWord;
            // Задать.
            set
            {
                word.RusWord = value;
                // Уведомление.
                OnPropertyChanged(nameof(Rus));
            }
        }


        public string Name
        {
            // Получить.
            get => api.MainUser.Name;
            // Задать.
            set
            {
                api.MainUser.Name = value;
                // Уведомление.
                OnPropertyChanged(nameof(Name));
            }
        }


    }
}
