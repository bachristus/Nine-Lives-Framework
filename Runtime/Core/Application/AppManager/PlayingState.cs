namespace NineLives.Framework.Core.Application.Manager
{
    public class PlayingState : ApplicationFSMState
    {
        public PlayingState(IAppContext context) : base(context)
        {
        }

        public override AppState AppState => AppState.Playing;

        public override void Enter()
        {
            context.SimulationTime.Resume();
            context.PauseGameRequested += OnPauseGameRequested;
        }

        private void OnPauseGameRequested()
        {
            context.ChangeState(context.Pause);
        }

        public override void Exit()
        {
            context.PauseGameRequested -= OnPauseGameRequested;
        }        
    }    
}
