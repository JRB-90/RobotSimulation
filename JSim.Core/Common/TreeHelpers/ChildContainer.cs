using System.Collections;

namespace JSim.Core.Common
{
    /// <summary>
    /// Standard implementation of a child container.
    /// </summary>
    public class ChildContainer<T> : IChildContainer<T>
    {
        public ChildContainer()
        {
            children = new List<T>();
        }

        /// <summary>
        /// Gets a collection of all the children attached to the container.
        /// </summary>
        public IReadOnlyCollection<T> Children => children;

        /// <summary>
        /// Event fired then a child has been added ot the collection.
        /// </summary>
        public event ChildAddedEventHandler<T>? ChildAdded;

        /// <summary>
        /// Event fired when a child has been removed from a collection.
        /// </summary>
        public event ChildRemovedEventHandler<T>? ChildRemoved;

        /// <summary>
        /// Event fired when the number of children in a container has been modified.
        /// </summary>
        public event ChildContainerModifiedEventHandler? ChildContainerModified;

        /// <summary>
        /// Clears all currently attached children.
        /// </summary>
        public void ClearAllChildren()
        {
            children.Clear();
            ChildContainerModified?.Invoke(this, new ChildContainerModifiedEventArgs());
        }

        /// <summary>
        /// Attaches a child to the collection.
        /// </summary>
        /// <param name="child">Child object to attach.</param>
        /// <returns>True if attach was successful. False if child is already attached.</returns>
        public bool AttachChild(T child)
        {
            if (children.Contains(child))
            {
                return false;
            }
            else
            {
                children.Add(child);
                ChildAdded?.Invoke(this, new ChildAddedEventArgs<T>(child));
                ChildContainerModified?.Invoke(this, new ChildContainerModifiedEventArgs());

                return true;
            }
        }

        /// <summary>
        /// Detaches a child from the collection.
        /// </summary>
        /// <param name="child">Child to detach.</param>
        /// <returns>True if detach was successful. False is child is not attached to the collection.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DetachChild(T child)
        {
            if (!children.Contains(child))
            {
                return false;
            }
            else
            {
                children.Remove(child);
                ChildRemoved?.Invoke(this, new ChildRemovedEventArgs<T>(child));
                ChildContainerModified?.Invoke(this, new ChildContainerModifiedEventArgs());

                return true;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private List<T> children;
    }
}
