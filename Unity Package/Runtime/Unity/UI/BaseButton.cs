using UnityEngine;
using UnityEngine.UI;

namespace NineLives.Framework.Unity.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour
    {
        protected Button button;
        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if (button != null)
            {
                button.onClick.AddListener(OnClick);
            }
        }

        private void OnDisable()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(OnClick);
            }
        }

        protected abstract void OnClick();
    }
}
