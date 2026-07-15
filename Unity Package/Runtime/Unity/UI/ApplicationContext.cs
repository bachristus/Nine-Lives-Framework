using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.UI;

namespace NineLives.Framework.Unity
{
    public class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(IAppManager appManager, IUIRequest uIRequest)
        {
            this.AppManager = appManager;
            this.UIRequest = uIRequest;
        }

        public IAppManager AppManager { get; private set; }

        public IUIRequest UIRequest { get; private set; }
    }
}
