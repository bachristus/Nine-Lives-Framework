using NineLives.Framework.Core.Async;

namespace NineLives.Framework.Core.Application
{
    public interface ISimulationProvider
    {        
        bool IsSimulationReady(string? savedGameName);
        IProgressAsyncOperation<ILoadedSimulation> Load(string? savedGameName, int cancelAfterMilliseconds=0);
        
        ISimpleAsyncOperation<int> UnloadGame();
    }
}