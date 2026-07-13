namespace NineLives.Framework.Core.Application
{
    public class GameLoadingRequest
    {
        public string? RequestedGameName { get; private set; }

        public GameLoadingRequest(string? requestedGameName)
        {
            RequestedGameName = requestedGameName;
        }
    }
}