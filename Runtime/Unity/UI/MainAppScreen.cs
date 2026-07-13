using NineLives.Framework.Core.UI;
using NineLives.Framework.Unity.UI;
using System.Collections.Generic;

namespace NineLives.Framework.Unity.Assets.Scripts.Framework.Runtime.Unity.UI
{
    public class MainAppScreen : VisualUIElement
    {
        private readonly List<AppScreen> subScreens=new();

        protected virtual void Awake()
        {
            var foundScreens = gameObject.GetComponentsInChildren<AppScreen>(includeInactive: true);
            foreach (var foundScreen in foundScreens)
            {
                Register(foundScreen);
            }
            IsVisible = false;
        }

        protected virtual void OnDestroy()
        {
            foreach (var subScreen in subScreens.ToArray())
            {
                Unregister(subScreen);
            }                        
        }

        private void Register(AppScreen subScreen)
        {
            subScreens.Add(subScreen);
            subScreen.VisibleChanged += OnSubScreenVisibleChanged;
        }
        
        private void OnSubScreenVisibleChanged(IVisualElement screen)
        {
            UpdateActive();
        }

        private void UpdateActive()
        {
            bool anyVisibleSubscreen = false;
            foreach (var subScreen in subScreens)
            {
                if(subScreen.IsVisible)
                {
                    anyVisibleSubscreen = true;
                    break;
                }
            }

            gameObject.SetActive(anyVisibleSubscreen);
        }

        private void Unregister(AppScreen subScreen)
        {
            subScreen.VisibleChanged -= OnSubScreenVisibleChanged;
            subScreens.Remove(subScreen);
        }
    }
}