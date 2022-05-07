using Avalonia.Controls;
using Avalonia.Input;
using JSim.Core.Input;

namespace JSim.OpenTK.Input
{
    /// <summary>
    /// Provides a JSim keyboard input provider implementation to be used with avalonia controls.
    /// </summary>
    public class ControlKeyInputProvider : IKeyboardProvider
    {
        readonly Control control;

        public ControlKeyInputProvider(Control control)
        {
            this.control = control;
            control.KeyDown += OnKeyDown;
            control.KeyUp += OnKeyUp;
        }

        public event KeyDownEventHandler? KeyDown;

        public event KeyUpEventHandler? KeyUp;

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (KeyConverter.TryConvertKey(e.Key, out Keys key))
            {
                KeyDown?.Invoke(this, new KeyDownEventArgs(key));
            }
        }

        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
            if (KeyConverter.TryConvertKey(e.Key, out Keys key))
            {
                KeyUp?.Invoke(this, new KeyUpEventArgs(key));
            }
        }
    }
}
