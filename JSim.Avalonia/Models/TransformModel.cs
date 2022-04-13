using JSim.Core.Maths;

namespace JSim.Avalonia.Models
{
    public class TransformModel
    {
        public TransformModel(Transform3D transform)
        {
            this.transform = transform;
        }

        public Transform3D Transform
        {
            get => transform;
            set
            {
                transform = value;
                TransformModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public double X
        {
            get => Transform.Translation.X;
            set
            {
                Transform.Translation =
                    new Vector3D(
                        value,
                        transform.Translation.Y,
                        transform.Translation.Z
                    );
                TransformModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public double Y
        {
            get => Transform.Translation.Y;
            set
            {
                Transform.Translation =
                    new Vector3D(
                        transform.Translation.X,
                        value,
                        transform.Translation.Z
                    );
                TransformModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public double Z
        {
            get => Transform.Translation.Z;
            set
            {
                Transform.Translation =
                    new Vector3D(
                        transform.Translation.X,
                        transform.Translation.Y,
                        value
                    );
                TransformModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public double Rx
        {
            get => Transform.Rotation.AsFixed().Rx;
            set
            {
                Transform.Rotation =
                    new FixedRotation3D(
                        value,
                        transform.Rotation.AsFixed().Ry,
                        transform.Rotation.AsFixed().Rz
                    );
                TransformModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public double Ry
        {
            get => Transform.Rotation.AsFixed().Ry;
            set
            {
                Transform.Rotation =
                    new FixedRotation3D(
                        transform.Rotation.AsFixed().Rx,
                        value,
                        transform.Rotation.AsFixed().Rz
                    );
                TransformModified?.Invoke(this, EventArgs.Empty);
            }
        }
        public double Rz
        {
            get => Transform.Rotation.AsFixed().Rz;
            set
            {
                Transform.Rotation =
                    new FixedRotation3D(
                        transform.Rotation.AsFixed().Rx,
                        transform.Rotation.AsFixed().Ry,
                        value
                    );
                TransformModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler? TransformModified;

        private Transform3D transform;
    }
}
