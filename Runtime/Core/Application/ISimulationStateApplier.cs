using NineLives.Framework.Core.Persistance;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Application
{
    public interface ISimulationStateApplier<TSimulationState> where TSimulationState : ISimulationState
    {
        Task Apply(TSimulationState savedGame);
    }
}