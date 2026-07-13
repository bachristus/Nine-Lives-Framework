using NineLives.Framework.Core.Application;

namespace NineLives.Framework.Unity.UI
{
    public class HUDScreen: AppScreen
    {
        public override AppState AppState => AppState.Playing;

        protected override void OnCancelPressed()
        {
            this.GameManager?.PauseGame();
        }
    }
}