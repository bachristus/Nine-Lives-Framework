using NineLives.Framework.Core.Application;
using NineLives.Framework.Core.Application.Tests;
using NineLives.Framework.Core.Tests;
using NUnit.Framework;
using System;
using System.Linq;

namespace NineLives.Framework.Core.UI.Tests
{
    public class UIManagerTests
    {          
        [TestCase(AppState.Menu,true)]
        [TestCase(AppState.Playing,true)]
        [TestCase(AppState.Loading, false)]
        [TestCase(AppState.Pause, false)]
        public void UIManager_ShowsOnlyScreenThatCorrespondsToAppState(AppState appState, bool hideAllShoodBeCalled)
        {
            var appStateHolder = new DummyAppStateHolder();
            var screenShower = new DummyScreenShower();

            var noneScreen = new DummyScreen("other screen") { AppState = AppState.None };
            var menuScreen = new DummyScreen("menu screen") { AppState = AppState.Menu };
            var loadingScreen = new DummyScreen("loading screen") { AppState = AppState.Loading };            
            var pauseScreen = new DummyScreen("pause screen") { AppState = AppState.Pause };
            var playingScreen = new DummyScreen("playing screen") { AppState = AppState.Playing };

            DummyScreen[] screens = new[] { noneScreen, loadingScreen, menuScreen, pauseScreen, playingScreen };

            var properScreen=screens.Where(s=>s.AppState==appState).FirstOrDefault();
            Assert.IsNotNull(properScreen);

            var manager = new UIManager(appStateHolder, new DummyInput(), screenShower, screens, new DummyDialogProvider());

            int showCalledCount = 0;
            int hideCalledCount = 0;
            screenShower.HideAllScreensCalled += () => { hideCalledCount++; };
            screenShower.ShowScreenCalled += (screen) =>
            {
                Assert.AreEqual(hideAllShoodBeCalled ? 1 : 0, hideCalledCount);
                Assert.AreEqual(properScreen, screen);
                showCalledCount++;
            };
            appStateHolder.InvokeAppStateChanged(appState);
            Assert.AreEqual(1, showCalledCount);
            Assert.AreEqual(hideAllShoodBeCalled ? 1 : 0, hideCalledCount);
        }

        [TestCase(AppState.None)]
        [TestCase(AppState.None-1)]
        public void UIManager_UnknownAppStateThrowsException(AppState appState)
        {
            var appStateHolder = new DummyAppStateHolder();
            var screenShower = new DummyScreenShower();

            var appStateScreen = new DummyScreen("other screen") { AppState = appState };

            var menuScreen = new DummyScreen("menu screen") { AppState = AppState.Menu };
            var loadingScreen = new DummyScreen("loading screen") { AppState = AppState.Loading };
            var pauseScreen = new DummyScreen("pause screen") { AppState = AppState.Pause };
            var playingScreen = new DummyScreen("playing screen") { AppState = AppState.Playing };

            DummyScreen[] screens = new[] { appStateScreen, loadingScreen, menuScreen, pauseScreen, playingScreen };

            var manager = new UIManager(appStateHolder, new DummyInput(), screenShower, screens, new DummyDialogProvider());

            int showCalledCount = 0;
            int hideCalledCount = 0;
            screenShower.HideAllScreensCalled += () => { hideCalledCount++; };
            screenShower.ShowScreenCalled += (screen) => { showCalledCount++; };

            Assert.Throws<ArgumentException>(()=>appStateHolder.InvokeAppStateChanged(appState));

            Assert.AreEqual(0, showCalledCount);
            Assert.AreEqual(0, hideCalledCount);
        }

        [Test]
        public void UIManager_EscapePressedCallsCurrentScreen()
        {
            var appStateHolder = new DummyAppStateHolder();
            var screenShower = new DummyScreenShower();

            var settingsScreen = new DummyScreen("settings screen") { AppState = AppState.None };
            var subSettingsScreen = new DummyScreen(" sub settings screen") { AppState = AppState.None };

            var menuScreen = new DummyScreen("menu screen") { AppState = AppState.Menu };
            var loadingScreen = new DummyScreen("loading screen") { AppState = AppState.Loading };
            var pauseScreen = new DummyScreen("pause screen") { AppState = AppState.Pause };
            var playingScreen = new DummyScreen("playing screen") { AppState = AppState.Playing };

            DummyScreen[] screens = new[] { loadingScreen, menuScreen, pauseScreen, playingScreen , settingsScreen, subSettingsScreen};
            var input = new DummyInput();
            var manager = new UIManager(appStateHolder, input, screenShower, screens, new DummyDialogProvider());

            int callsCount = 0;
            subSettingsScreen.ProcessCancelPressed += () => { callsCount++; };

            appStateHolder.InvokeAppStateChanged(AppState.Menu);
            manager.RequestScreenToBeShown(settingsScreen.Id);
            manager.RequestScreenToBeShown(subSettingsScreen.Id);
            Assert.AreEqual(0, callsCount);
            input.InvokeEscapePressed();
            Assert.AreEqual(1, callsCount);
            input.InvokeEscapePressed();
            Assert.AreEqual(2, callsCount);
        }
    }
}
