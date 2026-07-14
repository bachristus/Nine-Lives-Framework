using NineLives.Framework.Core.Application.Tests;
using NUnit.Framework;

namespace NineLives.Framework.Core.Application.Manager.Tests
{
    public class AppManagerTests
    {        
        [Test]
        public void AppManager_StartsWithMenuState()
        {
            //Arrange
            var simulation = new DummySimulationProvider();
            var manager = new AppManager(simulation, new DummyTime());
            bool eventRaised = false;
            AppState receivedState = default;
            manager.AppStateChanged += state =>
            {
                eventRaised = true;
                receivedState = state;
            };
            //Act
            manager.Start();
            //Assert
            Assert.That(eventRaised, Is.True);
            Assert.That(receivedState, Is.EqualTo(AppState.Menu));
        }
    }
}
