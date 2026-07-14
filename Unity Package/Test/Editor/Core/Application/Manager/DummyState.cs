
namespace NineLives.Framework.Core.Application.Manager.Tests
{
    internal class DummyState : IApplicationFSMState
    {
        public AppState AppState => throw new System.NotImplementedException();

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}