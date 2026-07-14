using System;

namespace NineLives.Framework.Core.UI
{
    public interface IDialogScreen : IScreen
    {
        string Message { get; set; }
        event Action<DialogButtonInfo> Closed;
    }
}