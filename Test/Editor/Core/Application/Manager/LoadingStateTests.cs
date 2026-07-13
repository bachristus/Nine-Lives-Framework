using NineLives.Framework.Core.Application.Tests;
using NUnit.Framework;
using System;

namespace NineLives.Framework.Core.Application.Manager.Tests
{
    public class LoadingStateTests
    {
        [Test]
        public void LoadingState_GameStateIdIsProper()
        {
            var context = new DummyGameContext();
            var loadingState = new LoadingState(context);
            Assert.That(loadingState.AppState, Is.EqualTo(AppState.Loading));
        }

        [Test]
        public void LoadingState_IfNoGameLoadingRequestInContextThrowExceptionOnEnter()
        {
            var provider = new DummySimulationProvider();
            var context = new DummyGameContext(new DummyTime(), provider);
            var loadingState = new LoadingState(context);
            Assert.IsNull(context.GameLoadingRequest);
            Assert.Throws<InvalidOperationException>(() =>
            {
                loadingState.Enter();
            });            
        }

        [TestCase(null)]
        [TestCase("some game name")]
        public void LoadingState_CallsLoadingWithCorrectGameNameArgumentWhenEntered(string? gameName)
        {
            var provider = new DummySimulationProvider();
            var context = new DummyGameContext(new DummyTime(), provider);
            context.PlaceGameLoadingRequest(gameName);
            var loadingState = new LoadingState(context);
            Assert.IsFalse(provider.IsSimulationReady(gameName));
            loadingState.Enter();
            Assert.IsTrue(provider.IsSimulationReady(gameName));
        }
        
        [TestCase(null)]
        [TestCase("some saved game")]
        public void LoadingState_StartsLoadingOperation(string? gameName)
        {
            var loadingOperation = new DummyGameLoadingOperation();
            var provider = new DummySimulationProvider(loadingOperation);
            var context = new DummyGameContext(new DummyTime(), provider);
            var loadingState = new LoadingState(context);
            
            Assert.IsFalse(loadingOperation.IsStarted);
            context.PlaceGameLoadingRequest(gameName);
            loadingState.Enter();
            Assert.IsTrue(loadingOperation.IsStarted);
        }

        [TestCase(null)]
        [TestCase("some saved game")]
        public void LoadingState_ChangesStateToPlayingIfLoadingOperationCompletesSuccessfully(string? gameName)
        {
            var loadingOperation = new DummyGameLoadingOperation();
            var provider = new DummySimulationProvider(loadingOperation);
            var context = new DummyGameContext(new DummyTime(), provider);
            var loadingState = new LoadingState(context);
            
            context.PlaceGameLoadingRequest(gameName);
            loadingState.Enter();
            loadingOperation.InvokeCompletedSuccessfully();
            Assert.AreEqual(context.Playing,context.CurrentState);
        }

        [TestCase(null)]
        [TestCase("some saved game")]
        public void LoadingState_ChangesStateToMenuIfLoadingOperationFailes(string? gameName)
        {
            var loadingOperation = new DummyGameLoadingOperation();
            var provider = new DummySimulationProvider(loadingOperation);
            var context = new DummyGameContext(new DummyTime(), provider);
            var loadingState = new LoadingState(context);

            context.PlaceGameLoadingRequest(gameName);
            loadingState.Enter();
            loadingOperation.InvokeFailed();
            Assert.AreEqual(context.Menu, context.CurrentState);
        }

        [TestCase(null)]
        [TestCase("some saved game")]
        public void LoadingState_ChangesStateToMenuIfLoadingOperationIsCancelled(string? gameName)
        {
            var loadingOperation = new DummyGameLoadingOperation();
            var provider = new DummySimulationProvider(loadingOperation);
            var context = new DummyGameContext(new DummyTime(), provider);
            var loadingState = new LoadingState(context);

            context.PlaceGameLoadingRequest(gameName);
            loadingState.Enter();
            loadingOperation.InvokeCancelled();
            Assert.AreEqual(context.Menu, context.CurrentState);
        }       
        
        [TestCase(null)]
        [TestCase("some saved game")]
        public void LoadingState_InvokesLoadingStartedEventWhenEntered(string? gameName)
        {
            var loadingOperation = new DummyGameLoadingOperation();
            var provider = new DummySimulationProvider(loadingOperation);
            var context = new DummyGameContext(new DummyTime(), provider);
            var loadingState = new LoadingState(context);

            context.PlaceGameLoadingRequest(gameName);
            int i = 0;
            loadingState.LoadingStarted += (op) => { i++; };
            loadingState.Enter();

            Assert.AreEqual(1, i);
        }
    }
}
