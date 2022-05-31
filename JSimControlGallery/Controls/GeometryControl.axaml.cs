using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JSim.Core.Render;

namespace JSimControlGallery.Controls
{
    public partial class GeometryControl : UserControl
    {
        public GeometryControl()
        {
            InitializeComponent();
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
                if (geometry != null)
                {
                    RaisePropertyChanged(GeometryNameProperty, "", geometry.Name);
                    MaterialControl = new MaterialControl() { Material = geometry.Material };
                }
            }
        }

        public static readonly DirectProperty<GeometryControl, string?> GeometryNameProperty =
            AvaloniaProperty.RegisterDirect<GeometryControl, string?>(
                nameof(GeometryName),
                o => o.GeometryName
            );

        public string? GeometryName =>
            Geometry?.Name;

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

        private IGeometry? geometry;
        private MaterialControl? materialControl;
    }
}
