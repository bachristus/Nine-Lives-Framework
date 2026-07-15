using NineLives.Framework.Core.Application;

namespace NineLives.Framework.Core.UI
{
    public interface IScreen:IVisualElement
    {
        string Title { get; set; }
        string Id { get; }
        AppState AppState { get; }
        bool IsModal { get; }
        bool IsVisibleExclusively { get; }

        void Initialize(IApplicationContext context);
       
        void ProcessCancel();

        bool IsInteractable { get; set; }
    }
}