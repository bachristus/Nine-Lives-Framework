using NineLives.Framework.Core.UI;
using NineLives.Framework.Unity.UI;

namespace NineLives.TopRunner
{
    public class QuitGameButton : ApplicationContextButton
    {
        protected async override void OnClick()
        {         
            var quit = new DialogButtonInfo("Quit");
            var cancel = new DialogButtonInfo("Cancel");
            var button = await context.UIRequest!.ShowDialog(
                new DialogArguments("Quit the game?", "Save game progress?",
                        quit,
                        cancel));

            if (button == quit) context.AppManager?.QuitGame();
        }
    }
}
