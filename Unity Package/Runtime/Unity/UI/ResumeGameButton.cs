namespace NineLives.Framework.Unity.UI
{
    public class ResumeGameButton : ApplicationContextButton
    {
        protected override void OnClick()
        {
            if (context.AppManager != null)
            {
                context.AppManager?.ResumeGame();
            }
        }
    }
}
