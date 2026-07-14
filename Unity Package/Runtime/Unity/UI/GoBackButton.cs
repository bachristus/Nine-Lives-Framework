using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public class GoBackButton : UIRequestButton
    {
        public override void OnClick()
        {
            Debug.Log($"GoBackButton clicked, going back to previous screen");
            ui.GoBackToPreviousScreen();
        }
    }
}
