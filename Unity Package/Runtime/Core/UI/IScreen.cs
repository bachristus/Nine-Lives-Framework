using NineLives.Framework.Core.Application;

namespace NineLives.Framework.Core.UI
{
    public interface IScreen:IVisualElement
    {
        string Title { get; set; }
        CurrentScreenId Id { get; }
        AppState AppState { get; }
        bool IsModal { get; }
        bool IsVisibleExclusively { get; }

        void Initialize(IAppManager gameManager, IUIRequest uiRequest);
        void ProcessCancel();

        bool IsInteractable { get; set; }
    }
}