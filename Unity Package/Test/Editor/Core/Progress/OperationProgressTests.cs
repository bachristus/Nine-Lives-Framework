using NUnit.Framework;

namespace NineLives.Framework.Core.Progress.Tests
{
    public class OperationProgressTests
    {
        
        [Test]
        public void OperationProgress_ReportCallsEvent()
        {
            var op= new OperationProgress();
            op.Changed += (data) =>
            {
                Assert.That(data.progress01, Is.EqualTo(0.5f));
                Assert.That(data.currentOperation, Is.EqualTo("Loading"));
            };
            op.Report(0.5f, "Loading");
        }

        [Test]
        public void OperationProgress_ReportWithDataCallsEvent()
        {
            var op = new OperationProgress();
            op.Changed += (data) =>
            {
                Assert.That(data.progress01, Is.EqualTo(0.5f));
                Assert.That(data.currentOperation, Is.EqualTo("Loading"));
            };
            op.Report(new(0.5f, "Loading"));
        }

        [Test]
        public void OperationProgress_ReportChangesData()
        {
            var op = new OperationProgress();   
            var data = new OperationProgressData(0.5f, "Loading");
            op.Report(data);
            Assert.AreEqual(op.Data, data);
        }
    }
}
