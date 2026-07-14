using System;

namespace NineLives.Framework.Core.UI
{
    public interface IUIEvents
    {
        event Action<string> ShowScreenRequested;
        event Action GoBackRequested;
    }
}