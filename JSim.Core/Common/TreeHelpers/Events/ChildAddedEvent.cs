namespace JSim.Core.Common
{
    public delegate void ChildAddedEventHandler<T>(object sender, ChildAddedEventArgs<T> e);
    public class ChildAddedEventArgs<T> : EventArgs
    {
        public ChildAddedEventArgs(T child)
        {
            Child = child;
        }

        public T Child { get; }
    }
}
