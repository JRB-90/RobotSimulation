using System.Collections;

namespace JSim.Core.Common
{
    /// <summary>
    /// Standard implementation of a child container.
    /// </summary>
    public class ChildContainer<T> : IChildContainer<T>
    {
        public IReadOnlyCollection<T> Children => children;

        public bool AttachChild(T child)
        {
            throw new NotImplementedException();
        }

        public bool DetachChild(T child)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private List<T> children;
    }
}
