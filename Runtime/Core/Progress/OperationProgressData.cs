using System;
namespace NineLives.Framework.Core.Progress
{
    public readonly struct OperationProgressData
    {
        public readonly float progress01;
        public readonly string currentOperation;
        
        public OperationProgressData(float progress, string currentOperation = "")
        {
            this.progress01 = Math.Clamp(progress, 0f, 1f);
            this.currentOperation = currentOperation;            
        }
    }
}

