using DrackoExecutor.Core.Impl;
using NUnit.Framework;

namespace DrackoExecutor.Core.Test
{
    public class SimpleExecutorTest
    {
        private Executor _executor;
        
        [SetUp]
        public void Setup()
        {
            _executor = new SimpleExecutor();
        }

        [Test]
        public void TestExecuteCalledOnceChangingVariableValue()
        {
            var a = 0;
            _executor.Execute(() => a = 1);
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
            Assert.AreEqual(5, a);
        }
    }
}