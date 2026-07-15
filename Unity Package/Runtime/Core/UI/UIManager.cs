using NineLives.Framework.Core.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.UI
{
    public class UIManager : IUIRequest, IDisposable
    {
        private readonly IScreenShower shower;
        private readonly Dictionary<string, IScreen> screensById = new();
        private readonly Dictionary<AppState, IScreen> screensByGameState = new();
        private readonly IAppStateHolder gameStateHolder;
        private readonly IGameInput input;
        private readonly IDialogProvider dialogProvider;

        public UIManager(IAppStateHolder gameStateHolder, IGameInput gameInput, IScreenShower screenShower, IEnumerable<IScreen> screens, IDialogProvider dialogProvider)
        {
            this.shower = screenShower;
            this.gameStateHolder = gameStateHolder;
            this.input = gameInput;
            this.dialogProvider = dialogProvider;

            foreach (var screen in screens)
            {
                screen.IsVisible = false;
                var type = screen.GetType();
                if (screensById.TryGetValue(screen.Id, out _))
                {
                    throw new Exception($"Screen of type={type.FullName} has screen ID duplicated in screens list");
                }
                screensById.Add(screen.Id, screen);

                if (screen.AppState != AppState.None)
                {
                    if (screensByGameState.TryGetValue(screen.AppState, out _))
                    {
                        throw new Exception($"Screen of type={type.FullName} has game state ID duplicated in screens list");
                    }

                    screensByGameState.Add(screen.AppState, screen);
                }                
            }

            gameStateHolder.AppStateChanged += OnGameStateChanged;
            this.input.CancelPressed += OnCancel;
        }

        private void OnCancel()
        {
            shower.Current?.ProcessCancel();
        }

        public void GoBackToPreviousScreen()
        {
            shower.TryGoBackToPreviousScreen(out _);
        }

        private void OnGameStateChanged(AppState appState)
        {
            switch (appState)
            {
                case AppState.Menu:
                    shower.HideAllScreens();
                    break;

                case AppState.Loading:
                    break;

                case AppState.Playing:
                    shower.HideAllScreens();
                    break;

                case AppState.Pause:
                    break;

                default:
                    throw new ArgumentException($"Unknown application state={appState}");
            }
            ShowScreen(appState);
        }

        private void ShowScreen(AppState stateId)
        {
            if (screensByGameState.TryGetValue(stateId, out var screen))
            {
                shower.ShowScreen(screen);
            }
        }

        public void ShowScreen(string screenId)
        {
            if (screensById.TryGetValue(screenId, out var screen))
            {
                shower.ShowScreen(screen);
            }
        }

        public void Dispose()
        {
            gameStateHolder.AppStateChanged -= OnGameStateChanged;
        }

        public async Task<DialogButtonInfo> ShowDialog(DialogArguments dialogArguments)
        {
            IDialogScreen dialogScreen = this.dialogProvider.GetDialog(dialogArguments);
            var tcs = new TaskCompletionSource<DialogButtonInfo>();

            void OnClosed(DialogButtonInfo info)
            {
                dialogScreen.Closed -= OnClosed;
                if (shower.TryGoBackToPreviousScreen(out _))
                {
                    dialogProvider.Release(dialogScreen);
                }
                tcs.TrySetResult(info);
            }

            dialogScreen.Closed += OnClosed;

            shower.ShowScreen(dialogScreen);

            return await tcs.Task;
        }
    }
}