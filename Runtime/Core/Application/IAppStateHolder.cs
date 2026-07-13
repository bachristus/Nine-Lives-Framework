using System;

namespace NineLives.Framework.Core.Application
{
    public interface IAppStateHolder
    {
        event Action<AppState> AppStateChanged;
    }
}