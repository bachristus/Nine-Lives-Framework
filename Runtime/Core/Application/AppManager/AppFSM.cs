using System;

namespace NineLives.Framework.Core.Application.Manager
{
    public class AppFSM : IDisposable
    {
        private IApplicationFSMState? currentState;

        public event Action<AppState>? StateChanged;

        public void ChangeState(IApplicationFSMState? newState)
        {
            if (newState == currentState) return;
            
            if (currentState != null)
            {
                currentState?.Exit();
            }
            if (newState != null)
            {                
                currentState = newState;
                currentState.Enter();
                StateChanged?.Invoke(currentState.AppState);
            }
        }

        internal void Stop()
        {
            ChangeState(null);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}