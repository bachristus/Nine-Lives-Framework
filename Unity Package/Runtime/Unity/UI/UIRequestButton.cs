using NineLives.Framework.Core.UI;
using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public abstract class UIRequestButton : MonoBehaviour
    {        
        protected IUIRequest ui;

        public void Initialize(IUIRequest ui)
        {
            this.ui = ui;
        }

        public abstract void OnClick();
    }

}
