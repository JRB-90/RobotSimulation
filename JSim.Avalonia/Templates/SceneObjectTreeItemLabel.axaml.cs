using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Material.Icons;

namespace JSim.Avalonia.Templates
{
    public partial class SceneObjectTreeItemLabel : ContentControl
    {
        public SceneObjectTreeItemLabel()
        {
            InitializeComponent();
        }

        public static readonly StyledProperty<MaterialIconKind> IconProperty =
            AvaloniaProperty.Register<SceneObjectTreeItemLabel, MaterialIconKind>(
                nameof(Icon)
            );

        public MaterialIconKind Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly StyledProperty<string?> ObjectNameProperty =
            AvaloniaProperty.Register<SceneObjectTreeItemLabel, string?>(
                nameof(ObjectName)
            );

        public string? ObjectName
        {
            get => GetValue(ObjectNameProperty);
            set => SetValue(ObjectNameProperty, value);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
