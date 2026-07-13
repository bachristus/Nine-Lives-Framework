using NUnit.Framework;

namespace NineLives.Framework.Core.Application.Manager.Tests
{
    public class PlayingStateTests
    {
        [Test]
        public void PlayingState_EnterResumesGame()
        {
            var context = new DummyGameContext();
            var playingState = new PlayingState(context);

            context.SimulationTime.Pause();
            Assert.IsTrue(context.SimulationTime.IsPaused);

            playingState.Enter();
            Assert.IsFalse(context.SimulationTime.IsPaused);
        }

        [Test]
        public void PlayingState_GameStateIdIsProper()
        {
            var context = new DummyGameContext();
            var playingState = new PlayingState(context);
            Assert.AreEqual(AppState.Playing, playingState.AppState);
        }

        [Test]
        public void PlayingState_IfPauseIsRequestedChangesCurrentStateToPause()
        {
            var context = new DummyGameContext();
            var playingState = new PlayingState(context); 
            context.Pause = new DummyState();
            playingState.Enter();

            Assert.IsNull(context.CurrentState);

            context.RequestedPause();
            Assert.AreEqual(context.Pause, context.CurrentState);
        }

        [Test]
        public void PlayingState_DoesNotChangeStateOfContextIfExited()
        {
            var context = new DummyGameContext();
            var playingState = new PlayingState(context);
            context.Pause = new DummyState();
            playingState.Enter();

            Assert.IsNull(context.CurrentState);
            playingState.Exit();
            context.RequestedPause();
            Assert.IsNull(context.CurrentState);
        }

        [Test]
        public void PlayingState_DoesNotChangeStateOfContextIfWasNotEntered()
        {
            var context = new DummyGameContext();
            _ = new PlayingState(context);

            Assert.IsNull(context.CurrentState);

            context.RequestedPause();
            Assert.IsNull(context.CurrentState);
        }
    }
}
