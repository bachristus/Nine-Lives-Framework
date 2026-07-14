using NineLives.Framework.Core.Progress;
using TMPro;
using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public class TextedProgressBar : ProgressBar, IOperationProgressIndicator
    {
        [SerializeField] private TMP_Text text;
        private IOperationProgressReporter? progressReporter;

        public void SetProgressReporter(IOperationProgressReporter? newReporter)
        {
            if (progressReporter != null)
                progressReporter.Changed -= OnProgressChanged;

            progressReporter = newReporter;

            if (progressReporter != null)
                progressReporter.Changed += OnProgressChanged;
        }

        private void OnProgressChanged(OperationProgressData progressData)
        {
            SetProgress(progressData.progress01);
            text.SetText(progressData.currentOperation);
        }

        private void OnDestroy()
        {
            if (progressReporter != null)
                progressReporter.Changed -= OnProgressChanged;
        }
    }
}