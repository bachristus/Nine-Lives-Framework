
using System.Collections.Generic;

namespace NineLives.Framework.Core.Application
{
    public class LoadedSimulation : ILoadedSimulation
    {
        public string? Name { get; private set; }
        public IEnumerable<string> LoadedScenes { get; private set; }

        public LoadedSimulation(string? gameName, IEnumerable<string> loadedScenes)
        {
            this.Name = gameName;
            LoadedScenes = loadedScenes;
        }
    }
}