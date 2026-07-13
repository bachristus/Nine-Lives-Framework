
using NineLives.Framework.Core.Async;
using System;

namespace NineLives.Framework.Core.Application.Tests
{
    internal class DummyGameUnloadingOperation : ISimpleAsyncOperation<int>
    {
        public string Name => throw new NotImplementedException();

        public event Action<ISimpleAsyncOperation<int>>? Completed;
        public event Action<ISimpleAsyncOperation<int>>? Failed;

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}