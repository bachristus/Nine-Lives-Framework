using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public class GoBackButton : ApplicationContextButton
    {
        protected override void OnClick()
        {
            Debug.Log($"GoBackButton clicked, going back to previous screen");
            context.UIRequest.GoBackToPreviousScreen();
        }
    }
}
