using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
   public class MyDictViewModel: BaseViewModel
    {
        private Word std;
        private Word st;
        private List<Word> words = new List<Word>();
        public MyDictViewModel()
        {
            //api.MainUser.Name
            MuClick = new Command(Mu_Click);
            AddClick = new Command(Add_Click);
            //DelClick = new Command(Del_Click);
            Sound = new Command(LoadAudAsync);
            LoadDictAsync();
        }
        SoundPlayer sp = new SoundPlayer();

        public async void LoadAudAsync(object ob)
        {
            std = new Word();
            std = (Word)ob;

            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                //загружает и общий и пользоватлеьский словари.
                var aud = await api.GetAudioOfWordAsync(std.ID);

                // Если слов нет, то выходим.
                if (aud == null) return;

                sp.SoundLocation = aud.PathAudio;
                sp.LoadAsync();
                sp.Play();
                // sp.PlayLooping();


                OnPropertyChanged(nameof(WordsList));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                words = await api.GetUserWordsAsync(api.MainUser.ID);

                // Если слов нет, то выходим.
                if (words == null) return;

               
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



        //private void Del_Click(object ob)
        //{
        //    First.Base_frame.Navigate(new marks());
        //    //открытие новой страницы с вводом логина и пароля 
        //}
        private void Add_Click(object ob)
        {
            First.Base_frame.Navigate(new AddWordPage());
            //открытие новой страницы с вводом логина и пароля 
        }
        private void Mu_Click(object ob)
        {
            st = new Word();
            st = (Word)ob;
            // string iclass = cls.ID;
            string istud = st.ID;
            if (MessageBox.Show("Удалить это слово?", "Уточнение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                var del = api.DelWordAsync(istud);
                LoadDictAsync();
               // OnPropertyChanged(nameof(WordsList));
                //do yes stuff
            }
            //открытие новой страницы с вводом логина и пароля 
        }
        public Command MuClick
        {
            get;
            set;
        }
        public Command AddClick
        {
            get;
            set;
        }
        public Command Sound
        {
            get;
            set;
        }

    }
}
