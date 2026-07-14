using NineLives.Framework.Core.Async;
using System;

namespace NineLives.Framework.Core.Application
{
    public interface IGameLoadingEvents
    {
        event Action<IProgressAsyncOperation<ILoadedSimulation>> GameLoadingStarted;
    }
}