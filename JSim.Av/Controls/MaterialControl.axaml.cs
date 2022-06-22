using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using AvaloniaColorPicker;
using JSim.Core.Render;
using System.Diagnostics;

namespace JSim.Av.Controls
{
    public partial class MaterialControl : UserControl
    {
        readonly Grid mainGrid;
        readonly ColorButton<RGBColorPickerWindow> ambientColorButton;
        readonly ColorButton<RGBColorPickerWindow> diffuseColorButton;
        readonly ColorButton<RGBColorPickerWindow> specularColorButton;
        readonly ComboBox shadingComboBox;

        public MaterialControl()
        {
            InitializeComponent();

            material = new Material();

            mainGrid = this.FindControl<Grid>("MainGrid");

            ambientColorButton = new ColorButton<RGBColorPickerWindow>();
            diffuseColorButton = new ColorButton<RGBColorPickerWindow>();
            specularColorButton = new ColorButton<RGBColorPickerWindow>();

            AddColorButtonToGrid(ambientColorButton, 0, 1);
            AddColorButtonToGrid(diffuseColorButton, 1, 1);
            AddColorButtonToGrid(specularColorButton, 2, 1);

            ambientColorButton.PropertyChanged += OnAmbientColorChanged;
            diffuseColorButton.PropertyChanged += OnDiffuseColorChanged;
            specularColorButton.PropertyChanged += OnSpecularColorChanged;

            shadingComboBox = this.FindControl<ComboBox>("ShadingComboBox");
            shadingComboBox.Items = Enum.GetValues(typeof(ShadingType)).Cast<ShadingType>();
            shadingComboBox.SelectedItem = ShadingType.Solid;
            shadingComboBox.SelectionChanged += OnShadingTypeChanged;

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(Material))
            {
                UpdateDisplayedValues();
            }
        }

        public static readonly DirectProperty<MaterialControl, IMaterial> MaterialProperty =
            AvaloniaProperty.RegisterDirect<MaterialControl, IMaterial>(
                nameof(Material),
                o => o.Material,
                (o, v) => o.Material = v
           );

        public IMaterial Material
        {
            get => material;
            set => SetAndRaise(MaterialProperty, ref material, value);
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
            shadingComboBox.SelectedItem = Material.Shading;

            Trace.WriteLine("Updated displayed material properties");
        }

        private void OnAmbientColorChanged(
            object? sender,
            AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(ColorButton.Color))
            {
                Material.Ambient = ToJSimColor(ambientColorButton.Color);
            }
        }

        private void OnDiffuseColorChanged(
            object? sender, 
            AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(ColorButton.Color))
            {
                Material.Diffuse = ToJSimColor(diffuseColorButton.Color);
            }
        }

        private void OnSpecularColorChanged(
            object? sender,
            AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(ColorButton.Color))
            {
                Material.Specular = ToJSimColor(specularColorButton.Color);
            }
        }

        private void OnShadingTypeChanged(
            object? sender, 
            SelectionChangedEventArgs e)
        {
            if (shadingComboBox.SelectedItem != null)
            {
                Material.Shading = (ShadingType)shadingComboBox.SelectedItem;
            }
        }

        private static Color ToJSimColor(Avalonia.Media.Color color)
        {
            return
                new Color(
                    color.A,
                    color.R,
                    color.G,
                    color.B
                );
        }

        private static Avalonia.Media.Color ToAvaloniaColor(Color color)
        {
            return
                new Avalonia.Media.Color(
                    color.A.ArgbFloatToByte(),
                    color.R.ArgbFloatToByte(),
                    color.G.ArgbFloatToByte(),
                    color.B.ArgbFloatToByte()
                );
        }

        private void AddColorButtonToGrid(
            ColorButton<RGBColorPickerWindow> control,
            int row, 
            int column)
        {
            control.Margin = new Thickness(5);
            Grid.SetRow(control, row);
            Grid.SetColumn(control, column);
            mainGrid.Children.Add(control);
        }

        private double shininess;
        private IMaterial material;

        private class RGBColorPickerWindow : ColorPickerWindow
        {
            public RGBColorPickerWindow()
              :
                base()
            {
                SetProperties();
            }

            public RGBColorPickerWindow(Avalonia.Media.Color? previousColour)
              :
                base(previousColour)
            {
                SetProperties();
            }

            private void SetProperties()
            {
                ColorSpace = ColorPicker.ColorSpaces.RGB;
                IsColourSpaceSelectorVisible = false;
                IsColourBlindnessSelectorVisible = false;
                IsPaletteVisible = false;
                IsHSBSelectable = false;
                IsHSBVisible = false;
                IsCIELABSelectable = false;
                IsCIELABVisible = false;
                Color = Color;
            }
        }
    }
}
