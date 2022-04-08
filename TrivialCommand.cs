using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace tadar
{
    class TrivialCommand : NotifyPropertyChangedImpl, ICommand
    {
        public TrivialCommand(Action onExecute) { this.onExecute = onExecute; }
        public bool CanExecute(object parameter) { return canExecute; }
        public void Execute(object parameter) { onExecute(); }
        public event EventHandler CanExecuteChanged;
        public bool CanExecuteProperty
        {
            get { return canExecute; }
            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    NotifyPropertyChange();
                    if (CanExecuteChanged != null)
                        CanExecuteChanged(this, new EventArgs());
                }
            }
        }
        bool canExecute;
        Action onExecute;
    }
}
