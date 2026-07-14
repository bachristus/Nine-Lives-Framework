using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.UI;
using System;
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

        protected IUIRequest? UIRequest { get; private set; }

        private bool isInitialized;        
        protected IAppManager? GameManager { get; set; }
        public CurrentScreenId Id => screenIdSO.Id;        
        public virtual AppState AppState => AppState.None;
        public bool IsModal => isModal;

        private CanvasGroup? canvasGroup;

        private bool isInteractable = true;
        public bool IsInteractable
        {
            get=> isInteractable;
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

        public virtual void Initialize(IAppManager gameManager, IUIRequest uiRequest)
        {
            if (isInitialized) throw new Exception($"Method Initialize() is called twice on '{GetType()}' instance");

            GameManager = gameManager;
            UIRequest = uiRequest;            

            canvasGroup=GetComponent<CanvasGroup>();

            InitializeUIRequestButtons(uiRequest);

            isInitialized = true;
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