using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.UI;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace NineLives.Framework.Unity.UI
{
    public class MainMenuScreen : MenuScreen
    {
        [SerializeField] private Button startNewGameButton;
        [SerializeField] private Button quitGameButton;

        protected virtual void Start()
        {
            startNewGameButton.onClick.AddListener(OnStartNewGameClicked);
            quitGameButton.onClick.AddListener(OnQuitGameClickedAsync);
        }

        private async void OnQuitGameClickedAsync()
        {
            var quit = new DialogButtonInfo("Quit");
            var cancel = new DialogButtonInfo("Cancel");
            var button = await UIRequest!.ShowDialog(
                new DialogArguments("Quit the game?", "Save game progress?",
                        quit,
                        cancel));

            if (button == quit) AppManager?.QuitGame();
        }

        public override AppState AppState => AppState.Menu;

        private void OnStartNewGameClicked()
        {
            AppManager?.StartGame();
        }
    }
}