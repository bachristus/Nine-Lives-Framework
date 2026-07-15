namespace NineLives.Framework.Unity.UI
{
    public class PlayNewGameButton : ApplicationContextButton
    {
        protected override void OnClick()
        {
            if (context.AppManager != null)
            {
                context.AppManager.StartGame();
            }
        }
    }
}
