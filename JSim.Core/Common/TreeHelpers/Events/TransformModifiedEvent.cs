using JSim.Core.Maths;

namespace JSim.Core.Common
{
    public delegate void TransformModifiedEventHandler(object sender, TransformModifiedEventArgs e);
    public class TransformModifiedEventArgs
    {
        public TransformModifiedEventArgs(Transform3D modifiedTransform)
        {
            ModifiedTransform = modifiedTransform;
        }

        public Transform3D ModifiedTransform { get; }
    }
}
