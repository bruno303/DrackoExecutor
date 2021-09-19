using System;

namespace DrackoExecutor.Core.Impl
{
    public class SimpleExecutor : Executor
    {
        public void Execute(Action action)
        {
            action();
        }
    }
}