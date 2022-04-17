using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JSim.Avalonia.Views
{
    public partial class SceneObjectView : UserControl
    {
        public SceneObjectView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
