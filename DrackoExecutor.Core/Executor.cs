using System;

namespace DrackoExecutor.Core
{
    public interface Executor
    {
        void Execute(Action action);
    }
}