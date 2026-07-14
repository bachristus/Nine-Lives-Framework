using System.Threading.Tasks;

namespace NineLives.Framework.Core.UI
{
    public interface IUIRequest
    {
        void ShowScreen(string screenID);
        void GoBackToPreviousScreen();
        Task<DialogButtonInfo> ShowDialog(DialogArguments dialogArguments);
    }
}