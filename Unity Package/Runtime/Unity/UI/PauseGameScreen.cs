using NineLives.Framework.Core.Application;
using NineLives.Framework.Unity.UI;
using UnityEngine;
using UnityEngine.UI;

namespace NineLives.Village.Game
{
    internal class PauseGameScreen : AppScreen
    {
        override public AppState AppState => AppState.Pause;        
    }
}