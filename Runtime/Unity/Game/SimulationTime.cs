using NineLives.Framework.Core.Application;
using UnityEngine;

namespace NineLives.Framework.Unity.Game
{
    public class SimulationTime: ISimulationTime
    {
        public bool IsPaused => Time.timeScale==0;

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }
    }
}
