using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JSim.Core.Render;

namespace JSimControlGallery.Controls
{
    public partial class GeometryControl : UserControl
    {
        readonly CheckBox isVisibleCheckBox;
        readonly CheckBox isHighlightedCheckBox;

        public GeometryControl()
        {
            InitializeComponent();

            isVisibleCheckBox = this.FindControl<CheckBox>("IsVisibleCheckBox");
            isHighlightedCheckBox = this.FindControl<CheckBox>("IsHighlightedCheckBox");
            isVisibleCheckBox.PropertyChanged += OnIsVisibleChanged;
            isHighlightedCheckBox.PropertyChanged += OnIsHighlightedChanged;

            MaterialControl = new MaterialControl() { };
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

        public MaterialControl? MaterialControl
        {
            get => materialControl;
            private set => SetAndRaise(MaterialControlProperty, ref materialControl, value);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void UpdateDisplayedValues()
        {
            if (geometry != null)
            {
                RaisePropertyChanged(GeometryNameProperty, "", geometry.Name);
                RaisePropertyChanged(GeometryIDProperty, "", geometry.ID.ToString());
                isVisibleCheckBox.IsChecked = geometry.IsVisible;
                isHighlightedCheckBox.IsChecked = geometry.IsHighlighted;
                MaterialControl = new MaterialControl() { Material = geometry.Material };
            }
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
                    Geometry.IsVisible = isHighlightedCheckBox.IsChecked.Value;
                }
            }
        }

        private IGeometry? geometry;
        private MaterialControl? materialControl;
    }
}
