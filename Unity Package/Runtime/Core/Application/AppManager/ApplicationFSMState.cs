namespace NineLives.Framework.Core.Application.Manager
{
    public abstract class ApplicationFSMState:IApplicationFSMState
    {                
        protected readonly IAppStateContext context;

        protected ApplicationFSMState(IAppStateContext context)
        {            
            this.context=context;
        }

        public abstract AppState AppState { get; }

        public abstract void Enter();

        public abstract void Exit();
    }
}