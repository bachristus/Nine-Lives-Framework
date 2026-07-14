namespace NineLives.Framework.Core.Application
{
    public interface ISimulationTime
    {
        public void Pause();

        public void Resume();

        bool IsPaused { get; }
    }
}