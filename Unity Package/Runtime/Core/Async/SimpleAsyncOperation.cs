using System;
using System.Threading.Tasks;

namespace NineLives.Framework.Core.Async
{
    public class SimpleAsyncOperation<TResult> : ISimpleAsyncOperation<TResult>
    { 
        public SimpleAsyncOperation(string name, Func<Task<TResult>> asyncMethod)
        {
            Name = name;
            this.asyncMethod=asyncMethod;
        }

        public OperationStatus Status { get; private set; } = OperationStatus.Idle;

        public Exception? Exception { get; private set; }

        public string Name { get; private set; }

        private Func<Task<TResult>> asyncMethod;

        public Task<TResult?>? Task { get; set; }

        public event Action<ISimpleAsyncOperation<TResult>>? Started;
        public event Action<ISimpleAsyncOperation<TResult>>? Finished;

        public event Action<ISimpleAsyncOperation<TResult>>? Completed;
        public event Action<ISimpleAsyncOperation<TResult>>? Failed;

        private void Complete()
        {
            Status = OperationStatus.Completed;
            Completed?.Invoke(this);
        }

        private void Fail(Exception exception)
        {
            Status = OperationStatus.Failed;
            Exception = exception;
            Failed?.Invoke(this);
        }

        public void Start()
        {      
            Task=StartAsync(asyncMethod);
            Status = OperationStatus.Running;
            Started?.Invoke(this);
        }

       

        private async Task<TResult?> StartAsync(Func<Task<TResult>> asyncMethod)
        {
            try
            {
                var result = await asyncMethod();

                Complete();
                return result;
            }            
            catch (Exception ex)
            {
                Fail(ex);
            }
            finally
            {
                Finished?.Invoke(this);
            }
            return default;
        }
    }
}