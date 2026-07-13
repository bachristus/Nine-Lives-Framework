using System.Collections.Generic;

namespace NineLives.Framework.Core.Application.Tests
{
    public class DummySimulation : ILoadedSimulation
    {
        private string? savedGameName;
        private readonly string[] loadedScenes;

        public DummySimulation(string? savedGameName, string[]? loadedScenes =null)
        {
            this.savedGameName = savedGameName;
            this.loadedScenes= loadedScenes ?? (new string[] { });
        }

        public string? Name => savedGameName;

        public IEnumerable<string> LoadedScenes => loadedScenes;
    }
}
