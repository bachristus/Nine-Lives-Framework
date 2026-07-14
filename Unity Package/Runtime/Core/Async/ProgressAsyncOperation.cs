using NineLives.Framework.Core.Progress;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Async
{
    public class ProgressAsyncOperation<TResult> : IProgressAsyncOperation<TResult>, IDisposable
    {       
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly Func<IOperationProgress, CancellationToken, Task<TResult>> asyncMethod;

        public ProgressAsyncOperation(string name, Func<IOperationProgress, CancellationToken, Task<TResult>> asyncMethod, int autoCancelInMilliseconds = 0)
        {
            Name = name;
            cancellationTokenSource = autoCancelInMilliseconds > 0 ? new(autoCancelInMilliseconds) : new();
            progress = new OperationProgress();
            this.asyncMethod = asyncMethod;
        }
        private readonly OperationProgress progress;
        public IOperationProgressReporter Progress => progress;

        public OperationStatus Status { get; private set; } = OperationStatus.Running;

        public Exception? Exception { get; private set; }

        public CancellationToken Token => cancellationTokenSource.Token;

        public string Name { get; private set; }
        public Task<TResult?>? Task { get; set; }

        public event Action<IProgressAsyncOperation<TResult>>? Started;
        public event Action<IProgressAsyncOperation<TResult>>? Finished;

        public event Action<IProgressAsyncOperation<TResult>>? CompletedSuccesfully;
        public event Action<IProgressAsyncOperation<TResult>>? Failed;
        public event Action<IProgressAsyncOperation<TResult>>? Cancelled;

        public void Complete()
        {
            Status = OperationStatus.Completed;
            progress.Report(1f, $"Operation '{Name}' completed successfully");
            CompletedSuccesfully?.Invoke(this);
        }

        public void Cancel()
        {
            if (Status != OperationStatus.Running)
                return;

            Status = OperationStatus.Cancelled;

            cancellationTokenSource.Cancel();

            Cancelled?.Invoke(this);
        }

        public void Fail(Exception exception)
        {
            Status = OperationStatus.Failed;

            Exception = exception;

            Failed?.Invoke(this);
        }

        public void Dispose()
        {
            cancellationTokenSource.Dispose();
        }

        public void Start()
        {
            Task = StartAsync(asyncMethod);

            Started?.Invoke(this);
        }

        public void End()
        {
            Finished?.Invoke(this);
        }

        private async Task<TResult?> StartAsync(Func<IOperationProgress, CancellationToken, Task<TResult>> asyncMethod)
        {
            try
            {
                var result = await asyncMethod(progress, Token);

                Complete();
                return result;
            }
            catch (OperationCanceledException)
            {
                Cancel();
            }
            catch (Exception ex)
            {
                Fail(ex);
            }
            finally
            {
                End();
            }
            return default;
        }
    }
}