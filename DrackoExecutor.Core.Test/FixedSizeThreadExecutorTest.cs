using System.Threading;
using DrackoExecutor.Core.Impl;
using NUnit.Framework;

namespace DrackoExecutor.Core.Test
{
    public class FixedSizeThreadExecutorTest
    {
        private FixedSizeThreadExecutor _executor;
        
        [SetUp]
        public void Setup()
        {
            _executor = new FixedSizeThreadExecutor(5);
        }

        [Test]
        public void TestExecuteCalledOnceChangingVariableValue()
        {
            var a = 0;
            _executor.Execute(() => a = 1);
            Sleep();
            Assert.AreEqual(1, a);
        }

        [Test]
        public void TestExecuteCalledMultipleTimesChangingVariableValue()
        {
            var a = 0;
            _executor.Execute(() => a = 1);
            _executor.Execute(() => a = 2);
            _executor.Execute(() => a = 3);
            _executor.Execute(() => a = 4);
            _executor.Execute(() => a = 5);
            Sleep();
            Assert.GreaterOrEqual(a, 1);
            Assert.LessOrEqual(a, 5);
        }
        
        [Test]
        public void TestExecuteCalledMultipleTimesChangingVariableValueAndCanceling()
        {
            var a = 0;
            _executor.Execute(() => a = 1);
            _executor.Execute(() => a = 2);
            _executor.Execute(() => a = 3);
            _executor.Execute(() => a = 4);
            Sleep();
            _executor.Cancel();
            Sleep();
            _executor.Execute(() => a = 5);
            Sleep();
            Assert.GreaterOrEqual(a, 1);
            Assert.LessOrEqual(a, 4);
        }
        
        [Test]
        public void TestExecuteCalledMultipleTimesChangingSomeVariables()
        {
            int a = 0, b = 0, c = 0, d = 0, e = 0;
            _executor.Execute(() => a = 1);
            _executor.Execute(() => b = 2);
            _executor.Execute(() => c = 3);
            _executor.Execute(() => d = 4);
            _executor.Execute(() => e = 5);
            Sleep();
            Assert.AreEqual(1, a);
            Assert.AreEqual(2, b);
            Assert.AreEqual(3, c);
            Assert.AreEqual(4, d);
            Assert.AreEqual(5, e);
        }
        
        private void Sleep()
        {
            Thread.Sleep(500);
        }
    }
}