using NineLives.Framework.Core.Async;
using System;

namespace NineLives.Framework.Unity.UI
{    
    public class CancelButton:BaseButton
    {        
        private ICancelable? cancelable;
        protected override void OnClick()
        {
            if(cancelable == null) throw new Exception($"Cancelable object is not set in '{GetType()}' instance");
            cancelable.Cancel();
        }

        public void Initialize(ICancelable? cancelable)
        {
            this.cancelable = cancelable;
        }
    }
}