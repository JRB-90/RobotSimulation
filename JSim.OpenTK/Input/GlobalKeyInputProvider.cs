using Avalonia.Controls;
using JSim.Core.Input;

namespace JSim.OpenTK.Input
{
    /// <summary>
    /// Provides a JSim keyboard input provider implementation to be used with avalonia windows.
    /// </summary>
    public class GlobalKeyInputProvider : IKeyboardProvider
    {
        readonly Window window;

        public GlobalKeyInputProvider(Window window)
        {
            this.window = window;
            window.KeyDown += OnKeyDown;
            window.KeyUp += OnKeyUp;
        }

        public event KeyDownEventHandler? KeyDown;

        public event KeyUpEventHandler? KeyUp;

        private void OnKeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            if (KeyConverter.TryConvertKey(e.Key, out Keys key))
            {
                KeyDown?.Invoke(this, new KeyDownEventArgs(key));
            }
        }

        private void OnKeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            if (KeyConverter.TryConvertKey(e.Key, out Keys key))
            {
                KeyUp?.Invoke(this, new KeyUpEventArgs(key));
            }
        }
    }
}
