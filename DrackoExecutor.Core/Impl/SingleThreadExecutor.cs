namespace DrackoExecutor.Core.Impl
{
    public class SingleThreadExecutor : FixedSizeThreadExecutor, Executor
    {
        public SingleThreadExecutor() : base(1) { }
    }
}