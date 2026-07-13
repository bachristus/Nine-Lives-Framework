namespace NineLives.Framework.Core.Application.Tests
{
    internal class DummyTime : ISimulationTime
    {
        public bool IsPaused { get; set; }
        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused=false;
        }
    }
}