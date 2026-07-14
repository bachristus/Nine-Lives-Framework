using System;

namespace NineLives.Framework.Core.Application.Tests
{
    internal class DummyAppStateHolder : IAppStateHolder
    {
        public DummyAppStateHolder()
        {
        }

        public event Action<AppState>? AppStateChanged;
        public event Action? PauseGameCalled;

        public void CancelLoadingGame()
        {
            throw new NotImplementedException();
        }

        public void GoToMenu()
        {
            throw new NotImplementedException();
        }

        public void InvokeAppStateChanged(AppState state)
        {
            AppStateChanged?.Invoke(state);
        }

        public void PauseGame()
        {
            PauseGameCalled?.Invoke();
        }

        public void QuitGame()
        {
            throw new NotImplementedException();
        }

        public void ResumeGame()
        {
            throw new NotImplementedException();
        }

        public void StartGame(string? savedGameName = null)
        {
            throw new NotImplementedException();
        }
    }
}