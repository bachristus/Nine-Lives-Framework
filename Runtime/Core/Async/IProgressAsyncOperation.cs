using NineLives.Framework.Core.Progress;
using System;

namespace NineLives.Framework.Core.Async
{
    public interface IProgressAsyncOperation<TResult> : ICancelable
    {
        string Name { get; }
        IOperationProgressReporter Progress { get; }

        event Action<IProgressAsyncOperation<TResult>> CompletedSuccesfully;
        event Action<IProgressAsyncOperation<TResult>> Failed;
        event Action<IProgressAsyncOperation<TResult>> Cancelled;
        event Action<IProgressAsyncOperation<TResult>> Finished;

        
        void Start();
    }
}