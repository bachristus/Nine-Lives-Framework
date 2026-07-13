using UnityEngine;

namespace NineLives.Framework.Unity.UI
{
    public class InitializingScreen:VisualUIElement, IInitializingScreen
    {
        [SerializeField] private TextedProgressBar progressBar;

        public IOperationProgressIndicator ProgressIndicator => progressBar;
        
        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
    }
}