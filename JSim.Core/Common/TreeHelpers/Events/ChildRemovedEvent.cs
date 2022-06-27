namespace JSim.Core.Common
{
    public delegate void ChildRemovedEventHandler<T>(object sender, ChildRemovedEventArgs<T> e);
    public class ChildRemovedEventArgs<T> : EventArgs
    {
        public ChildRemovedEventArgs(T child)
        {
            Child = child;
        }

        public T Child { get; }
    }
}
