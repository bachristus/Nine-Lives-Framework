using NineLives.Framework.Core.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NineLives.Framework.Unity.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DialogScreen : AppScreen, IDialogScreen
    {
        [SerializeField] TMP_Text message;
        [SerializeField] RectTransform buttonsHolder;
        [SerializeField] Button buttonPrefab;

        public string Message { get => message.text; set => message.text = value; }

        public event Action<DialogButtonInfo>? Closed;

        internal void AddButtons(DialogButtonInfo[] buttons)
        {
            foreach (var buttonInfo in buttons)
            {
                var button = Instantiate<Button>(buttonPrefab,
                        new InstantiateParameters()
                        {
                            parent = buttonsHolder
                        });
                var text = button.GetComponentInChildren<TMP_Text>();
                if (text != null)
                {
                    text.text = buttonInfo.Caption;
                }
                button.onClick.AddListener(() => OnButtonClick(buttonInfo));
            }
        }

        private void OnButtonClick(DialogButtonInfo buttonInfo)
        {
            Closed?.Invoke(buttonInfo);
        }
    }
}