using System;

namespace NineLives.Framework.Core.Application
{
    public interface IAppStateDispatcher
    {        
        event Action<AppState> AppStateChanged;
    }
}