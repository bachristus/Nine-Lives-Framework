namespace NineLives.Framework.Core.Application.Manager
{
    public abstract class ApplicationFSMState:IApplicationFSMState
    {                
        protected readonly IAppContext context;

        protected ApplicationFSMState(IAppContext context)
        {            
            this.context=context;
        }

        public abstract AppState AppState { get; }

        public abstract void Enter();

        public abstract void Exit();
    }
}