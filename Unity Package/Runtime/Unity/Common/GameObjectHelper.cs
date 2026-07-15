using System.Linq;
using UnityEngine;

namespace NineLives.Framework.Unity
{
    public static class GameObjectHelper
    {
        public static T[] FindAllMonoBehavioursOfType<T>(FindObjectsInactive findObjectsInactive)
        {
            return GameObject.FindObjectsByType<MonoBehaviour>(findObjectsInactive)
                             .Where(behaviour => behaviour is T)
                             .Cast<T>()
                             .ToArray();
        }

        public static T? FindFirstMonoBehavioursOfType<T>(FindObjectsInactive findObjectsInactive)
        {
            var behaviours = GameObject.FindObjectsByType<MonoBehaviour>(findObjectsInactive);

            foreach (var b in behaviours)
            {
                if (b is T result)
                    return result;
            }

            return default;
        }
    }
}