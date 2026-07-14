namespace NineLives.Framework.Core.UI
{
    public interface IScreenShower
    {
        void HideAllScreens();
        void ShowScreen(IScreen screen);

        IScreen? Current { get; }

        bool TryGoBackToPreviousScreen(out IScreen? screen);        
    }
}