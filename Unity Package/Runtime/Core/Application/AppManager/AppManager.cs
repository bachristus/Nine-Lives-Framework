using NineLives.Framework.Core.Async;
using System;

namespace NineLives.Framework.Core.Application.Manager
{   
    public class AppManager : IAppManager, IAppStateHolder, IAppContext, IDisposable
    {
        private readonly ISimulationProvider simulationProvider;
        private readonly ISimulationTime time;
        private readonly AppFSM fsm;
        private GameLoadingRequest? loadingRequest;

        public event Action<string?>? StartGameRequested;
        public event Action? PauseGameRequested;
        public event Action? ResumeGameRequested;
        public event Action? GoToMenuRequested;
        public event Action? QuitGameRequested;
        public event Action? LoadingCancellationRequested;
        public event Action<AppState>? AppStateChanged;
        public event Action<IProgressAsyncOperation<ILoadedSimulation>>? SimulationLoadingStarted;

        public void CancelLoadingGame() => LoadingCancellationRequested?.Invoke();

        public void GoToMenu() => GoToMenuRequested?.Invoke();

        public void PauseGame() => PauseGameRequested?.Invoke();

        public void QuitGame() => QuitGameRequested?.Invoke();

        public void ResumeGame() => ResumeGameRequested?.Invoke();

        public void StartGame(string? savedGameName) => StartGameRequested?.Invoke(savedGameName);

        public ISimulationProvider SimulationProvider => simulationProvider;                
        public IApplicationFSMState Pause { get; private set; }
        public IApplicationFSMState Playing { get; private set; }        
        public IApplicationFSMState Menu { get; private set; }        
        public IApplicationFSMState Loading { get; private set; }

        public ISimulationTime SimulationTime => time;

        public AppManager(ISimulationProvider simulation, ISimulationTime time)
        {            
            this.simulationProvider = simulation;
            this.time = time;

            Playing = new PlayingState(this);
            Menu = new MenuState(this);
            Pause = new PauseState(this);
            var loading = new LoadingState(this);
            loading.LoadingStarted += OnGameLoadingStarted;
            Loading = loading;

            fsm = new AppFSM();

            fsm.StateChanged += OnFSMStateChanged;
            QuitGameRequested += OnQuitGameRequested;
        }

        private void OnGameLoadingStarted(IProgressAsyncOperation<ILoadedSimulation> loadingOperation)
        {
            SimulationLoadingStarted?.Invoke(loadingOperation);
        }

        private void OnFSMStateChanged(AppState stateId)
        {
            AppStateChanged?.Invoke(stateId);
        }

        public void Start()
        {
            ChangeState(Menu);
        }

        private void OnQuitGameRequested()
        {            
            fsm.Dispose();           
        }        

        public void ChangeState(IApplicationFSMState state)
        {
            fsm.ChangeState(state);            
        }

        public void Dispose()
        {
            fsm.Dispose();
        }

        public void PlaceGameLoadingRequest(string? savedGameName)
        {
            this.loadingRequest=new GameLoadingRequest(savedGameName);
        }

        public GameLoadingRequest? RetrieveGameLoadingRequest()
        {
            var request = this.loadingRequest;
            this.loadingRequest = null;
            return request;
        }
    }
}

