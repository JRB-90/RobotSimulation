using Avalonia.Controls;
using JSim.Core.Input;
using JSim.Core.Maths;

namespace JSim.OpenTK.Input
{
    /// <summary>
    /// Provides a JSim mouse input provider implementation to be used with avalonia controls.
    /// </summary>
    public class MouseInputProvider : IMouseInputProvider
    {
        readonly Control control;

        public MouseInputProvider(Control control)
        {
            this.control = control;
            control.PointerMoved += OnPointerMoved;
            control.PointerPressed += OnPointerPressed;
            control.PointerReleased += OnPointerReleased;
            control.PointerWheelChanged += OnPointerWheelChanged;
        }

        public event MouseMovedEventHandler? MouseMoved;

        public event MouseButtonPressedEventHandler? MouseButtonPressed;

        public event MouseButtonReleasedEventHandler? MouseButtonReleased;

        public event MouseWheelEventHandler? MouseWheelMoved;

        private void OnPointerMoved(object? sender, global::Avalonia.Input.PointerEventArgs e)
        {
            var pos = e.GetPosition(control);
            MouseMoved?.Invoke(this, new MouseMovedEventArgs(new Vector2D(pos.X, pos.Y)));
        }

        private void OnPointerPressed(object? sender, global::Avalonia.Input.PointerPressedEventArgs e)
        {
            var state = e.GetCurrentPoint(control).Properties;

            if (state.IsLeftButtonPressed)
            {
                MouseButtonPressed?.Invoke(this, new MouseButtonPressedEventArgs(MouseButton.Left));
            }
            if (state.IsRightButtonPressed)
            {
                MouseButtonPressed?.Invoke(this, new MouseButtonPressedEventArgs(MouseButton.Right));
            }
            if (state.IsMiddleButtonPressed)
            {
                MouseButtonPressed?.Invoke(this, new MouseButtonPressedEventArgs(MouseButton.Middle));
            }
        }

        private void OnPointerReleased(object? sender, global::Avalonia.Input.PointerReleasedEventArgs e)
        {
            var state = e.GetCurrentPoint(control).Properties;

            if (!state.IsLeftButtonPressed)
            {
                MouseButtonReleased?.Invoke(this, new MouseButtonReleasedEventArgs(MouseButton.Left));
            }
            if (!state.IsRightButtonPressed)
            {
                MouseButtonReleased?.Invoke(this, new MouseButtonReleasedEventArgs(MouseButton.Right));
            }
            if (!state.IsMiddleButtonPressed)
            {
                MouseButtonReleased?.Invoke(this, new MouseButtonReleasedEventArgs(MouseButton.Middle));
            }
        }

        private void OnPointerWheelChanged(object? sender, global::Avalonia.Input.PointerWheelEventArgs e)
        {
            MouseWheelMoved?.Invoke(this, new MouseWheelEventArgs(e.Delta.Length));
        }
    }
}
