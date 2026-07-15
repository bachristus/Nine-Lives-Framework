namespace NineLives.Framework.Core.Application.Manager
{
    public interface IAppStateContext : IAppStateChanger, IAppEvents
    {        
        ISimulationProvider SimulationProvider { get; }    
        ISimulationTime SimulationTime { get; }
        void PlaceGameLoadingRequest(string? savedGameName);
        GameLoadingRequest? RetrieveGameLoadingRequest();
    }
}