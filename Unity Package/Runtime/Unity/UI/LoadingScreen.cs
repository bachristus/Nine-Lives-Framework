using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.Async;
using NineLives.Framework.Core.UI;
using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public class LoadingScreen : AppScreen
    {
        [SerializeField] private TextedProgressBar progressBar;
        [SerializeField] private CancelButton cancelButton;
        private IProgressAsyncOperation<ILoadedSimulation>? loadingOperation;

        protected override void Awake()
        {
            if (cancelButton == null)
            {
                cancelButton = gameObject.GetComponentInChildren<CancelButton>();
            }
        }

        public override AppState AppState => AppState.Loading;

        void OnEnable()
        {
            if (AppManager != null)
            {
                AppManager.SimulationLoadingStarted += OnGameLoadingStarted;
            }
        }

        private void OnDisable()
        {
            if (AppManager != null)
            {
                AppManager.SimulationLoadingStarted -= OnGameLoadingStarted;
            }
        }

        private void OnGameLoadingStarted(IProgressAsyncOperation<ILoadedSimulation> operation)
        {
            loadingOperation = operation;
            loadingOperation.Finished += OnLoadingOperationFinished;
            progressBar.SetProgressReporter(loadingOperation.Progress);
            cancelButton.Initialize(loadingOperation);
        }

        private void OnLoadingOperationFinished(IProgressAsyncOperation<ILoadedSimulation> operation)
        {
            if (loadingOperation != null)
            {
                loadingOperation.Finished -= OnLoadingOperationFinished;
            }
            progressBar.SetProgressReporter(null);
            cancelButton.Initialize(null);
            loadingOperation = null;
        }

        //protected virtual void OnDestroy()
        //{
        //    if (AppManager != null)
        //    {
        //        AppManager.SimulationLoadingStarted -= OnGameLoadingStarted;
        //    }
        //}
    }
}