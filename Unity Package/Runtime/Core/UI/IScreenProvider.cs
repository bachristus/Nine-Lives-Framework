using NineLives.Framework.Core.Progress;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.UI
{
    public interface IScreensProvider
    {
        Task<IScreen[]> GetScreens(IOperationProgress progress, CancellationToken token);
    }
}