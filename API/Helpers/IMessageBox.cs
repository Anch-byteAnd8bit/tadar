using System.Threading.Tasks;

namespace Helpers
{
    public interface IMessageBox
    {
        void Show(string messageBoxText, string caption);
    }
}
