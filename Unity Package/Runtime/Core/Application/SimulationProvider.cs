using NineLives.Framework.Core.Async;
using NineLives.Framework.Core.Progress;
using System.Threading;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Application
{
    public class SimulationProvider : ISimulationProvider
    {
        private readonly ISimulationLoader loader;

        private ILoadedSimulation? simulation ; 

        public SimulationProvider(ISimulationLoader loader)
        {
            this.loader=loader;
        }

        public IProgressAsyncOperation<ILoadedSimulation> Load(string? savedGame, int cancelAfterMilliseconds=0)
        {
            return new ProgressAsyncOperation<ILoadedSimulation>(
                savedGame==null?"Loading new game":$"Loading game '{savedGame}'", 
                (progress,cancellationToken)=>LoadAsync(savedGame, progress, cancellationToken),
                cancelAfterMilliseconds);            
        }

        private async Task<ILoadedSimulation> LoadAsync(string? savedGame, IOperationProgress progress, CancellationToken cancellationToken)
        {
            if (simulation != null) await Unload();
            return simulation = await loader.LoadGameAsync(savedGame, progress, cancellationToken);
        }

        public ISimpleAsyncOperation<int> UnloadGame()
        {
            return new SimpleAsyncOperation<int>(
               $"Unloading game",
               Unload
               ); 
        }

        private Task<int> Unload()
        {
            simulation = null;
            return loader.UnloadGameAsync();
        }

        public bool IsSimulationReady(string? savedGameName)
        {
            return simulation != null && simulation.Name == savedGameName;
        }
    }
}