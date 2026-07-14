using NineLives.Framework.Core.Application;
using NineLives.Framework.Unity.UI;
using UnityEngine;
using UnityEngine.UI;

namespace NineLives.Village.Game
{
    internal class PauseGameScreen : AppScreen
    {
        [SerializeField] private Button resumePlayingButton;
        [SerializeField] private Button exitGameButton;

        protected virtual void Start()
        {
            resumePlayingButton.onClick.AddListener(OnResumePlayingClicked);
            exitGameButton.onClick.AddListener(OnExitGameClicked);
        }

        private void OnExitGameClicked()
        {
            AppManager?.GoToMenu();
        }

        private void OnResumePlayingClicked()
        {
            AppManager?.ResumeGame();
        }

        override public AppState AppState => AppState.Pause;

        protected override void OnCancelPressed()
        {
            OnResumePlayingClicked();
        }
    }
}