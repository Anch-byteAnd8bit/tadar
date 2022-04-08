using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadar
{
    class WizardVM : NotifyPropertyChangedImpl
    {
        // это таск, который окончится, когда выполнится OnFinish
        public Task Run()
        {
            return tcs.Task;
        }

        // получаем в конструкторе список объектов, которые мы будем
        // прогонять нашим визардом
        public WizardVM(IEnumerable<object> pages)
        {
            this.pages = pages.ToList();
            onNext = new TrivialCommand(ProcessNext);
            onFinish = new TrivialCommand(ProcessFinish);
            currentIndex = -1;
            ProcessNext();
        }

        // увеличивает currentIndex на 1 и устанавливает остальные данные
        void ProcessNext()
        {
            currentIndex++;
            // переходим к следующей странице в списке
            CurrentPage = pages[currentIndex];
            // если это последняя страница...
            if (currentIndex == pages.Count - 1)
            {
                // то дальше идти нельзя, но можно закончить
                onNext.CanExecuteProperty = false;
                onFinish.CanExecuteProperty = true;
            }
            else
            {
                // иначе дальше идти можно, закончить нельзя
                onNext.CanExecuteProperty = true;
                onFinish.CanExecuteProperty = false;
            }
        }

        void ProcessFinish()
        {
            tcs.TrySetResult(0);
            // один раз закончили — больше нельзя
            onFinish.CanExecuteProperty = false;
        }

        // список VM-объектов, представляющих страницы
        List<object> pages;
        // номер текущего объекта, в начале это 0
        int currentIndex;
        // таск, который будет завершён по окончанию работы
        TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

        // текущая страница, стандартное свойство с INPC
        object currentPage;
        public object CurrentPage
        {
            get { return currentPage; }
            set { if (currentPage != value) { currentPage = value; NotifyPropertyChange(); } }
        }

        // команда перехода к следующей странице
        TrivialCommand onNext;
        public TrivialCommand OnNext { get { return onNext; } }

        // команда окончания работы
        TrivialCommand onFinish;
        public TrivialCommand OnFinish { get { return onFinish; } }
    }
}
