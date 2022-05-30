using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using AvaloniaColorPicker;
using JSim.Core.Render;
using System.Diagnostics;

namespace JSimControlGallery.Controls
{
    public partial class MaterialControl : UserControl
    {
        readonly ColorButton ambientColorButton;
        readonly ColorButton diffuseColorButton;
        readonly ColorButton specularColorButton;

        public MaterialControl()
        {
            InitializeComponent();

            ambientColorButton = this.FindControl<ColorButton>("AmbientColorButton");
            diffuseColorButton = this.FindControl<ColorButton>("DiffuseColorButton");
            specularColorButton = this.FindControl<ColorButton>("SpecularColorButton");
            ambientColorButton.PropertyChanged += OnAmbientColorChanged;
            diffuseColorButton.PropertyChanged += OnDiffuseColorChanged;
            specularColorButton.PropertyChanged += OnSpecularColorChanged;

            PropertyChanged += OnPropertyChanged;
            Material.MaterialModified += OnMaterialModified;
        }

        public static readonly StyledProperty<Material> MaterialProperty =
            AvaloniaProperty.Register<MaterialControl, Material>(
                nameof(Material),
                new Material(),
                false,
                BindingMode.TwoWay
           );

        public Material Material
        {
            get => GetValue(MaterialProperty);
            set => SetValue(MaterialProperty, value);
        }

        public static readonly DirectProperty<MaterialControl, double> ShininessProperty =
            AvaloniaProperty.RegisterDirect<MaterialControl, double>(
                nameof(Shininess),
                o => o.Shininess,
                (o, v) => o.Shininess = v
            );

        public double Shininess
        {
            get => shininess;
            set
            {
                SetAndRaise(ShininessProperty, ref shininess, value);
                Material.Shininess = shininess;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void UpdateDisplayedValues()
        {
            ambientColorButton.Color = ToAvaloniaColor(Material.Ambient);
            diffuseColorButton.Color = ToAvaloniaColor(Material.Diffuse);
            specularColorButton.Color = ToAvaloniaColor(Material.Specular);
            Shininess = Material.Shininess;

            Trace.WriteLine("Updated displayed material properties");
        }

        private void OnMaterialModified(object sender, MaterialModifiedEventArgs e)
        {
            Trace.WriteLine("Material modified");
        }

        private void OnAmbientColorChanged(
            object? sender,
            AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == ColorButton.ColorProperty)
            {
                Material.Ambient = ToJSimColor(ambientColorButton.Color);
            }
        }

        private void OnDiffuseColorChanged(
            object? sender, 
            AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == ColorButton.ColorProperty)
            {
                Material.Diffuse = ToJSimColor(diffuseColorButton.Color);
            }
        }

        private void OnSpecularColorChanged(
            object? sender,
            AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == ColorButton.ColorProperty)
            {
                Material.Specular = ToJSimColor(specularColorButton.Color);
            }
        }

        private void OnPropertyChanged(
            object? sender,
            AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == MaterialProperty)
            {
                UpdateDisplayedValues();
            }
        }

        private Color ToJSimColor(Avalonia.Media.Color color)
        {
            return
                new Color(
                    color.A,
                    color.R,
                    color.G,
                    color.B
                );
        }

        private Avalonia.Media.Color ToAvaloniaColor(Color color)
        {
            return
                new Avalonia.Media.Color(
                    color.A.ArgbFloatToByte(),
                    color.R.ArgbFloatToByte(),
                    color.G.ArgbFloatToByte(),
                    color.B.ArgbFloatToByte()
                );
        }

        private double shininess;
    }
}
