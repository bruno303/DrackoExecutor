using System.Threading;
using DrackoExecutor.Core.Impl;
using NUnit.Framework;

namespace DrackoExecutor.Core.Test
{
    public class SingleThreadExecutorTest
    {
        private SingleThreadExecutor _executor;
        
        [SetUp]
        public void Setup()
        {
            _executor = new SingleThreadExecutor();
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
            _executor.Execute(() => a = 1);
            _executor.Execute(() => a = 0);
            _executor.Execute(() => a = 5);
            Sleep();
            Assert.AreEqual(5, a);
        }
        
        [Test]
        public void TestExecuteCalledMultipleTimesChangingVariableValueAndCanceling()
        {
            var a = 0;
            _executor.Execute(() => a = 1);
            _executor.Execute(() => a = 2);
            _executor.Execute(() => a = 3);
            _executor.Execute(() => a = 1);
            _executor.Execute(() => a = 4);
            Sleep();
            _executor.Cancel();
            Sleep();
            _executor.Execute(() => a = 5);
            Sleep();
            Assert.AreEqual(4, a);
        }

        private static void Sleep()
        {
            Thread.Sleep(500);
        }
    }
}