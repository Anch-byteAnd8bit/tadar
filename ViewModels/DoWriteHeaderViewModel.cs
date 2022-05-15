using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;

namespace Tadar.ViewModels
{
    public class DoWriteHeaderViewModel : BaseViewModel


    {
        private List<RegisteredClassroom> classrooms;
        private RegisteredClassroom selectedclassroom;
        public string Nametest;
        public string Desctest;
        public bool isChecked = false;
        public List<RegisteredClassroom> Classrooms
        {
            get { return classrooms; }
            set
            {
                classrooms = value;
                OnPropertyChanged(nameof(Classrooms));
                // Задаем новый выбранный жлемент из списка.
                SelectedClassroom = Classrooms[0];
            }
        }

        public RegisteredClassroom SelectedClassroom
        {
            get
            {
                return
                     classrooms.SingleOrDefault(el => el == selectedclassroom) ?? classrooms[0];

            }
            set
            {
                // Если есть элемент в списке полов, который равен задавемому элементу...
                if (Classrooms != null && Classrooms.Exists(el => el == value))
                {
                    selectedclassroom = value;
                }
                // Иначе, полу регистрируемого пользователя
                // присваиваем ID первого элемент списка.
                else selectedclassroom = Classrooms?[0] ?? null;
                // Уведомляем интфрейс о том, что это свйоство было изменено.
                OnPropertyChanged(nameof(Classrooms));
            }
        }


        public DoWriteHeaderViewModel()
        {


            //userent = new UserForAuthorization();
            // EntCommand = new Command(OnSave);
           
            //

            Classrooms = new List<RegisteredClassroom>()
            {
                new RegisteredClassroom{ ID = "1", Name = "Женский",
                    Description="class",
                    DateTimeClose=null,
                    DateTimeCreate=null
                },
                new RegisteredClassroom{ ID = "1", Name = "Ve;crjq",
                    Description="classddddddddd",
                    DateTimeClose=null,
                    DateTimeCreate=null},
            };
            GettingClassrooms();
            CreateClick = new Command(Create_Click);


        }


        public string Testname
        {
            // Когда надо вернуть фамилию.
            get => Nametest;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                Nametest = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Testname));
            }
        }

        public string TestDesc
        {
            // Когда надо вернуть фамилию.
            get => Desctest;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                Desctest = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(TestDesc));
            }
        }


        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(isChecked));
            }
        }



        private async void GettingClassrooms()
        {
            try
            {
                Classrooms = await api.GetAllClassess();
            }
            catch (Exception)
            {
                Classrooms = null;
            }
        }





        private void Create_Click(object ob)
        {
            First.Base_frame.Navigate(new DoWritePage(isChecked, Nametest, Desctest, selectedclassroom));
            //открытие новой страницы с вводом логина и пароля 
        }
        public Command CreateClick
        {
            get;
            set;
        }





    }
}
