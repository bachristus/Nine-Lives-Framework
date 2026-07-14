using System;

namespace NineLives.Framework.Core.Async
{
    public interface ISimpleAsyncOperation<TResult>
    {
        string Name { get; }

        event Action<ISimpleAsyncOperation<TResult>> Completed;
        event Action<ISimpleAsyncOperation<TResult>> Failed;

        void Start();
    }
}