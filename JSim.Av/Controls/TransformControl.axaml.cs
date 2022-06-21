using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using JSim.Core.Maths;
using System;
using System.Diagnostics;

namespace JSim.Av.Controls
{
    public partial class TransformControl : UserControl
    {
        readonly LateValidationTextBox xTextBox;
        readonly LateValidationTextBox yTextBox;
        readonly LateValidationTextBox zTextBox;
        readonly LateValidationTextBox rxTextBox;
        readonly LateValidationTextBox ryTextBox;
        readonly LateValidationTextBox rzTextBox;

        public TransformControl()
        {
            AvaloniaXamlLoader.Load(this);

            x = "";
            y = "";
            z = "";
            rx = "";
            ry = "";
            rz = "";

            xTextBox = this.FindControl<LateValidationTextBox>("XTextBox");
            yTextBox = this.FindControl<LateValidationTextBox>("YTextBox");
            zTextBox = this.FindControl<LateValidationTextBox>("ZTextBox");
            rxTextBox = this.FindControl<LateValidationTextBox>("RxTextBox");
            ryTextBox = this.FindControl<LateValidationTextBox>("RyTextBox");
            rzTextBox = this.FindControl<LateValidationTextBox>("RzTextBox");

            Transform = Transform3D.Identity;
        }

        public event TransformUpdatedEventHandler? TransformUpdated;

        public readonly static DirectProperty<TransformControl, Transform3D?> TransformProperty =
            AvaloniaProperty.RegisterDirect<TransformControl, Transform3D?>(
                nameof(Transform),
                o => o.Transform,
                (o, v) => o.Transform = v,
                null,
                BindingMode.TwoWay
            );

        public Transform3D? Transform
        {
            get => transform;
            set
            {
                SetAndRaise(TransformProperty, ref transform, value);
                RefreshDisplay();
            }
        }

        public readonly static DirectProperty<TransformControl, string> XProperty =
            AvaloniaProperty.RegisterDirect<TransformControl, string>(
                nameof(X),
                o => o.X,
                (o, v) => o.X = v,
                defaultBindingMode: BindingMode.TwoWay
            );

        public string X
        {
            get => x;
            set
            {
                if (Transform != null)
                {
                    try
                    {
                        var val = Convert.ToDouble(value);
                        Transform.Translation =
                            new Vector3D(
                                val,
                                Transform.Translation.Y,
                                Transform.Translation.Z
                            );
                        TransformUpdated?.Invoke(this, new TransformUpdatedEventArgs(Transform));
                        RefreshDisplay();
                    }
                    catch
                    {
                        xTextBox.ValidatedText = x;
                    }
                }
            }
        }

        public readonly static DirectProperty<TransformControl, string> YProperty =
            AvaloniaProperty.RegisterDirect<TransformControl, string>(
                nameof(Y),
                o => o.Y,
                (o, v) => o.Y = v,
                defaultBindingMode: BindingMode.TwoWay
            );

        public string Y
        {
            get => y;
            set
            {
                if (Transform != null)
                {
                    try
                    {
                        var val = Convert.ToDouble(value);
                        Transform.Translation =
                            new Vector3D(
                                Transform.Translation.X,
                                val,
                                Transform.Translation.Z
                            );
                        TransformUpdated?.Invoke(this, new TransformUpdatedEventArgs(Transform));
                        RefreshDisplay();
                    }
                    catch
                    {
                        yTextBox.ValidatedText = y;
                    }
                }
            }
        }

        public readonly static DirectProperty<TransformControl, string> ZProperty =
            AvaloniaProperty.RegisterDirect<TransformControl, string>(
                nameof(Z),
                o => o.Z,
                (o, v) => o.Z = v,
                defaultBindingMode: BindingMode.TwoWay
            );

        public string Z
        {
            get => z;
            set
            {
                if (Transform != null)
                {
                    try
                    {
                        var val = Convert.ToDouble(value);
                        Transform.Translation =
                            new Vector3D(
                                Transform.Translation.X,
                                Transform.Translation.Y,
                                val
                            );
                        TransformUpdated?.Invoke(this, new TransformUpdatedEventArgs(Transform));
                        RefreshDisplay();
                    }
                    catch
                    {
                        zTextBox.ValidatedText = z;
                    }
                }
            }
        }

