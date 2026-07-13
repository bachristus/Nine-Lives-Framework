using System;
using NUnit.Framework;

namespace NineLives.Framework.Core.UI.Tests
{
    public class StackScreenShowerTests
    {

        [Test]
        public void ScreensStackShower_CurrentScreenIsNullOnEmpty()
        {
            var shower = new StackScreenShower();            
            Assert.IsNull(shower.Current);
        }

        [Test]
        public void ScreensStackShower_SetsCurrentScreenOnShowScreen()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            screen.IsVisible = false;
            shower.ShowScreen(screen);
            Assert.AreEqual(screen , shower.Current);
        }

        [Test]
        public void ScreensStackShower_SetsScreenVisibleOnShowScreen()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            screen.IsVisible = false;
            shower.ShowScreen(screen);
            Assert.IsTrue(screen.IsVisible);
        }

        [Test]
        public void ScreensStackShower_SetsScreenInteractableOnShowScreen()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            screen.IsInteractable = false;
            shower.ShowScreen(screen);
            Assert.IsTrue(screen.IsInteractable);
        }

        [Test]
        public void ScreensStackShower_GoBackToPreviousScreenReturnsTheSameSingleScreen()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            shower.ShowScreen(screen);
            shower.TryGoBackToPreviousScreen(out var current);
            Assert.AreEqual(screen, current);
        }

        [Test]
        public void ScreensStackShower_GoBackToPreviousScreenDoesNotHideSingleScreen()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            shower.ShowScreen(screen);
            shower.TryGoBackToPreviousScreen(out _);
            Assert.IsTrue(screen.IsVisible);
        }

        [Test]
        public void ScreensStackShower_GoBackToPreviousScreenDoesNotSetInteractableToFalseIfSingleScreen()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            shower.ShowScreen(screen);
            shower.TryGoBackToPreviousScreen(out _);
            Assert.IsTrue(screen.IsInteractable);
        }

        [Test]
        public void ScreensStackShower_GoBackToPreviousScreenDoesNotChangeCurrentIfSingleScreen()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            shower.ShowScreen(screen);            
            shower.TryGoBackToPreviousScreen(out var current);
            Assert.AreEqual(screen, shower.Current);
        }

        [Test]
        public void ScreensStackShower_LastScreenIsHiddenOnGoBackToPreviousScreenCalledIfTwoScreensShown()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            var screen2 = new DummyScreen("dummy 2");
            shower.ShowScreen(screen);
            shower.ShowScreen(screen2);
            shower.TryGoBackToPreviousScreen(out _);
            Assert.IsFalse(screen2.IsVisible);
        }

        [Test]
        public void ScreensStackShower_LastScreenIsNotInteractableOnGoBackToPreviousScreenCalledIfTwoScreensShown()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            var screen2 = new DummyScreen("dummy 2");
            shower.ShowScreen(screen);
            shower.ShowScreen(screen2);
            shower.TryGoBackToPreviousScreen(out _);
            Assert.IsFalse(screen2.IsInteractable);
        }

        [Test]
        public void ScreensStackShower_PreviousScreenIsShownOnGoBackToPreviousScreenCalledIfTwoScreensShown()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            var screen2 = new DummyScreen("dummy 2");
            shower.ShowScreen(screen);
            shower.ShowScreen(screen2);
            shower.TryGoBackToPreviousScreen(out _);
            Assert.IsTrue(screen.IsVisible);
        }

        [Test]
        public void ScreensStackShower_PreviousScreenIsInteractableOnGoBackToPreviousScreenCalledIfTwoScreensShown()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            var screen2 = new DummyScreen("dummy 2");
            shower.ShowScreen(screen);
            shower.ShowScreen(screen2);
            shower.TryGoBackToPreviousScreen(out _);
            Assert.IsTrue(screen.IsInteractable);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(15)]
        public void ScreensStackShower_ScreensAppearInReverseSequenceAsTheyWereShownWhenCallingGoBackToPreviousScreen(int count)
        {
            var shower = new StackScreenShower();
            IScreen[] screens= new IScreen[count];
            for (int i = 0; i < count; i++)
            {
                screens[i] = new DummyScreen("dummy " + i);
                shower.ShowScreen(screens[i]);
                for (int j = 0; j < i; j++)
                {
                    Assert.IsFalse(screens[j].IsInteractable);
                    Assert.IsFalse(screens[j].IsVisible);
                }
                Assert.AreEqual(screens[i], shower.Current);
                Assert.IsTrue(screens[i].IsVisible);
                Assert.IsTrue(screens[i].IsInteractable);
            }

            int k = screens.Length - 1;
            while (shower.TryGoBackToPreviousScreen(out var screen))
            {
                Assert.AreEqual(screens[--k], screen);
                Assert.AreEqual(shower.Current, screen);

                Assert.IsTrue(screen?.IsVisible);
                Assert.IsTrue(screen?.IsInteractable);
                for (int l = 0; l < k; l++)
                {
                    Assert.IsFalse(screens[l].IsInteractable);
                    Assert.IsFalse(screens[l].IsVisible);
                }
            }
            Assert.AreEqual(screens[0], shower.Current);
            Assert.IsTrue(screens[0].IsVisible);
            Assert.IsTrue(screens[0].IsInteractable);
        }

        [Test]
        public void ScreensStackShower_WhenShowScreenWhichIsNotVisibleExclusivelyPreviousStaysVisible()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            var screen2 = new DummyScreen("dummy 2", isVisibleExclusively: false);
            shower.ShowScreen(screen);
            Assert.IsTrue(screen.IsVisible);
            shower.ShowScreen(screen2);            
            Assert.IsTrue(screen.IsVisible);
        }

        [Test]
        public void ScreensStackShower_WhenShowScreenWhichIsVisibleExclusivelyPreviousHides()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            var screen2 = new DummyScreen("dummy 2", isVisibleExclusively: true);
            shower.ShowScreen(screen);
            Assert.IsTrue(screen.IsVisible);
            shower.ShowScreen(screen2);
            Assert.IsFalse(screen.IsVisible);
        }        

        [Test]
        public void ScreensStackShower_WhenShowScreenWhichIsNotModalPreviousStaysInteractable()
        {
            var shower = new StackScreenShower();
            var screen = new DummyScreen("dummy");
            var screen2 = new DummyScreen("dummy 2");
            screen2.IsModal = false;
            shower.ShowScreen(screen);
            Assert.IsTrue(screen.IsInteractable);
            shower.ShowScreen(screen2);
            Assert.IsTrue(screen.IsInteractable);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void ScreensStackShower_WhenShowScreenWhichIsModalAllPreviousBecomesNonInteractable(int count)
        {
            var shower = new StackScreenShower();

            IScreen[] screens = new IScreen[count];
            for (int i = 0; i < count; i++)
            {
                screens[i] = new DummyScreen("dummy " + i) { IsModal=false};                
                shower.ShowScreen(screens[i]);
            }            
            var screen = new DummyScreen("dummy modal");
            screen.IsModal = true;
            shower.ShowScreen(screen);
            for (int i = 0; i < count; i++)
            {
                Assert.IsFalse(screens[i].IsInteractable);
            }            
            Assert.IsTrue(screen.IsInteractable);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void ScreensStackShower_WhenShowScreensWhichAreNotModalAllPreviousStayInteractable(int count)
        {
            var shower = new StackScreenShower();

            IScreen[] screens = new IScreen[count];
            for (int i = 0; i < count; i++)
            {
                screens[i] = new DummyScreen("dummy " + i) { IsModal = false };
                shower.ShowScreen(screens[i]);
            }
            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(screens[i].IsInteractable);
            }            
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void ScreensStackShower_WhenShowScreenWhichIsVisibleExclusivelyAllPreviousHide(int count)
        {
            var shower = new StackScreenShower();

            IScreen[] screens = new IScreen[count];
            for (int i = 0; i < count; i++)
            {
                screens[i] = new DummyScreen("dummy not exclusive " + i) { IsVisibleExclusively = false };
                shower.ShowScreen(screens[i]);
            }
            var screen = new DummyScreen("dummy exclusive");
            screen.IsVisibleExclusively = true;
            shower.ShowScreen(screen);
            for (int i = 0; i < count; i++)
            {
                Assert.IsFalse(screens[i].IsVisible);
            }
            Assert.IsTrue(screen.IsVisible);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void ScreensStackShower_WhenShowScreenWhichIsNotVisibleExclusivelyAllPreviousStayVisible(int count)
        {
            var shower = new StackScreenShower();

            IScreen[] screens = new IScreen[count];
            for (int i = 0; i < count; i++)
            {
                screens[i] = new DummyScreen("dummy not exclusive" + i) { IsVisibleExclusively = false };
                shower.ShowScreen(screens[i]);
            }
            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(screens[i].IsVisible);
            }
        }

        public void ScreensStackShower_AttemptToShowAlreadyShownScreenThrowsException()
        {
            var shower = new StackScreenShower();
            int count = 5;
            IScreen[] screens = new IScreen[count];
            for (int i = 0; i < count; i++)
            {
                screens[i] = new DummyScreen("dummy " + i);
                shower.ShowScreen(screens[i]);
            }
            var screen = screens[2];
            Assert.Throws<InvalidOperationException>(() => { shower.ShowScreen(screen); });            
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void ScreensStackShower_WhenGoBackToPreviousScreenCalledAfterLastScreenWhichIsVisibleExclusivelyAllPreviousNonExclusiveScreensMustBeVisible(int count)
        {
            var shower = new StackScreenShower();

            IScreen[] screens = new IScreen[count];
            for (int i = 0; i < count; i++)
            {
                screens[i] = new DummyScreen("dummy not exclusive " + i) { IsVisibleExclusively = false };
                shower.ShowScreen(screens[i]);
            }
            var screen = new DummyScreen("dummy exclusive");
            screen.IsVisibleExclusively = true;
            shower.ShowScreen(screen);
            shower.TryGoBackToPreviousScreen(out _);
            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(screens[i].IsVisible);
            }
            Assert.IsFalse(screen.IsVisible);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void ScreensStackShower_WhenGoBackToPreviousScreenCalledAfterLastScreenWhichIsModalAllPreviousNonModalScreensMustBeInteractive(int count)
        {
            var shower = new StackScreenShower();

            IScreen[] screens = new IScreen[count];
            for (int i = 0; i < count; i++)
            {
                screens[i] = new DummyScreen("dummy not modal " + i) { IsModal = false };
                shower.ShowScreen(screens[i]);
            }
            var screen = new DummyScreen("dummy modal");
            screen.IsModal = true;
            shower.ShowScreen(screen);
            shower.TryGoBackToPreviousScreen(out _);
            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(screens[i].IsInteractable);
            }
            Assert.IsFalse(screen.IsInteractable);
        }
    }
}
