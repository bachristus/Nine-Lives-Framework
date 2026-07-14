namespace NineLives.Framework.Core.UI
{
    public interface IDialogProvider
    {
        IDialogScreen GetDialog(DialogArguments dialogArguments);
        void Release(IDialogScreen dialogScreen);
    }
}