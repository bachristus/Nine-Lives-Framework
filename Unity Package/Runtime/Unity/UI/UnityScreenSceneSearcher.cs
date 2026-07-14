using NineLives.Framework.Core.Progress;
using NineLives.Framework.Core.UI;
using System.Threading;
using System.Threading.Tasks;

namespace NineLives.Framework.Unity.UI
{
    public class ScreenSceneSearcher : IScreensProvider
    {
        public Task<IScreen[]> GetScreens(IOperationProgress progress, CancellationToken token)
        {
            return Task.FromResult(GameObjectHelper.FindAllMonoBehavioursOfType<IScreen>(UnityEngine.FindObjectsInactive.Include));
        }
    }
}