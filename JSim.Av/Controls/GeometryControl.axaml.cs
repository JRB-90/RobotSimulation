using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using JSim.Core.Render;

namespace JSim.Av.Controls
{
    public partial class GeometryControl : UserControl
    {
        readonly CheckBox isVisibleCheckBox;
        readonly CheckBox isHighlightedCheckBox;
        readonly TransformControl transformControl;
        readonly RadioButton localRadioButton;

        public GeometryControl()
        {
            AvaloniaXamlLoader.Load(this);

            isVisibleCheckBox = this.FindControl<CheckBox>("IsVisibleCheckBox");
            isHighlightedCheckBox = this.FindControl<CheckBox>("IsHighlightedCheckBox");
            transformControl = this.FindControl<TransformControl>("TransformControl");
            localRadioButton = this.FindControl<RadioButton>("LocalRadioButton");
            isVisibleCheckBox.PropertyChanged += OnIsVisibleChanged;
            isHighlightedCheckBox.PropertyChanged += OnIsHighlightedChanged;
            localRadioButton.PropertyChanged += OnPropertyChanged;

            isLocalSelected = localRadioButton.IsChecked.Value;
            MaterialControl = new MaterialControl() { };
            UpdateDisplayedValues();
        }

        public static readonly DirectProperty<GeometryControl, IGeometry?> GeometryProperty =
            AvaloniaProperty.RegisterDirect<GeometryControl, IGeometry?>(
                nameof(Geometry),
                o => o.Geometry,
                (o, v) => o.Geometry = v
            );

        public IGeometry? Geometry
        {
            get => geometry;
            set
            {
                SetAndRaise(GeometryProperty, ref geometry, value);
                localRadioButton.IsChecked = isLocalSelected;
                UpdateDisplayedValues();
            }
        }

        public static readonly DirectProperty<GeometryControl, string?> GeometryNameProperty =
            AvaloniaProperty.RegisterDirect<GeometryControl, string?>(
                nameof(GeometryName),
                o => o.GeometryName
            );

        public string? GeometryName =>
            Geometry?.Name;

        public static readonly DirectProperty<GeometryControl, string?> GeometryIDProperty =
            AvaloniaProperty.RegisterDirect<GeometryControl, string?>(
                nameof(GeometryID),
                o => o.GeometryID
            );

        public string? GeometryID =>
            Geometry?.ID.ToString();

        public static readonly DirectProperty<GeometryControl, MaterialControl?> MaterialControlProperty =
            AvaloniaProperty.RegisterDirect<GeometryControl, MaterialControl?>(
                nameof(MaterialControl),
                o => o.MaterialControl
            );

        public static readonly DirectProperty<GeometryControl, int?> VertexCountProperty =
            AvaloniaProperty.RegisterDirect<GeometryControl, int?>(
                nameof(VertexCount),
                o => o.VertexCount
            );

        public int? VertexCount =>
            Geometry?.Vertices.Count;

        public MaterialControl? MaterialControl
        {
            get => materialControl;
            private set => SetAndRaise(MaterialControlProperty, ref materialControl, value);
        }

        private void UpdateDisplayedValues()
        {
            if (geometry != null)
            {
                isVisibleCheckBox.IsEnabled = true;
                isVisibleCheckBox.IsChecked = geometry.IsVisible;
                isHighlightedCheckBox.IsEnabled = true;
                isHighlightedCheckBox.IsChecked = geometry.IsHighlighted;
                MaterialControl = new MaterialControl() { Material = geometry.Material };

                if (isLocalSelected)
                {
                    transformControl.Transform = geometry.LocalFrame;
                }
                else
                {
                    transformControl.Transform = geometry.WorldFrame;
                }
            }
            else
            {
                isVisibleCheckBox.IsEnabled = false;
                isVisibleCheckBox.IsChecked = false;
                isHighlightedCheckBox.IsChecked = false;
                isHighlightedCheckBox.IsEnabled = false;
                MaterialControl = null;
                transformControl.Transform = null;
            }

            RaisePropertyChanged(GeometryNameProperty, Optional<string?>.Empty, BindingValue<string?>.DoNothing);
            RaisePropertyChanged(GeometryIDProperty, Optional<string?>.Empty, BindingValue<string?>.DoNothing);
            RaisePropertyChanged(VertexCountProperty, Optional<int?>.Empty, BindingValue<int?>.DoNothing);
        }

        private void OnIsVisibleChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(CheckBox.IsChecked))
            {
                if (Geometry!= null &&
                    isVisibleCheckBox.IsChecked != null)
                {
                    Geometry.IsVisible = isVisibleCheckBox.IsChecked.Value;
                }
            }
        }

        private void OnIsHighlightedChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(CheckBox.IsChecked))
            {
                if (Geometry != null &&
                    isHighlightedCheckBox.IsChecked != null)
                {
                    Geometry.IsHighlighted = isHighlightedCheckBox.IsChecked.Value;
                }
            }
        }

        private void OnTransformUpdated(object sender, TransformUpdatedEventArgs e)
        {
            if (e.Transform != null &&
                Geometry != null)
            {
                if (isLocalSelected)
                {
                    Geometry.LocalFrame = e.Transform;
                }
                else
                {
                    Geometry.WorldFrame = e.Transform;
                }
            }
        }

        private void OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(RadioButton.IsChecked))
            {
                isLocalSelected = localRadioButton.IsChecked.Value;
                UpdateDisplayedValues();
            }
        }

        private IGeometry? geometry;
        private MaterialControl? materialControl;
        private bool isLocalSelected;
    }
}
