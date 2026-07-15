using NineLives.Framework.Core.UI;

namespace NineLives.Framework.Core.Application
{
    public interface IApplicationContext
    {
        IAppManager AppManager { get; }
        IUIRequest UIRequest { get; }
    }
}
