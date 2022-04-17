using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JSim.Avalonia.Views
{
    public partial class SceneObjectDataView : UserControl
    {
        public SceneObjectDataView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
