using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.Persistance;
using NineLives.Framework.Core.Progress;
using NineLives.Framework.Unity.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace NineLives.Framework.Unity.Game
{
    public class SimulationLoader<TSimulationState>: ISimulationLoader where TSimulationState : ISimulationState
    {        
        private readonly ISceneLoader sceneLoader;
        private readonly ISimulationStateSaverLoader<TSimulationState> saves;
        private readonly ISimulationStateApplier<TSimulationState> applier;
        private readonly TSimulationState newGameSimulationState;
        private readonly HashSet<string> loadedSimulationScenes=new();

        public SimulationLoader(
            ISceneLoader sceneLoader,
            ISimulationStateSaverLoader<TSimulationState> saverLoader,
            ISimulationStateApplier<TSimulationState> applier,
            TSimulationState newGameSimulationState)
        {            
            this.sceneLoader = sceneLoader;
            this.saves=saverLoader;
            this.applier = applier;
            this.newGameSimulationState = newGameSimulationState;
        }

        public async Task<ILoadedSimulation> LoadGameAsync(
            string? savedGame,
            IOperationProgress progress,
            CancellationToken cancellationToken)
        {
            var reportGame = savedGame == null ? "new game" : $"saved game '{savedGame}'";
            progress.Report(0.05f, $"Starting loading {reportGame}...");

#if DEBUG
            await Task.Delay(100);
#endif
            TSimulationState simulationState = savedGame == null
                ? newGameSimulationState
                : await saves.Load(savedGame);

            if (cancellationToken.IsCancellationRequested)
            {
                progress.Report(0.3f, $"Loading {reportGame} cancelled");

#if DEBUG
                await Task.Delay(100);
#endif
            }
            cancellationToken.ThrowIfCancellationRequested();

            if (simulationState == null)
            {
                throw new Exception($"Saved game '{savedGame}' not found");
            }

            progress.Report(0.3f, $"Saved data loaded from '{savedGame}'");
#if DEBUG
            await Task.Delay(100);
#endif

            await LoadScenesAsync(simulationState.RequiredScenes, progress, cancellationToken);
            
            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(0.8f, $"Game scene loaded for '{savedGame}'");
#if DEBUG
            await Task.Delay(100);
#endif
            await applier.Apply(simulationState);
            progress.Report(0.99f, $"Game '{savedGame}' loaded...");
            return new LoadedSimulation(savedGame,loadedSimulationScenes);
        }

        private async Task LoadScenesAsync(IEnumerable<string> requiredScenes, IOperationProgress progress, CancellationToken cancellationToken)
        {
            foreach (var sceneName in requiredScenes)
            {
#if DEBUG
                await Task.Delay(100);
#endif
                await LoadSceneAsync(sceneName, progress, cancellationToken);
            }
        }

        private async Task LoadSceneAsync(string sceneName, IOperationProgress progress, CancellationToken cancellationToken)
        {
            if (loadedSimulationScenes.Contains(sceneName))
            {       
                throw new InvalidOperationException($"Scene '{sceneName}' is already loaded");
            }

            await sceneLoader.LoadSceneAsync(sceneName,
                UnityEngine.SceneManagement.LocalPhysicsMode.Physics3D,
                UnityEngine.SceneManagement.LoadSceneMode.Additive,
                progress,
                cancellationToken);

            loadedSimulationScenes.Add(sceneName);
        }

        public async Task<int> UnloadGameAsync()
        {          
            int i = 0;
            foreach (string sceneName in loadedSimulationScenes.ToArray())
            {
                await sceneLoader.UnloadSceneAsync(sceneName);
                loadedSimulationScenes.Remove(sceneName);
                i++;
            }
            return i;
        }
    }
}