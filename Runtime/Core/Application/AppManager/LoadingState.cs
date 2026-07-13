using NineLives.Framework.Core.Async;
using System;

namespace NineLives.Framework.Core.Application.Manager
{   
    public  class LoadingState : ApplicationFSMState
    {        
        private IProgressAsyncOperation<ILoadedSimulation>? loadingOp;

        public event Action<IProgressAsyncOperation<ILoadedSimulation>>? LoadingStarted;

        public LoadingState(IAppContext context) : base(context)
        {           
        }        

        public override AppState AppState => AppState.Loading;

        public override void Enter()
        {
            var gameLoadingRequest = context.RetrieveGameLoadingRequest() ?? throw new InvalidOperationException("Game loading request is null. Cannot load a game without a loading request");   

            loadingOp= context.SimulationProvider.Load(gameLoadingRequest.RequestedGameName); 
            
            loadingOp.CompletedSuccesfully += OnLoaded;
            loadingOp.Cancelled += OnCancelled;
            loadingOp.Failed += OnFailed;            
            
            loadingOp.Start();
            LoadingStarted?.Invoke(loadingOp);
        }

        private void OnCancelled(IProgressAsyncOperation<ILoadedSimulation> operation)
        {
            context.ChangeState(context.Menu);
        }

        private void OnFailed(IProgressAsyncOperation<ILoadedSimulation> operation)
        {
            context.ChangeState(context.Menu);
        }

        private void OnLoaded(IProgressAsyncOperation<ILoadedSimulation> operation)
        {
            context.ChangeState(context.Playing);
        }

        private void OnLoadingCancellationRequested()
        {
            loadingOp?.Cancel();
            context.ChangeState(context.Menu);
        }        

        public override void Exit()
        {
            if (loadingOp != null)
            { 
                loadingOp.CompletedSuccesfully -= OnLoaded;
                loadingOp.Cancelled -= OnCancelled;
                loadingOp.Failed -= OnFailed;
                loadingOp.Cancel();
                loadingOp= null;
            }
        }
    }
}
