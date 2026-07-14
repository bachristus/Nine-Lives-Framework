using System;

namespace NineLives.Framework.Core.Progress
{
    public interface IOperationProgressReporter
    {
        event Action<OperationProgressData> Changed;
    }
}