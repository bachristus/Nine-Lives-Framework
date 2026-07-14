using NineLives.Framework.Core.Progress;

namespace NineLives.Framework.Unity.UI
{
    public interface IOperationProgressIndicator
    {
        public void SetProgressReporter(IOperationProgressReporter reporter);
    }
}