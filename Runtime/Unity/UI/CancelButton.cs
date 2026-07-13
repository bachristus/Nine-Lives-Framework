using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.Async;
using System;
using UnityEngine;

namespace NineLives.Framework.Unity.UI
{    
    public class CancelButton:MonoBehaviour
    {        
        private ICancelable? cancelable;
        public void OnButtonClick()
        {
            if(cancelable == null) throw new Exception($"Cancellation token is not set in '{GetType()}' instance");
            cancelable.Cancel();
        }

        public void SetCancelable(ICancelable? cancelable)
        {
            this.cancelable = cancelable;
        }
    }
}