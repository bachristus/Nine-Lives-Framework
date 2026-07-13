using NUnit.Framework;

namespace NineLives.Framework.Core.Application.Manager.Tests
{
    public class PauseStateTests
    {
        [Test]
        public void PauseState_EnterPausesGame()
        {
            var context = new DummyGameContext();
            var pauseState = new PauseState(context);

            context.SimulationTime.Resume();
            Assert.IsFalse(context.SimulationTime.IsPaused);

            pauseState.Enter();
            Assert.IsTrue(context.SimulationTime.IsPaused);
        }

        [Test]
        public void PauseState_GameStateIdIsProper()
        {
            var context = new DummyGameContext();
            var pauseState = new PauseState(context);
            Assert.AreEqual(AppState.Pause, pauseState.AppState);
        }

        [Test]
        public void PauseState_ChangesCurrentStateToPlayingIfPlayingIsRequested()
        {
            var context = new DummyGameContext();
            var pauseState = new PauseState(context);
            pauseState.Enter();

            Assert.IsNull(context.CurrentState);

            context.RequestResumePlaying();
            Assert.AreEqual(context.Playing, context.CurrentState);
        }

        [Test]
        public void PauseState_DoesNotChangeStateIfPlayingIsRequestedButPauseStateWasNotEntered()
        {
            var context = new DummyGameContext();
            var pauseState = new PauseState(context);     

            Assert.IsNull(context.CurrentState);

            context.RequestResumePlaying();
            Assert.IsNull(context.CurrentState);
        }

        [Test]
        public void PauseState_DoesNotChangeStateIfPlayingIsRequestedButPauseStateWasExited()
        {
            var context = new DummyGameContext();
            var pauseState = new PauseState(context);
            pauseState.Enter();

            Assert.IsNull(context.CurrentState);

            pauseState.Exit();

            context.RequestResumePlaying(); 
            Assert.IsNull(context.CurrentState);
        }
    }
}
