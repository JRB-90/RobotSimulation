using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JSimControlGallery.Controls
{
    public partial class MaterialControl : UserControl
    {
        public MaterialControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
