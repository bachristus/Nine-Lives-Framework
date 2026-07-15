using NineLives.Framework.Core.Application.Manager;

namespace NineLives.Framework.Unity.UI
{
    public class ExitToMenuButton : ApplicationContextButton
    {
        protected override void OnClick()
        {
            if (context.AppManager != null)
            {
                context.AppManager?.GoToMenu();
            }
        }
    }
}
