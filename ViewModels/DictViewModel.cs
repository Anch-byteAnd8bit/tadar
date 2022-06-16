using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
  public class DictViewModel : BaseViewModel
    {

        private List<Word> words = new List<Word>();
        public DictViewModel()
        {
            //api.MainUser.Name
            AddClick = new Command(Add_Click);
            //DelClick = new Command(Del_Click);
           
            LoadDictAsync();
        }

       


        public async void LoadDictAsync()
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                //загружает и общий и пользоватлеьский словари.
                words = await api.GetCombiWordsAsync(api.MainUser.ID);
                
                // Если слов нет, то выходим.
                if (words == null) return;

                // Афигеть! Сортировка у Ани!
                words.Sort(delegate (Word teacher1, Word teacher2)
                { return teacher1.RusWord.CompareTo(teacher2.RusWord); });

                OnPropertyChanged(nameof(WordsList));
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
        }


        // public ObservableCollection<Word> WordsList { get; set; }

        public List<Word> WordsList
        {
            get { return words; }
            set
            {
                words = value;
                OnPropertyChanged(nameof(WordsList));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }


        private void Add_Click(object ob)
        {
            First.Base_frame.Navigate(new AddWordPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        //private void Del_Click(object ob)
        //{
        //    First.Base_frame.Navigate(new marks());
        //    //открытие новой страницы с вводом логина и пароля 
        //}


        public Command AddClick
        {
            get;
            set;
        }
        //public Command DelClick
        //{
        //    get;
        //    set;
        //}
        //public string Surname
        //{
        //    // Когда надо вернуть фамилию.
        //    get => api.MainUser.Surname;
        //    // Когда надо задать фамилию.
        //    set
        //    {
        //        // Присваиваем новое значение фамилии.
        //        api.MainUser.Surname = value;
        //        // Уведомляем форму, что свойство "Surname" изменилось.
        //        OnPropertyChanged(nameof(Surname));
        //    }
        //}
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
