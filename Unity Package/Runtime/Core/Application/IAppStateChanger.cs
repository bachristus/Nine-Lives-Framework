namespace NineLives.Framework.Core.Application.Manager
{
    public interface IAppStateChanger
    {
        void ChangeState(IApplicationFSMState appState);
        IApplicationFSMState Pause { get; }
        IApplicationFSMState Playing { get; }
        IApplicationFSMState Menu { get; }
        IApplicationFSMState Loading { get; }
    }
}