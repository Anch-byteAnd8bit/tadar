using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace tadar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // создаём VM-ки страниц визарда
            var inputs = new[]
            {
            new SingleInputVM("Here, input your name"),
            new SingleInputVM("... and now address, please"),
            new SingleInputVM("... and your age, too")
        };

            // создаём VM самого визарда
            var wizard = new WizardVM(inputs);
            // и его окно
            var window = new First_Window() { DataContext = wizard };
            // показываем окно...
            window.Show();
            // и ждём, когда работа визарда закончится
            await wizard.Run();
            // закрываем окно
            window.Close();
            // и показываем результат
            MessageBox.Show(
                string.Format(
                    "you are {0}, age {2}, live at {1}",
                    inputs[0].Input,
                    inputs[1].Input,
                    inputs[2].Input));
        }
    }
}
