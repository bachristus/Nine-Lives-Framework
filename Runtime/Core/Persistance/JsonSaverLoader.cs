using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Persistance
{
    public sealed class JsonSaverLoader<TGameState> : ISimulationStateSaverLoader<TGameState> where TGameState: ISimulationState
    {
        private readonly IGameSaveTextSaverLoader saverLoader;
        public JsonSaverLoader(IGameSaveTextSaverLoader saverLoader)
        {
            this.saverLoader = saverLoader;
        }
        public async Task<TGameState> Load(string saveName, CancellationToken cancellationToken = default)
        {
            string json =await saverLoader.LoadTextByNameAsync(saveName, cancellationToken);

            TGameState? save = JsonSerializer.Deserialize<TGameState>(json);

            return save == null ? throw new InvalidDataException($"Game data was not deserialized properly for type='{typeof(TGameState)}'") : save;
        }

        public Task Save(string saveName, TGameState gameState, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(gameState, new JsonSerializerOptions { WriteIndented = true });
            return saverLoader.SaveTextByNameAsync(saveName, json, cancellationToken);
        }
    }
}