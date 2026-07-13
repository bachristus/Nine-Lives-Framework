using NineLives.Framework.Core.UI;
using System;
using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public abstract class VisualUIElement: MonoBehaviour, IVisualElement
    {
        public virtual bool IsVisible
        {
            get => gameObject.activeSelf;
            set 
            {
                if (value != gameObject.activeSelf)
                {
                    gameObject.SetActive(value);
                    VisibleChanged?.Invoke(this);
                }
            }            
        }

        //public static void SetActiveWithParents(GameObject gameObject, bool value)
        //{
        //    Transform current = gameObject.transform;

        //    while (current != null)
        //    {
        //        current.gameObject.SetActive(value);
        //        current = current.parent;
        //    }
        //}

        public event Action<IVisualElement>? VisibleChanged;       
    }
}