using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadar
{
    class SingleInputVM : NotifyPropertyChangedImpl
    {
        public SingleInputVM(string prompt) { this.prompt = prompt; }

        // строка, описывающая вводимый текст
        readonly string prompt; // не изменяется
        public string Prompt { get { return prompt; } }

        // строка, принимающая ввод (и стандартное INPC-свойство)
        string input;
        public string Input
        {
            get { return input; }
            set { if (input != value) { input = value; NotifyPropertyChange(); } }
        }
    }
}
