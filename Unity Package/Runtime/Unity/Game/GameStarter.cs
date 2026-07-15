using NineLives.Framework.Core;
using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.Application.Manager;
using NineLives.Framework.Core.Progress;
using NineLives.Framework.Core.UI;
using NineLives.Framework.Unity.UI;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace NineLives.Framework.Unity.Game
{
    public class GameStarter
    {
        private readonly IGameInput input;

        private readonly IInitializingScreen initializingScreen;
        private readonly IScreensProvider screensProvider;
        private readonly ISimulationProvider simulationProvider;
        private AppManager? appManager;

        public event Action? GameStarted;

        public GameStarter(IGameInput input,
            IInitializingScreen initializingScreen,
            IScreensProvider screensProvider,
            ISimulationProvider simulationProvider
            )
        {
            Debug.Log($"'{GetType()}' ctor");
            this.input = input;
            this.initializingScreen = initializingScreen;
            this.screensProvider = screensProvider;
            this.simulationProvider = simulationProvider;
            Debug.Log($"'{GetType()}' ctor ended");
        }

        public async Task StartGame(int cancellationTimeout)
        {
            Debug.Log($"'{GetType()}' StartGame()");
            try
            {
                var generalProgress = new WeightedProgressAggregator();
                initializingScreen.ProgressIndicator.SetProgressReporter(generalProgress);
                initializingScreen.IsVisible = true;
                var uiLoadingProgress = generalProgress.CreateSubProgress(0.2f);
                var gameLoadingProgress = generalProgress.CreateSubProgress(0.8f);
                gameLoadingProgress.Report(0f, "Game loading started...");

                var cancellationSource = new CancellationTokenSource();
                cancellationSource.CancelAfter(cancellationTimeout);

                appManager = new AppManager(simulationProvider, new SimulationTime());
                gameLoadingProgress.Report(0.1f, "Game manager created...");

                var screens = await screensProvider.GetScreens(uiLoadingProgress, cancellationSource.Token);

                var pr = 0.6f;
                gameLoadingProgress.Report(pr, "UI loaded...");

                var dialogProvider = GameObjectHelper.FindFirstMonoBehavioursOfType<IDialogProvider>(FindObjectsInactive.Include);
                if (dialogProvider == null) throw new Exception("UI Scene does not contain {typeof(IdialogProvider)} components");
                var screenStack = new StackScreenShower();
                var uiManager = new UIManager(
                    appManager,
                    input,
                    screenStack,
                    screens,
                    dialogProvider);

                var applicationContext = new ApplicationContext(appManager, uiManager);

                foreach (var screen in screens)
                {
                    gameLoadingProgress.Report(pr += 0.05f, $"Screen '{screen.Id}' initialized...");
                    screen.Initialize(applicationContext);
                }

                appManager.QuitGameRequested += QuitApp;

                gameLoadingProgress.Report(pr += 0.95f, $"Game is about to start...");

                appManager.Start();

                GameStarted?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Application.Quit();
            }
            finally
            {
                Debug.Log($"'{GetType()}' StartGame() finally");
            }
            Debug.Log($"'{GetType()}' StartGame() ended");
        }

        private void QuitApp()
        {
            if (appManager != null)
            {
                appManager.QuitGameRequested -= QuitApp;
                appManager.Dispose();
                appManager = null;
            }
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}


