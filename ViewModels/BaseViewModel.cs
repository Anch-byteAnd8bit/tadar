using System.ComponentModel;

namespace Tadar.ViewModels
{
    /// <summary>
    /// Базовый класс для всех классов ViewModel.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Для работы с сервером.
        /// </summary>
        protected static nsAPI.API api;

        /// <summary>
        /// Событие, которое вызывается, когда происходит изменение одного из свойств класса.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Этот метод надо вызывать, когда изменяется свойство класса.
        /// </summary>
        /// <param name="propertyName">Имя свойства, которое изменилось</param>
        protected void OnPropertyChanged(string propertyName)
        {
            // Получаем ссылку на событие, уведомляющее об изменении свойства.
            PropertyChangedEventHandler handler = PropertyChanged;

            // Если к нему кто-то приязан, то 
            if (handler != null)
            {
                // Уведомляем всех, кто к нему привязан, что свойство propertyName изменилось.
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
