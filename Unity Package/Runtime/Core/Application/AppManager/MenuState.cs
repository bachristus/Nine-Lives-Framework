namespace NineLives.Framework.Core.Application.Manager
{
    public class MenuState : ApplicationFSMState
    {        
        public override AppState AppState => AppState.Menu;
        private readonly bool unloadGameOnEnter;
        public MenuState(IAppStateContext context, bool unloadGameOnEnter=true) : base(context)
        {
            this.unloadGameOnEnter = unloadGameOnEnter;
        }

        public override void Enter()
        {
            if (unloadGameOnEnter)
            {
                context.SimulationProvider.UnloadGame();
            }
            
            context.StartGameRequested += OnStartGameRequested;
        }

        private void OnStartGameRequested(string? savedGameName=null)
        {
            if (context.SimulationProvider.IsSimulationReady(savedGameName))
            {
                context.ChangeState(context.Playing);
            }
            else
            {
                context.PlaceGameLoadingRequest(savedGameName);
                context.ChangeState(context.Loading);
            }
        }

        public override void Exit()
        {
            context.StartGameRequested -= OnStartGameRequested;
        }           
    }
}
