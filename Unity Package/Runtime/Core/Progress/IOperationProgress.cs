using System;

namespace NineLives.Framework.Core.Progress
{
    public interface IOperationProgress : IProgress<OperationProgressData> 
    { 
        void Report(float progress, string operationStage);
    }
}
    
