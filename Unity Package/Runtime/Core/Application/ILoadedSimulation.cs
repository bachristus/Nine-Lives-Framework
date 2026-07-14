
using System.Collections.Generic;

namespace NineLives.Framework.Core.Application
{
    public interface ILoadedSimulation
    {
        string? Name { get; }
        IEnumerable<string> LoadedScenes { get; }
    }
}