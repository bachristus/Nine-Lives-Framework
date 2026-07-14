using NineLives.Framework.Core.Async;
using System;

namespace NineLives.Framework.Core.Application
{
    public interface ISimulationLoadingEvents
    {
        event Action<IProgressAsyncOperation<ILoadedSimulation>> SimulationLoadingStarted;
    }
}