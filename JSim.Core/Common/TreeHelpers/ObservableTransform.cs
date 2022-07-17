using JSim.Core.Maths;

namespace JSim.Core.Common
{
    /// <summary>
    /// Manages a transform so that all changes to it are relayed to observers.
    /// </summary>
    public class ObservableTransform
    {
        public ObservableTransform()
        {
            transform = Transform3D.Identity;
        }

        public ObservableTransform(Transform3D transform)
        {
            this.transform = transform;
        }

        public double X
        {
            get
            {
                lock (transformLock)
                {
                    return transform.Translation.X;
                }
            }
            set
            {
                lock (transformLock)
                {
                    transform.Translation =
                        new Vector3D(
                            value,
                            transform.Translation.Y,
                            transform.Translation.Z
                        );

                    RaiseTransformModified();
                }
            }
        }

        public double Y
        {
            get
            {
                lock (transformLock)
                {
                    return transform.Translation.Y;
                }
            }
            set
            {
                lock (transformLock)
                {
                    transform.Translation =
                        new Vector3D(
                            transform.Translation.X,
                            value,
                            transform.Translation.Z
                        );

                    RaiseTransformModified();
                }
            }
        }

        public double Z
        {
            get
            {
                lock (transformLock)
                {
                    return transform.Translation.Z;
                }
            }
            set
            {
                lock (transformLock)
                {
                    transform.Translation =
                        new Vector3D(
                            transform.Translation.X,
                            transform.Translation.Y,
                            value
                        );

                    RaiseTransformModified();
                }
            }
        }

        public double Rx
        {
            get
            {
                lock (transformLock)
                {
                    return transform.Rotation.AsFixed().Rx;
                }
            }
            set
            {
                lock (transformLock)
                {
                    transform.Rotation =
                        new FixedRotation3D(
                            value,
                            transform.Rotation.AsFixed().Ry,
                            transform.Rotation.AsFixed().Rz
                        );

                    RaiseTransformModified();
                }
            }
        }

        public double Ry
        {
            get
            {
                lock (transformLock)
                {
                    return transform.Rotation.AsFixed().Ry;
                }
            }
            set
            {
                lock (transformLock)
                {
                    transform.Rotation =
                        new FixedRotation3D(
                            transform.Rotation.AsFixed().Rx,
                            value,
                            transform.Rotation.AsFixed().Rz
                        );

                    RaiseTransformModified();
                }
            }
        }

        public double Rz
        {
            get
            {
                lock (transformLock)
                {
                    return transform.Rotation.AsFixed().Rz;
                }
            }
            set
            {
                lock (transformLock)
                {
                    transform.Rotation =
                        new FixedRotation3D(
                            transform.Rotation.AsFixed().Rx,
                            transform.Rotation.AsFixed().Ry,
                            value
                        );

                    RaiseTransformModified();
                }
            }
        }

        public event TransformModifiedEventHandler? TransformModified;

        public Transform3D GetTransformCopy()
        {
            lock (transformLock)
            {
                var t = new Transform3D(transform);

                return t;
            }
        }

        public void SetTransform(Transform3D transform)
        {
            lock (transformLock)
            {
                this.transform = new Transform3D(transform);
                RaiseTransformModified();
            }
        }

        private void RaiseTransformModified()
        {
            TransformModified?.Invoke(
                this,
                new TransformModifiedEventArgs(
                    new Transform3D(transform)
                )
            );
        }

        private Transform3D transform;
        private readonly object transformLock = new object();
    }
}
