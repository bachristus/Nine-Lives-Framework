using NineLives.Framework.Core.Application;

namespace NineLives.Framework.Unity.UI
{
    public abstract class ApplicationContextButton : BaseButton
    {
        protected IApplicationContext context;        

        public void Initialize(IApplicationContext context)
        {
            this.context = context;
        }
    }    
}
