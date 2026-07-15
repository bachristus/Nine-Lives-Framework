using System.Threading.Tasks;
using UnityEngine;

namespace NineLives.Framework.Unity.Scenes
{
    public static class AsyncOperationExtensions
    {
        public static Task AsTask(this AsyncOperation operation)
        {
            if (operation.isDone)
                return Task.CompletedTask;

            var tcs = new TaskCompletionSource<bool>();

            operation.completed += _ => tcs.SetResult(true);

            return tcs.Task;
        }
    }
}