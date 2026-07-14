namespace NineLives.Framework.Core.Application.Manager
{
    public interface IApplicationFSMState
    {
        AppState AppState { get; }
        void Enter();
        void Exit();        
    }
}