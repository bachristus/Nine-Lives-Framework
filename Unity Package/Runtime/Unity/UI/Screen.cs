using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.UI;
using TMPro;
using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AppScreen : VisualUIElement, IScreen
    {
        [SerializeField] TMP_Text title;
        [SerializeField] private ScreenIdSO screenIdSO;
        [SerializeField] private bool isModal;
        [SerializeField] private bool isVisibleExclusively;

        private IUIRequest? uiRequest;
        public virtual IUIRequest? UIRequest
        {
            get => uiRequest;

            set
            {
                uiRequest = value;
                if (uiRequest != null)
                {
                    InitializeUIRequestButtons(uiRequest);
                }
            }
        }

        public virtual IAppManager? AppManager { get; set; }
        public string Id => screenIdSO.UniqueId;
        public virtual AppState AppState => AppState.None;
        public bool IsModal => isModal;

        private CanvasGroup? canvasGroup;

        private bool isInteractable = true;
        public bool IsInteractable
        {
            get => isInteractable;
            set
            {
                isInteractable = value;
                if (canvasGroup != null)
                {
                    canvasGroup.interactable = isInteractable;
                    //canvasGroup.blocksRaycasts = true;
                }
            }
        }
        public bool IsVisibleExclusively => isVisibleExclusively;

        public string Title { get => title.text; set => title.text = value; }
        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void InitializeUIRequestButtons(IUIRequest uiRequest)
        {
            var uiRequestButtons = gameObject.GetComponentsInChildren<UIRequestButton>();

            for (int i = 0; i < uiRequestButtons.Length; i++)
            {
                uiRequestButtons[i].Initialize(uiRequest);
            }
        }

        public void ProcessCancel()
        {
            if (IsInteractable)
            {
                OnCancelPressed();
            }
        }

        protected virtual void OnCancelPressed()
        {
        }
    }
}