using NineLives.Framework.Core.Async;
using NineLives.Framework.Core.Persistance;
using System;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Application.Tests
{
    internal class DummySimulationProvider : ISimulationProvider
    {
        public DummySimulationProvider()
        {
            
        }
        public DummySimulationProvider(IProgressAsyncOperation<ILoadedSimulation> loadingOperation)
        {             
            this.loadingOperation=loadingOperation;
        }

        public DummySimulationProvider(ILoadedSimulation simulation)
        {
            this.simulation = simulation;
        }

        private ILoadedSimulation? simulation;
        private readonly IProgressAsyncOperation<ILoadedSimulation>? loadingOperation;

        public bool IsLoaded =>simulation != null;

        public bool IsSimulationReady(string? savedGameName) => simulation != null && simulation.Name == savedGameName;

        public IProgressAsyncOperation<ILoadedSimulation> Load(string? savedGameName, int cancelAfterMilliseconds = 0)
        {            
            simulation=new DummySimulation(savedGameName);
            return loadingOperation ?? new DummyGameLoadingOperation();
        }

        public ISimpleAsyncOperation<int> UnloadGame()
        {            
            simulation = null;
            return new DummyGameUnloadingOperation();
        }
    }
}
