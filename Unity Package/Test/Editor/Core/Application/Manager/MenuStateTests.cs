using NineLives.Framework.Core.Application.Tests;
using NUnit.Framework;

namespace NineLives.Framework.Core.Application.Manager.Tests
{
    public class MenuStateTests
    {
        [Test]
        public void MenuState_EnterUnloadsGameIfUnloadGameOnEnterIsSetToTrue()
        {
            var provider = new DummySimulationProvider();
            var context = new DummyGameContext(new DummyTime(), provider);
            var menuState = new MenuState(context, unloadGameOnEnter:true);
            context.SimulationProvider.Load("some game");

            Assert.IsTrue(provider.IsLoaded);

            menuState.Enter();
            
            Assert.IsFalse(provider.IsLoaded);
        }

        [Test]
        public void MenuState_EnterDoesNotUnloadGameIfUnloadGameOnEnterIsSetToFalse()
        {
            var provider = new DummySimulationProvider();
            var context = new DummyGameContext(new DummyTime(), provider);
            var menuState = new MenuState(context, unloadGameOnEnter:false);
            context.SimulationProvider.Load("some game");

            Assert.IsTrue(provider.IsLoaded);

            menuState.Enter();

            Assert.IsTrue(provider.IsLoaded);
        }

        [Test]
        public void MenuState_GameStateIdIsProper()
        {
            var context = new DummyGameContext();
            var menuState = new MenuState(context);
            Assert.AreEqual(AppState.Menu, menuState.AppState);
        }

        //[Test]
        [TestCase(null)]
        [TestCase("some saved game")]
        public void MenuState_ChangesCurrentStateToLoadingIfPlayingGameIsRequestedAndSimulationIsNotReady(string? gameName)
        {
            //var simulation = new DummySimulation();
            var provider = new DummySimulationProvider(/*simulation*/);
            var context = new DummyGameContext(new DummyTime(), provider);
            var menuState = new MenuState(context);
            menuState.Enter();

            Assert.IsNull(context.CurrentState);
            Assert.IsFalse(provider.IsSimulationReady(gameName));

            context.RequestPlayingGame(gameName);
            Assert.AreEqual(context.Loading, context.CurrentState);
            Assert.IsNotNull(context.GameLoadingRequest);
            Assert.AreEqual(gameName, context.GameLoadingRequest!.RequestedGameName);
        }

        //[Test]
        [TestCase(null)]
        [TestCase("some saved game")]
        public void MenuState_ChangesCurrentStateToPlayingIfPlayingGameIsRequestedAndSimulationIsReady(string? gameName)
        {
            //var simulation = new DummySimulation();
            var provider = new DummySimulationProvider(/*simulation*/);
            var context = new DummyGameContext(new DummyTime(), provider);            
            var menuState = new MenuState(context);
            menuState.Enter();

            Assert.IsNull(context.CurrentState);

            provider.Load(gameName);
            Assert.IsTrue(provider.IsSimulationReady(gameName));

            context.RequestPlayingGame(gameName);

            Assert.AreEqual(context.Playing, context.CurrentState);
            Assert.IsNull(context.GameLoadingRequest);
        }

        [TestCase(null)]
        [TestCase("some saved game")]
        public void MenuState_DoesNotChangeStateIfPlayingIsRequestedButMenuStateWasNotEntered(string? gameName)
        {
            var context = new DummyGameContext();
            _ = new MenuState(context);        

            Assert.IsNull(context.CurrentState);

            context.RequestPlayingGame(gameName);
            Assert.IsNull(context.CurrentState);
        }

        [TestCase(null)]
        [TestCase("some saved game")]
        public void MenuState_DoesNotChangeStateIfPlayingIsRequestedButMenuStateWasExited(string? gameName)
        {
            var context = new DummyGameContext();
            var menuState = new MenuState(context);
            menuState.Enter();

            Assert.IsNull(context.CurrentState);

            menuState.Exit();

            context.RequestPlayingGame(gameName);
            Assert.IsNull(context.CurrentState);
        }
    }
}
