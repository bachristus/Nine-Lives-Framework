using NineLives.Framework.Core.Application;
using System;

namespace NineLives.Framework.Core.UI.Tests
{
    internal class DummyScreen : IScreen
    {
        public DummyScreen(string name, bool isVisibleExclusively = true, bool isVisible = true, bool isInteractable = true, bool isModal = true, AppState appState = AppState.None)
        {
            Id = name;
            Title = name.ToUpperInvariant();
            AppState = appState;
            IsModal = isModal;
            IsVisibleExclusively = isVisibleExclusively;
            IsVisible = isVisible;
            IsInteractable = isInteractable;
        }

        public string Id { get; private set; }

        public AppState AppState { get; set; }

        public bool IsModal { get; set; }

        public bool IsVisibleExclusively { get; set; }

        public bool IsInteractable { get; set; }
        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public IAppManager? AppManager { get; set; }
        public IUIRequest? UIRequest { get; set; }

        public event Action? ProcessCancelPressed;

        public void ProcessCancel()
        {
            ProcessCancelPressed?.Invoke();
        }
    }
}