using Avalonia.Controls;
using Avalonia.Styling;

namespace JSim.Avalonia.Controls
{
    public class ExtendedMenuItem : MenuItem, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ExtendedMenuItem);
    }
}
