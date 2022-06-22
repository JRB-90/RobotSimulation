using JSim.Core.Maths;

namespace JSim.Av.Controls
{
    public delegate void TransformUpdatedEventHandler(object sender, TransformUpdatedEventArgs e);
    public class TransformUpdatedEventArgs : EventArgs
    {
        public TransformUpdatedEventArgs(Transform3D transform)
        {
            Transform = transform;
        }

        public Transform3D Transform { get; }
    }
}
