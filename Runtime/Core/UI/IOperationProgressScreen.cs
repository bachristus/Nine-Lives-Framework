using NineLives.Framework.Unity.UI;

namespace NineLives.Framework.Unity.Game
{
    public interface IProgressShower
    {
        IOperationProgressIndicator ProgressIndicator { get; }        
    }
}