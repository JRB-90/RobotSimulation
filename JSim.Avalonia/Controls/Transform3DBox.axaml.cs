using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using JSim.Core.Maths;

namespace JSim.Avalonia.Controls
{
    public partial class Transform3DBox : UserControl
    {
        public Transform3DBox()
        {
            InitializeComponent();
            translation = Vector3D.Origin;
            rotation = new FixedRotation3D(0.0, 0.0, 0.0);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static readonly StyledProperty<Transform3D?> TransformProperty =
            AvaloniaProperty.Register<Transform3DBox, Transform3D?>(
                nameof(Transform),
                Transform3D.Identity,
                defaultBindingMode: BindingMode.TwoWay
            );

        public static readonly DirectProperty<Transform3DBox, Transform3D?> Transform2Property =
            AvaloniaProperty.RegisterDirect<Transform3DBox, Transform3D?>(
                nameof(Transform2),
                o => o.Transform2,
                (o, v) => o.Transform2 = v,
                defaultBindingMode: BindingMode.TwoWay,
                enableDataValidation: true
            );

        public Transform3D? Transform
        {
            get => GetValue(TransformProperty);
            set
            {
                if (value != null)
                {
                    translation = new Vector3D(value.Translation);
                    rotation = new FixedRotation3D(value.Rotation);
                }
                SetValue(TransformProperty, value);
            }
        }

        public Transform3D? Transform2
        {
            get => transform2;
            set => SetAndRaise(Transform2Property, ref transform2, value);
        }

        public double? X
        {
            get => Transform?.Translation.X;
            set
            {
                if (value.HasValue)
                {
                    translation.X = value.Value;
                    Rebuild();
                }
            }
        }

        public double? Y
        {
            get => Transform?.Translation.Y;
            set
            {
                if (value.HasValue)
                {
                    translation.Y = value.Value;
                    Rebuild();
                }
            }
        }

        public double? Z
        {
            get => Transform?.Translation.Z;
            set
            {
                if (value.HasValue)
                {
                    translation.Z = value.Value;
                    Rebuild();
                }
            }
        }

        public double? Rx
        {
            get => Transform?.Rotation.AsFixed().Rx;
            set
            {
                if (value.HasValue)
                {
                    rotation.Rx = value.Value;
                    Rebuild();
                }
            }
        }

        public double? Ry
        {
            get => Transform?.Rotation.AsFixed().Ry;
            set
            {
                if (value.HasValue)
                {
                    rotation.Ry = value.Value;
                    Rebuild();
                }
            }
        }

        public double? Rz
        {
            get => Transform?.Rotation.AsFixed().Rz;
            set
            {
                if (value.HasValue)
                {
                    rotation.Rz = value.Value;
                    Rebuild();
                }
            }
        }

        private void Rebuild()
        {
            Transform =
                new Transform3D(
                    translation,
                    rotation
                );

            Transform2 =
                new Transform3D(
                    translation,
                    rotation
                );
        }

        private Vector3D translation;
        private FixedRotation3D rotation;

        private Transform3D? transform2;
    }
}
