using NineLives.Framework.Core.UI;
using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public class DialogProvider :MonoBehaviour, IDialogProvider
    {
        [SerializeField] private DialogScreen prefab;
        [SerializeField] private Canvas dialogParent;

        public IDialogScreen GetDialog(DialogArguments dialogArguments)
        {
            var dialog = GameObject.Instantiate<DialogScreen>(prefab,
               new InstantiateParameters()
               {
                   parent = dialogParent.transform,
                   originalImmutable = true,
                   worldSpace = false
               });

            PlaceInTheCenterOfParent(dialog);

            dialog.Title = dialogArguments.Title;
            dialog.Message = dialogArguments.Message ?? "";
            dialog.AddButtons(dialogArguments.Buttons);

            return dialog;
        }

        private static void PlaceInTheCenterOfParent(DialogScreen dialog)
        {
            RectTransform rect = dialog.GetComponent<RectTransform>();

            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);

            rect.anchoredPosition = Vector2.zero;
            rect.localScale = Vector3.one;
        }

        public void Release(IDialogScreen dialogScreen)
        {
            if(dialogScreen is MonoBehaviour mono)
            {
                DestroyImmediate(mono);
            }
        }
    }
}

    
