using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public class OpenScreenButton : UIRequestButton
    {
        [SerializeField] private ScreenIdSO targetScreen;

        public override void OnClick()
        {
            Debug.Log($"OpenScreenButton clicked, opening screen: {targetScreen.UniqueId}");
            ui.ShowScreen(targetScreen.UniqueId);
        }
    }
}
