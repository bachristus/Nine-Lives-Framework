
using System;

namespace NineLives.Framework.Core.Tests
{
    internal class DummyInput : IGameInput
    {
        public event Action? CancelPressed;

        public void InvokeEscapePressed() { CancelPressed?.Invoke(); }
    }
}