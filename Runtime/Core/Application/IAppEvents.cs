using System;

namespace NineLives.Framework.Core.Application
{
    public interface IAppEvents
    {        
        event Action<string?> StartGameRequested;
        event Action PauseGameRequested;
        event Action ResumeGameRequested;
        event Action GoToMenuRequested;
        event Action QuitGameRequested;        
    }
}