using System;

namespace NineLives.Framework.Core.Progress
{
    public class OperationProgress:IOperationProgress, IOperationProgressReporter
    {
        public OperationProgressData Data { get; private set; }        
        public void Report(OperationProgressData value)
        {
            Data=value;

            Changed?.Invoke(Data);
        }

        public void Report(float progress, string stageName)
        {
            Report(new(progress, stageName));            
        }

        public event Action<OperationProgressData>? Changed;
    }    
}