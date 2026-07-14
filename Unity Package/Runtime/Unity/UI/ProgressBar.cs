using NineLives.Framework.Core.Application;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace NineLives.Framework.Unity.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressFill;

        public void SetProgress(float value)
        {
            Debug.Log($"{GetType()}: SetProgress({value})");
            progressFill.fillAmount = value;
        }
    }
}