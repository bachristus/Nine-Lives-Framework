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

        void Awake()
        {
            if (cancelButton == null)
            {
                cancelButton = gameObject.GetComponentInChildren<CancelButton>();
            }
        }
        
        public override AppState AppState => AppState.Loading;

        public override void Initialize(IAppManager gameManager, IUIRequest uiRequest/*, IGameInput input*/)
        {
            base.Initialize(gameManager, uiRequest/*, input*/);
            gameManager.GameLoadingStarted += OnGameLoadingStarted;
        }

        private void OnGameLoadingStarted(IProgressAsyncOperation<ILoadedSimulation> operation)
        {           
            loadingOperation= operation;
            loadingOperation.Finished += OnLoadingOperationFinished;
            progressBar.SetProgressReporter(loadingOperation.Progress);
            cancelButton.SetCancelable(loadingOperation);
        }

        private void OnLoadingOperationFinished(IProgressAsyncOperation<ILoadedSimulation> operation)
        {
            if (loadingOperation != null)
            {
                loadingOperation.Finished -= OnLoadingOperationFinished;
            }
            progressBar.SetProgressReporter(null);
            cancelButton.SetCancelable(null);
            loadingOperation = null;
        }        

        protected virtual void OnDestroy()
        {
            if (GameManager != null)
            {
                GameManager.GameLoadingStarted -= OnGameLoadingStarted;
            }
        }
    }
}