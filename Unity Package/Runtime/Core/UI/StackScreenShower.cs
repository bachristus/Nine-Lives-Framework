using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NineLives.Framework.Core.UI
{
    public class StackScreenShower : IScreenShower
    {
        private readonly List<IScreen> stack = new();

        public IScreen? Current => stack.Count == 0 ? null : stack[^1];

        public bool TryGoBackToPreviousScreen(out IScreen? screen)
        {
            screen = null;
            if (stack.Count == 0) return false;
            if (stack.Count == 1)
            {
                screen= stack[0];// cannot close last screen. At least one must remain visible
                return false;
            }

            var popped = stack[^1];
            stack.RemoveAt(stack.Count-1);
            Hide(popped);

            screen = stack[^1];
            

            AdjustInteractivity();
            AdjustVisibility();

            return true;
        }

        private void Hide(IScreen popped)
        {
            popped.IsVisible = false;
            popped.IsInteractable = false;
        }

        public void HideAllScreens()
        {
            foreach (var screen in stack)
            {
                Hide(screen);
            }
            stack.Clear();
        }

        public void ShowScreen(IScreen screen)
        {
            if (screen == null) throw new ArgumentNullException(nameof(screen));
            if(stack.Contains(screen)) throw new InvalidOperationException($"{GetType()} instance cannot show screen which it already shown");
            
            stack.Add(screen);
            screen.IsVisible = true;
            screen.IsInteractable = true;

            AdjustInteractivity();
            AdjustVisibility();
        }

        private void AdjustInteractivity()
        {
            var topScreen = stack[^1];
            topScreen.IsInteractable = true;

            if (stack.Count>=2)
            {
                int i = stack.Count - 2; 
                do 
                {
                    stack[i--].IsInteractable = !topScreen.IsModal;
                }
                while (i>=0 && !stack[i].IsModal); // we suppose the further iteration makes no sense as they should be adjusted before
            }
        }

        private void AdjustVisibility()
        {
            var topScreen = stack[^1];
            topScreen.IsVisible = true;

            if (stack.Count >= 2)
            {
                int i = stack.Count - 2;
                do
                {
                    stack[i--].IsVisible = !topScreen.IsVisibleExclusively;
                }
                while (i >= 0 && !stack[i].IsVisibleExclusively); // we suppose the further iteration makes no sense as they should be adjusted before
            }
        }
    }
}