namespace NineLives.Framework.Unity.UI
{
    public class MenuScreen : AppScreen
    {
        protected override void OnCancelPressed()
        {
            UIRequest?.GoBackToPreviousScreen();            
        }
    }
}