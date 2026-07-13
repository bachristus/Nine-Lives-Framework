using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.Progress;
using NineLives.Framework.Core.UI;
using NineLives.Framework.Unity.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace NineLives.Framework.Unity.UI
{
    public class SeparateUISceneScreenProvider:IScreenProvider
    {
        private readonly string uiSceneName;        
        private readonly ISceneLoader sceneLoader;
        private readonly IScreenProvider screensSearcher;
        private IScreen[]? screens;

        public SeparateUISceneScreenProvider(string uiSceneName, ISceneLoader sceneLoader, IScreenProvider screenSearcher)
        {
            this.uiSceneName = uiSceneName;
            
            this.sceneLoader = sceneLoader;
            this.screensSearcher = screenSearcher;
        }

        public IEnumerable<IScreen> GetScreens()
        {
            return screens ?? throw new Exception($"{this.GetType()} was not initialized. Call Initialize() before calling this method");
        }

        public async Task Initialize(IOperationProgress progress, CancellationToken cancellationToken)
        {
            await sceneLoader.LoadSceneAsync(uiSceneName,
                LocalPhysicsMode.None,
                LoadSceneMode.Additive,
                progress,
                cancellationToken);            

            screens = screensSearcher.GetScreens().ToArray();   
        }
    }
}

    
