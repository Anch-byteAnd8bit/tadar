using Helpers;
using nsAPI;
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
  public class DoTheoryViewModel : BaseViewModel
    {
        private List<RegisteredClassroom> classrooms;
        private RegisteredClassroom selectedclassroom;
        private Theory theory = new Theory();

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

        public DoTheoryViewModel()
        {


            //userent = new UserForAuthorization();
            // EntCommand = new Command(OnSave);
           
            //

            Classrooms = new List<RegisteredClassroom>()
            {
                new RegisteredClassroom{ ID = "1", Name = "Класс 1",
                    Description="class",
                    DateTimeClose=null,
                    DateTimeCreate=null
                },
                new RegisteredClassroom{ ID = "1", Name = "Класс 2",
                    Description="classddddddddd",
                    DateTimeClose=null,
                    DateTimeCreate=null},
            }; 
            GettingClassrooms();
            CreateClick = new Command(Create_Click);


        }


        public string Name
        {
            // Когда надо вернуть фамилию.
            get => theory.Topic;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                theory.Topic = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Sour
        {
            // Когда надо вернуть фамилию.
            get => theory.Source;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                theory.Source = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Sour));
            }
        }
        public string Cont
        {
            // Когда надо вернуть фамилию.
            get => theory.Content;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                theory.Content = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Cont));
            }
        }




        private async void Create_Click(object ob)
        {
            // Во время любой операции с сервером может вылезти ошибка!
            try

            {
                theory.id_Class = selectedclassroom.ID;
                theory = await api.AddTheoryAsync(theory);
                if (theory==null)
                {
                    Msg.Write(api.LastException.Message);
                }
                //  MessageBox.Show(work.WorkHeader.ID + ": " + work.WorkHeader.Name + work.WorkBody[0].TaskTitle);



                First.Base_frame.Navigate(new MenuPage());
            }
            // TODO: надо потом определять тип ошибки и выводить соотвествующие сообщения...
            //catch (ErrorResponseException ex)
            //{
            //    switch (ex.ErrCode)
            //    {
            //        case CODE_ERROR.ERR_UserAlreadyReg:
            //            MessageBox.Show("is registered");
            //            break;
            //        default:
            //            break;
            //    }
            //}
            catch (Exception ex)
            {
                Msg.Write(ex);
            }

        }
        public Command CreateClick
        {
            get;
            set;
        }


        private async void GettingClassrooms()
        {
            try
            {
                string id_TeacherRole = api.Refbooks.GetRole(TRoles.Teacher)?.ID ?? "3";
                Classrooms = await api.GetClassroomsByUserIdAsync(api.MainUser.ID, id_TeacherRole);
                if (Classrooms == null)
                {
                    if (api.LastException != null && api.LastException.TypeError == TError.DefinedError)
                    {
                        if (api.LastException.CodeAPI == CODE_ERROR.ERR_ClassNotFound)
                        {
                            // Классы не найдены.
                        }
                    }
                }
            }
            catch (Exception)
            {
                Classrooms = null;
            }
        }





    }
}
