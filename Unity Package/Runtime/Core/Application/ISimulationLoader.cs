using NineLives.Framework.Core.Persistance;
using NineLives.Framework.Core.Progress;
using System.Threading;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Application
{
    public interface ISimulationLoader
    {        
        Task<ILoadedSimulation> LoadGameAsync(string? savedGameName, IOperationProgress progress, CancellationToken cancellationToken);
        
        Task<int> UnloadGameAsync();
    }
}