using System.Threading;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Persistance
{
    public interface IGameSaveTextSaverLoader
    {
        Task<string> LoadTextByNameAsync(string saveName, CancellationToken cancellationToken = default);

        Task SaveTextByNameAsync(string saveName, string content, CancellationToken cancellationToken = default);
    }
}