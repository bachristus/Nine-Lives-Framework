using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Persistance
{
    public class UserDirectoryFilesGameSaverLoader : IGameSaveTextSaverLoader
    {
        private readonly string saveDirectory;
        private readonly string extension;

        public UserDirectoryFilesGameSaverLoader(string saveDirectory, string extension = ".json")
        {
            this.saveDirectory = saveDirectory;
            this.extension = extension;
        }

        public Task<string> LoadTextByNameAsync(
           string name,
           CancellationToken cancellationToken = default)
        {
            string? path = TryFindFileByName(name) ?? throw new FileNotFoundException("Save file '{name}' not found ");
            return File.ReadAllTextAsync(
                    path,
                    cancellationToken);
        }

        public Task SaveTextByNameAsync(string saveName, string content, CancellationToken cancellationToken = default)
        {
            string path = GetFullFilePath(saveName);
            if (File.Exists(path))
            {
                throw new InvalidOperationException($"Save file '{saveName}' already exists");
            }
            else
            {
                return File.WriteAllTextAsync(
                    path,
                    content,
                    cancellationToken);
            }
        }

        public string? TryFindFileByName(string saveName)
        {
            string path = GetFullFilePath(saveName);

            return File.Exists(path) ? path : null;
        }

        private string GetFullFilePath(string saveName) => Path.Combine(saveDirectory, saveName + extension);
    }
}