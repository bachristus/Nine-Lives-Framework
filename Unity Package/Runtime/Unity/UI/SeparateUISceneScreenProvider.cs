using NineLives.Framework.Core.Progress;
using NineLives.Framework.Core.UI;
using NineLives.Framework.Unity.Scenes;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace NineLives.Framework.Unity.UI
{
    public class SeparateUISceneScreenProvider : IScreensProvider
    {
        private readonly string uiSceneName;
        private readonly ISceneLoader sceneLoader;
        private readonly IScreensProvider screensSearcher;
        private IScreen[]? screens;

        public SeparateUISceneScreenProvider(string uiSceneName, ISceneLoader sceneLoader, IScreensProvider screenSearcher)
        {
            this.uiSceneName = uiSceneName;

            this.sceneLoader = sceneLoader;
            this.screensSearcher = screenSearcher;
        }

        public async Task<IScreen[]> GetScreens(IOperationProgress progress, CancellationToken cancellationToken)
        {
            await sceneLoader.LoadSceneAsync(uiSceneName,
                LocalPhysicsMode.None,
                LoadSceneMode.Additive,
                progress,
                cancellationToken);

            screens = await screensSearcher.GetScreens(progress, cancellationToken);
            return screens;
        }
    }
}


