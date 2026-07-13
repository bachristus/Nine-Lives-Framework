using System;

namespace NineLives.Framework.Core.UI
{
    public interface IUIEvents
    {
        event Action<ScreenId> ShowScreenRequested;
        event Action GoBackRequested;
    }
}