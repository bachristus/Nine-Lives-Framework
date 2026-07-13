using NineLives.Framework.Core.UI;
using System.Collections.Generic;

namespace NineLives.Framework.Unity.UI
{
    public class UnityScreenSceneSearcher : IScreenProvider
    {
        public IEnumerable<IScreen> GetScreens()
        {
            return GameObjectHelper.FindAllMonoBehavioursOfType<IScreen>(UnityEngine.FindObjectsInactive.Include);
        }
    }
}