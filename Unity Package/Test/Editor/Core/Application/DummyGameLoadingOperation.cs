using NineLives.Framework.Core.Async;
using NineLives.Framework.Core.Progress;
using System;

namespace NineLives.Framework.Core.Application.Tests
{
    internal class DummyGameLoadingOperation : IProgressAsyncOperation<ILoadedSimulation>
    {
        public IOperationProgressReporter Progress => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public bool IsStarted { get; private set; }
        public bool WasCancelled { get; private set; }

        public event Action<IProgressAsyncOperation<ILoadedSimulation>>? CompletedSuccesfully;
        public event Action<IProgressAsyncOperation<ILoadedSimulation>>? Failed;
        public event Action<IProgressAsyncOperation<ILoadedSimulation>>? Cancelled;
        public event Action<IProgressAsyncOperation<ILoadedSimulation>>? Finished;

        public void Cancel()
        {
            WasCancelled = true;
        }

        public void InvokeFailed()
        {
            Failed?.Invoke(this);
        }

        public void InvokeCompletedSuccessfully()
        {
            CompletedSuccesfully?.Invoke(this);
        }

        public void InvokeCancelled()
        {
            Cancelled?.Invoke(this);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            IsStarted=true;
        }
    }    
}
