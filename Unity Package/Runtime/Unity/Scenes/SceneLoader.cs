using NineLives.Framework.Core.Progress;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace NineLives.Framework.Unity.Scenes
{
    public class SceneLoader : ISceneLoader
    {        
        public async Task LoadSceneAsync(
            string sceneName,
            LocalPhysicsMode localPhysicsMode,
            LoadSceneMode loadSceneMode,
            IOperationProgress progress,
            CancellationToken cancellationToken = default)
        {
            progress?.Report(new(0f, $"Starting loading scene '{sceneName}'"));

            var operation = SceneManager.LoadSceneAsync(sceneName,
                new LoadSceneParameters
                {
                    loadSceneMode = loadSceneMode,
                    localPhysicsMode = localPhysicsMode
                });

            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    progress?.Report(operation.progress, "Loading scene '{sceneName}' cancelled");
                    return;
                }

                progress?.Report(operation.progress, $"Loading scene '{sceneName}'");

                await Task.Yield();
            }

            progress?.Report(operation.progress, $"Preparing scene '{sceneName}' activation...");

            if (cancellationToken.IsCancellationRequested)
            {
                progress?.Report(0.9f, $"Cancelled before scene '{sceneName}' activation");
                return;
            }
                
            operation.allowSceneActivation = true;
                
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            progress?.Report(1f, $"Scene '{sceneName}' loaded successfully");
        }

        public UniTask UnloadSceneAsync(string sceneName)
        {
            return SceneManager.UnloadSceneAsync(sceneName).ToUniTask();
        }
    }
}