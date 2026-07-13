using System.Threading;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Persistance
{
    public interface ISimulationStateSaverLoader<TSimulationState> where TSimulationState : ISimulationState
    {
        Task Save(string saveName, TSimulationState gameState, CancellationToken cancellationToken = default);
        Task<TSimulationState> Load(string saveName, CancellationToken cancellationToken = default);
    }
}