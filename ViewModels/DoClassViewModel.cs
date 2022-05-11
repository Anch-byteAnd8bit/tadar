using Helpers;
using nsAPI;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tadar.Helpers;
using Tadar.Models;
using Tadar.Views;


namespace Tadar.ViewModels
{
    public class DoClassViewModel : BaseViewModel
    {
        private JournalForReg journ;
        private ClassroomForReg classroom;
        public Command RegCommand { get; set; }
        public DoClassViewModel()
        {
            journ = new JournalForReg();
            classroom = new ClassroomForReg();
            RegCommand = new Command(SaveAsync, ValidateSave);

        }

        

        public string Name
        {
            // Когда надо вернуть фамилию.
            get => journ.Name;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                journ.Name = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            // Когда надо вернуть фамилию.
            get => journ.Description;
            // Когда надо задать фамилию.
            set
            {
                // Присваиваем новое значение фамилии.
                journ.Description = value;
                // Уведомляем форму, что свойство "Surname" изменилось.
                OnPropertyChanged(nameof(Description));
            }
        }





        private bool ValidateSave()
        {
            bool isGood = true;
            if (string.IsNullOrWhiteSpace(journ.Name))
            {
                //birth.ToolTip = "Выберите дату рождения!";
                //birth.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else
            {
                //birth.ToolTip = null;
                //birth.BorderBrush = Brushes.Transparent;
            }


            if (string.IsNullOrWhiteSpace(journ.Description))
            {
                //f_Box.ToolTip = "Введите фамилию!";
                //f_Box.BorderBrush = Brushes.Red;
                isGood = false;
            }
            else
            {
                //f_Box.ToolTip = null;
                //f_Box.BorderBrush = Brushes.Transparent;
            }

            return isGood;
        }

        private async void SaveAsync(object o)
        {
            // Во время любой операции с сервером может вылезти ошибка!
            try
            {
                classroom.Journal = journ;
                classroom.Name = journ.Name;
                classroom.Description = journ.Description;
                classroom.id_User = api.MainUser.ID;
                RegisteredClassroom classroo = await api.AddClassroomAsync(classroom);


                MessageBox.Show(classroo.Name + ": " + classroo.ID);

                First.Base_frame.Navigate(new AddToClassPage());
            }
            // TODO: надо потом определять тип ошибки и выводить соотвествующие сообщения...
            catch (ErrorResponseException ex)
            {
                switch (ex.ErrCode)
                {
                    case CODE_ERROR.ERR_UserAlreadyReg:
                        MessageBox.Show("is registered");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }


    }
}
