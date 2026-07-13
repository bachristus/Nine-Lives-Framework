using System.Collections.Generic;

namespace NineLives.Framework.Core.UI
{
    public interface IScreenProvider
    {        
        IEnumerable<IScreen> GetScreens();
    }
}