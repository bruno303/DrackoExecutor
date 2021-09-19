using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace DrackoExecutor.Core.Impl
{
    public class FixedSizeThreadExecutor : Executor
    {
        private readonly ConcurrentQueue<Action> _queue;
        private volatile bool _cancel;
        private readonly object _cancelLock = new();
        private readonly List<Thread> _threads;
        private readonly int _numberOfThreads;

        public FixedSizeThreadExecutor(int numberOfThreads)
        {
            _numberOfThreads = numberOfThreads;
            _cancel = false;
            _queue = new ConcurrentQueue<Action>();
            _threads = new List<Thread>();
            InitializeThreads();
        }

        private void InitializeThreads()
        {
            for (var i = 0; i < _numberOfThreads; i++)
            {
                _threads.Add(new Thread(Run)
                {
                    IsBackground = true,
                    Name = $"executor-thread-{Guid.NewGuid().ToString()}"
                });
            }
            
            _threads.ForEach(t => t.Start());
        }

        private void Run()
        {
            while (!_cancel)
            {
                var dequeued = _queue.TryDequeue(out var actionToExecute);
                if (dequeued)
                {
                    actionToExecute();
                }
            }
        }
        
        public void Execute(Action action)
        {
            _queue.Enqueue(action);
        }
        
        public void Cancel()
        {
            //lock (_cancelLock)
            //{
                _cancel = true;
            //}
        }

        private bool IsCanceled()
        {
            lock (_cancelLock)
            {
                return _cancel;
            }
        }
    }
}