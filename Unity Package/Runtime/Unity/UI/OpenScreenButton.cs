using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public class OpenScreenButton : ApplicationContextButton
    {
        [SerializeField] private ScreenIdSO targetScreen;

        protected override void OnClick()
        {
            Debug.Log($"OpenScreenButton clicked, opening screen: {targetScreen.UniqueId}");
            context.UIRequest.ShowScreen(targetScreen.UniqueId);
        }
    }
}
