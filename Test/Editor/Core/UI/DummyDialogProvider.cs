
namespace NineLives.Framework.Core.UI.Tests
{
    internal class DummyDialogProvider : IDialogProvider
    {
        public IDialogScreen GetDialog(DialogArguments dialogArguments)
        {
            throw new System.NotImplementedException();
        }

        public void Release(IDialogScreen dialogScreen)
        {
            throw new System.NotImplementedException();
        }
    }
}