using System;
using NineLives.Framework.Core.Application.Tests;

namespace NineLives.Framework.Core.Application.Manager.Tests
{
    internal class DummyGameContext : IAppContext
    {
        private GameLoadingRequest? request;

        public ISimulationTime SimulationTime { get; private set; }
        public DummyGameContext(): this(new DummyTime(), new DummySimulationProvider()) { }
        public DummyGameContext(ISimulationTime time, ISimulationProvider simulationProvider)
        {
            this.SimulationTime = time;
            this.SimulationProvider = simulationProvider;

            Pause = new DummyState();
            Playing = new DummyState();
            Menu = new DummyState();
            Loading = new DummyState();
        }

        public ISimulationProvider SimulationProvider { get; private set; } 

        public IApplicationFSMState Pause { get; set; }
        public IApplicationFSMState Playing { get; set; }
        public IApplicationFSMState Menu { get; set; }
        public IApplicationFSMState Loading { get; set; }

        public IApplicationFSMState? CurrentState { get; private set; }

        public GameLoadingRequest? GameLoadingRequest=>request;

        public event Action<string?>? StartGameRequested;
        public event Action? PauseGameRequested;
        public event Action? ResumeGameRequested;
        public event Action? GoToMenuRequested;
        public event Action? QuitGameRequested;
        public event Action? LoadingCancellationRequested;

        public void ChangeState(IApplicationFSMState gameState)
        {
            CurrentState = gameState;
        }

        internal void RequestedPause()
        {
            PauseGameRequested?.Invoke();
        }

        internal void RequestResumePlaying()
        {
            ResumeGameRequested?.Invoke();
        }

        internal void RequestPlayingGame(string? gameName=null)
        {
            StartGameRequested?.Invoke(gameName);
        }

        public void PlaceGameLoadingRequest(string? savedGameName)
        {
            this.request = new GameLoadingRequest(savedGameName); 
        }

        public GameLoadingRequest? RetrieveGameLoadingRequest()
        {
            var req = request;
            request = null;
            return req;
        }

        internal void InvokeLoadingCancellationRequested()
        {
            LoadingCancellationRequested?.Invoke();
        }
    }
}