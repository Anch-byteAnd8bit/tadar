using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    class TheoryThemeViewModel : BaseViewModel
    {
        private Theory st;
        private List <Theory> themes;
        private List<RegisteredUser> classroomsusers = new List<RegisteredUser>();
        private Theory theory;
        private RegisteredUser std;
        private string idclass;
        private Visibility visi;
        public TheoryThemeViewModel(string clsid)
        {
            idclass = clsid;
            TestClick = new Command(Test_Click);
            // TextClick = new Command(Text_Click);
            LoadTestsAsync(idclass);
            LoadClassAsync(clsid);
            BackClick = new Command(Back_Click);
            MuClick = new Command(Mu_Click);
            visi = Visibility.Hidden;
            OnPropertyChanged("Pota");
            //if (api.MainUser.ID== classroomsusers[0].ID)
            //{
            //    visi = Visibility.Visible;
            //}
        }
        public async void LoadClassAsync(string iclass)
        {
            try
            {
                if (api == null)
                {
                    throw new Exception("api не создан!!!");
                }
                classroomsusers =
                  await api.GetUsersByClassIdAsync(iclass,"3");
                if (api.MainUser.ID == classroomsusers[0].ID)
                {
                    visi = Visibility.Visible;
                    OnPropertyChanged("Pota");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Mu_Click(object ob)
        {
            st = new Theory();
            st = (Theory)ob;
            // string iclass = cls.ID;
            string istud = st.ID;
            if (MessageBox.Show("Удалить эту тему?", "Уточнение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                var del = api.DelTheory(istud);
                LoadTestsAsync(idclass);
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

        public Visibility Pota
        {
            get { return visi; }
            private set
            {
                visi = value;
                OnPropertyChanged("Pota");
            }
        }
        private void Back_Click(object ob)
        {
            First.Base_frame.Navigate(new MenuPage());

        }

        public Command BackClick
        {
            get;
            set;
        }
        private void Test_Click(object ob)
        {
            theory = new Theory();
            theory = (Theory)ob;
            
            First.Base_frame.Navigate(new ViewTheoryPage(theory));

        }
        public Command TestClick
        {
            get;

        }


        public async void LoadTestsAsync(string idclass)
        {
            try
            {
                Themes = await api.GetTheoriesByClassroomIDAsync(idclass);
                

                
                //OnPropertyChanged(nameof(Themes));
                //OnPropertyChanged(nameof(TextsList));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Вы не состоите ни в одном классе или в классе нет теории!");
            }
        }

     //   public ObservableCollection<Theory> Themes { get; set; }
        public List<Theory> Themes
        {
            get { return themes; }
            set
            {
                themes = value;
                OnPropertyChanged(nameof(Themes));
                // Задаем новый выбранный жлемент из списка.
                // SelectedClassroom = Classrooms[0];
            }
        }



    }
}
