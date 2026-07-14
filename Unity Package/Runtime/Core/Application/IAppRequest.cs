namespace NineLives.Framework.Core.Application
{
    public interface IAppRequest
    {
        void StartGame(string? savedGameName=null);
        void PauseGame();
        void ResumeGame();
        void GoToMenu();
        void QuitGame();
        void CancelLoadingGame();
    }
}