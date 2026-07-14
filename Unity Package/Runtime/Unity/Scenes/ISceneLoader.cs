using Cysharp.Threading.Tasks;
using NineLives.Framework.Core.Progress;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace NineLives.Framework.Unity.Scenes
{
    public interface ISceneLoader
    {
        Task LoadSceneAsync(
            string sceneName,
            LocalPhysicsMode localPhysicsMode,
            LoadSceneMode loadSceneMode,
            IOperationProgress progress,
            CancellationToken cancellationToken = default);
        UniTask UnloadSceneAsync(string sceneName);
    }
}