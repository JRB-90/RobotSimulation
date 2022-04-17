using JSim.Avalonia.Events;
using JSim.Core.Maths;

namespace JSim.Avalonia.Models
{
    public class TransformModel
    {
        public TransformModel()
        {
            transform = Transform3D.Identity;
        }

        public TransformModel(Transform3D transform)
        {
            this.transform = new Transform3D(transform);
        }

        public Transform3D Transform
        {
            get => transform;
            set
            {
                transform = value;
                TransformModified?.Invoke(this, new TransformModifiedEventArgs(Transform));
            }
        }

        public double X
        {
            get => Transform.Translation.X;
            set
            {
                UpdateTransform(
                    new Vector3D(
                        value,
                        transform.Translation.Y,
                        transform.Translation.Z
                    )
                );
            }
        }

        public double Y
        {
            get => Transform.Translation.Y;
            set
            {
                UpdateTransform(
                    new Vector3D(
                        transform.Translation.X,
                        value,
                        transform.Translation.Z
                    )
                );
            }
        }

        public double Z
        {
            get => Transform.Translation.Z;
            set
            {
                UpdateTransform(
                    new Vector3D(
                        transform.Translation.X,
                        transform.Translation.Y,
                        value
                    )
                );
            }
        }

        public double Rx
        {
            get => Transform.Rotation.AsFixed().Rx;
            set
            {
                UpdateTransform(
                    new FixedRotation3D(
                        value,
                        transform.Rotation.AsFixed().Ry,
                        transform.Rotation.AsFixed().Rz
                    )
                );
            }
        }

        public double Ry
        {
            get => Transform.Rotation.AsFixed().Ry;
            set
            {
                UpdateTransform(
                    new FixedRotation3D(
                        transform.Rotation.AsFixed().Rx,
                        value,
                        transform.Rotation.AsFixed().Rz
                    )
                );
            }
        }
        public double Rz
        {
            get => Transform.Rotation.AsFixed().Rz;
            set
            {
                UpdateTransform(
                    new FixedRotation3D(
                        transform.Rotation.AsFixed().Rx,
                        transform.Rotation.AsFixed().Ry,
                        value
                    )
                );
            }
        }

        public event TransformModifiedEventHandler? TransformModified;

        public override string ToString()
        {
            return transform.ToString();
        }

        private void UpdateTransform(Vector3D translation)
        {
            Transform =
                new Transform3D(
                    translation,
                    transform.Rotation
                );
            TransformModified?.Invoke(this, new TransformModifiedEventArgs(Transform));
        }

        private void UpdateTransform(Rotation3D rotation)
        {
            Transform =
                new Transform3D(
                    transform.Translation,
                    rotation
                );
            TransformModified?.Invoke(this, new TransformModifiedEventArgs(Transform));
        }

        private Transform3D transform;
    }
}
