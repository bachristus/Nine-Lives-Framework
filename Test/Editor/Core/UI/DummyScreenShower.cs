using NineLives.Framework.Core.UI;
using System;

namespace NineLives.Framework.Core.Tests
{
    internal class DummyScreenShower : IScreenShower
    {
        public event Action<IScreen>? ShowScreenCalled;
        public event Action? HideAllScreensCalled;
        public event Action? TryGoBackToPreviousScreenCalled;

        public DummyScreenShower()
        {
        }

        public IScreen? Current { get; set; }

        public void HideAllScreens()
        {
            HideAllScreensCalled?.Invoke();
        }

        public void ShowScreen(IScreen screen)
        {
            ShowScreenCalled?.Invoke(screen);
            Current = screen;
        }

        public bool TryGoBackToPreviousScreen(out IScreen? screen)
        {
            screen = null;
            TryGoBackToPreviousScreenCalled?.Invoke();
            return false;
        }
    }
}