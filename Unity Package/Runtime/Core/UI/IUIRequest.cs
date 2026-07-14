using System.Threading.Tasks;

namespace NineLives.Framework.Core.UI
{
    public interface IUIRequest
    {
        void RequestScreenToBeShown(ScreenId screenID);
        void RequestToGoBack();
        Task<DialogButtonInfo> ShowDialog(DialogArguments dialogArguments);
    }
}