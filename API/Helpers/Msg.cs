using System;
using System.Threading.Tasks;

namespace Helpers
{
    public static class Msg
    {
        public static void Write(string text)
        {
            if (text == null) text = "NULL";
            //DependencyService.Get<IMessageBox>().Show(DateTime.Now.ToString("T") + ":\t" + text, null);
            _ = System.Windows.MessageBox.Show(text);
        }

        public static void Write(Exception ex)
        {
            Write(ex.Message + " StackTrace: " + ex.StackTrace);
        }
    }
}
