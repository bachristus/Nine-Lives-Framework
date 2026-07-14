using System;

namespace NineLives.Framework.Core.UI
{
    public interface IUIEvents
    {
        event Action<CurrentScreenId> ShowScreenRequested;
        event Action GoBackRequested;
    }
}