        public readonly static DirectProperty<TransformControl, string> RxProperty =
            AvaloniaProperty.RegisterDirect<TransformControl, string>(
                nameof(Rx),
                o => o.Rx,
                (o, v) => o.Rx = v,
                defaultBindingMode: BindingMode.TwoWay
            );

        public string Rx
        {
            get => rx;
            set
            {
                if (Transform != null)
                {
                    try
                    {
                        var val = Convert.ToDouble(value);
                        Transform.Rotation =
                            new FixedRotation3D(
                                val,
                                Transform.Rotation.AsFixed().Ry,
                                Transform.Rotation.AsFixed().Rz
                            );
                        TransformUpdated?.Invoke(this, new TransformUpdatedEventArgs(Transform));
                        RefreshDisplay();
                    }
                    catch
                    {
                        rxTextBox.ValidatedText = rx;
                    }
                }
            }
        }

        public readonly static DirectProperty<TransformControl, string> RyProperty =
            AvaloniaProperty.RegisterDirect<TransformControl, string>(
                nameof(Ry),
                o => o.Ry,
                (o, v) => o.Ry = v,
                defaultBindingMode: BindingMode.TwoWay
            );

        public string Ry
        {
            get => ry;
            set
            {
                if (Transform != null)
                {
                    try
                    {
                        var val = Convert.ToDouble(value);
                        Transform.Rotation =
                            new FixedRotation3D(
                                Transform.Rotation.AsFixed().Rx,
                                val,
                                Transform.Rotation.AsFixed().Rz
                            );
                        TransformUpdated?.Invoke(this, new TransformUpdatedEventArgs(Transform));
                        RefreshDisplay();
                    }
                    catch
                    {
                        ryTextBox.ValidatedText = ry;
                    }
                }
            }
        }

        public readonly static DirectProperty<TransformControl, string> RzProperty =
            AvaloniaProperty.RegisterDirect<TransformControl, string>(
                nameof(Rz),
                o => o.Rz,
                (o, v) => o.Rz = v,
                defaultBindingMode: BindingMode.TwoWay
            );

        public string Rz
        {
            get => rz;
            set
            {
                if (Transform != null)
                {
                    try
                    {
                        var val = Convert.ToDouble(value);
                        Transform.Rotation =
                            new FixedRotation3D(
                                Transform.Rotation.AsFixed().Rx,
                                Transform.Rotation.AsFixed().Ry,
                                val
                            );
                        TransformUpdated?.Invoke(this, new TransformUpdatedEventArgs(Transform));
                        RefreshDisplay();
                    }
                    catch
                    {
                        rzTextBox.ValidatedText = rz;
                    }
                }
            }
        }

        private void RefreshDisplay()
        {
            if (Transform != null)
            {
                SetAndRaise(XProperty, ref x, $"{Transform.Translation.X:F3}");
                SetAndRaise(YProperty, ref y, $"{Transform.Translation.Y:F3}");
                SetAndRaise(ZProperty, ref z, $"{Transform.Translation.Z:F3}");
                SetAndRaise(RxProperty, ref rx, $"{Transform.Rotation.AsFixed().Rx:F3}");
                SetAndRaise(RyProperty, ref ry, $"{Transform.Rotation.AsFixed().Ry:F3}");
                SetAndRaise(RzProperty, ref rz, $"{Transform.Rotation.AsFixed().Rz:F3}");
            }
            else
            {
                SetAndRaise(XProperty, ref x, "");
                SetAndRaise(YProperty, ref y, "");
                SetAndRaise(ZProperty, ref z, "");
                SetAndRaise(RxProperty, ref rx, "");
                SetAndRaise(RyProperty, ref ry, "");
                SetAndRaise(RzProperty, ref rz, "");
            }
        }

        private Transform3D? transform;
        private string x;
        private string y;
        private string z;
        private string rx;
        private string ry;
        private string rz;
    }
}
