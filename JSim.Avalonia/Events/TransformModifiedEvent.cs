using JSim.Core.Maths;

namespace JSim.Avalonia.Events
{
    public delegate void TransformModifiedEventHandler(object sender, TransformModifiedEventArgs e);

    public class TransformModifiedEventArgs : EventArgs
    {
        public TransformModifiedEventArgs(Transform3D transform)
        {
            Transform = transform;
        }

        public Transform3D Transform { get; }
    }
}
