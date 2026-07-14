using System.Collections.Generic;

namespace NineLives.Framework.Core.Persistance
{
    public interface ISimulationState
    {
        IEnumerable<string> RequiredScenes {  get; }
    }
}