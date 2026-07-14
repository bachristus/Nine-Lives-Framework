using NineLives.Framework.Core;
using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.Application.Manager;
using NineLives.Framework.Core.Persistance;
using NineLives.Framework.Core.Progress;
using NineLives.Framework.Core.UI;
using NineLives.Framework.Unity.Scenes;
using NineLives.Framework.Unity.UI;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace NineLives.Framework.Unity.Game
{
    public class GameStarter<TSimulationState> where TSimulationState : ISimulationState
    {
        private readonly IGameInput input;

        private readonly IInitializingScreen initializingScreen;
        private readonly string uiSceneName;
        private readonly ISimulationStateSaverLoader<TSimulationState> saverLoader;
        private readonly TSimulationState newGameSimulationState;
        private readonly IDialogProvider dialogProvider;
        private readonly ISimulationStateApplier<TSimulationState> savesDataApplier;
        private AppManager? gameManager;

        public event Action? GameStarted;

        public GameStarter(IGameInput input,
            IInitializingScreen initializingScreen,
            string uiSceneName,            
            ISimulationStateSaverLoader<TSimulationState> saverLoader,
            ISimulationStateApplier<TSimulationState> savesDataApplier,
            TSimulationState newGameSimulationState)
        {
            Debug.Log($"'{GetType()}' ctor");
            this.input=input;
            this.initializingScreen = initializingScreen;
            this.uiSceneName = uiSceneName;
            this.savesDataApplier = savesDataApplier;
            this.saverLoader = saverLoader;
            this.newGameSimulationState = newGameSimulationState;
            Debug.Log($"'{GetType()}' ctor ended");
        }

        public async void StartGame()
        {
            Debug.Log($"'{GetType()}' StartGame()");
            try
            {                
                var generalProgress = new WeightedProgressAggregator();
                initializingScreen.ProgressIndicator.SetProgressReporter(generalProgress);
                initializingScreen.IsVisible=true;
                var uiLoadingProgress = generalProgress.CreateSubProgress(0.2f);
                var gameLoadingProgress = generalProgress.CreateSubProgress(0.8f);
                gameLoadingProgress.Report(0f, "Game loading started...");
                var cancellationSource = new CancellationTokenSource();
                cancellationSource.CancelAfter(60000);
                Debug.Log($"'{GetType()}' A");
#if DEBUG
                await Task.Delay(100);

#endif
                var sceneLoader = new SceneLoader();
                var simulationLoader = new SimulationLoader<TSimulationState>(sceneLoader, saverLoader, savesDataApplier, newGameSimulationState);
                var simulation = new SimulationProvider(simulationLoader);
                gameManager = new AppManager(simulation, new SimulationTime());
                gameLoadingProgress.Report(0.1f, "Game manager created...");
                Debug.Log($"'{GetType()}' B");
#if DEBUG
                await Task.Delay(100);
#endif
                var screenSearcher = new UnityScreenSceneSearcher();
                var screenStack = new StackScreenShower();
                var screensProvider = new SeparateUISceneScreenProvider(uiSceneName, sceneLoader, screenSearcher);
                gameLoadingProgress.Report(0.3f, "UI loading started...");
                Debug.Log($"'{GetType()}' C");
                await screensProvider.Initialize(uiLoadingProgress, cancellationSource.Token);

#if DEBUG
                await Task.Delay(100);
#endif
                var pr = 0.6f;
                gameLoadingProgress.Report(pr, "UI loaded...");
                Debug.Log($"'{GetType()}' D");
                generalProgress = null;

                var screens = screensProvider.GetScreens();
                var dialogProvider=GameObjectHelper.FindFirstMonoBehavioursOfType<IDialogProvider>(FindObjectsInactive.Include);
                if (dialogProvider == null) throw new Exception("UI Scene does not contain {typeof(IdialogProvider)} components");
                var uiManager = new UIManager(gameManager, input, screenStack, screens, dialogProvider);
                
                foreach (var screen in screens)
                {
#if DEBUG
                    await Task.Delay(100);
#endif

                    gameLoadingProgress.Report(pr+=0.05f, $"Screen '{screen.Id.Id}' initialized...");
                    
                    screen.Initialize(gameManager, uiManager/*, GameInput*/);
                }
                Debug.Log($"'{GetType()}' E");
                gameManager.QuitGameRequested += QuitApp;
#if DEBUG
                await Task.Delay(100);
#endif
                gameLoadingProgress.Report(pr += 0.95f, $"Game is about to start...");
#if DEBUG
                await Task.Delay(100);
#endif

                gameManager.Start();

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
            if (gameManager != null)
            {
                gameManager.QuitGameRequested -= QuitApp;
                gameManager.Dispose();
                gameManager = null;
            }
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}