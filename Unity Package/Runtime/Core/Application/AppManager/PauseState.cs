namespace NineLives.Framework.Core.Application.Manager
{
    public class PauseState : ApplicationFSMState
    {
        public PauseState(IAppStateContext context) : base(context)
        {
        }

        public override AppState AppState => AppState.Pause;

        public override void Enter()
        {   
            context.SimulationTime.Pause();
            context.GoToMenuRequested += OnMenuRequested;
            context.ResumeGameRequested += OnResumeGameRequested;
        }

        private void OnResumeGameRequested()
        {
            context.ChangeState(context.Playing);
        }

        private void OnMenuRequested()
        {
            context.ChangeState(context.Menu);
        }

        public override void Exit()
        {            
            context.ResumeGameRequested -= OnResumeGameRequested;
            context.GoToMenuRequested -= OnMenuRequested;
        }
    }
}
