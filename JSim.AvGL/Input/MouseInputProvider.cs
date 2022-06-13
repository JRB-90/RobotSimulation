using Avalonia.Controls;
using JSim.Core.Input;
using JSim.Core.Maths;

namespace JSim.AvGL
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

        public event MouseButtonDownEventHandler? MouseButtonDown;

        public event MouseButtonUpEventHandler? MouseButtonUp;

        public event MouseWheelEventHandler? MouseWheelMoved;

        private void OnPointerMoved(object? sender, global::Avalonia.Input.PointerEventArgs e)
        {
            var pos = e.GetPosition(control);

            MouseMoved?.Invoke(
                this, 
                new MouseMovedEventArgs(new Vector2D(pos.X, pos.Y))
            );
        }

        private void OnPointerPressed(object? sender, global::Avalonia.Input.PointerPressedEventArgs e)
        {
            var pos = e.GetCurrentPoint(control).Position;
            var state = e.GetCurrentPoint(control).Properties;

            if (state.IsLeftButtonPressed)
            {
                MouseButtonDown?.Invoke(
                    this, 
                    new MouseButtonDownEventArgs(
                        MouseButton.Left, 
                        new Vector2D(pos.X, pos.Y)
                    )
                );
            }

            if (state.IsRightButtonPressed)
            {
                MouseButtonDown?.Invoke(
                    this,
                    new MouseButtonDownEventArgs(
                        MouseButton.Right,
                        new Vector2D(pos.X, pos.Y)
                    )
                );
            }

            if (state.IsMiddleButtonPressed)
            {
                MouseButtonDown?.Invoke(
                    this,
                    new MouseButtonDownEventArgs(
                        MouseButton.Middle,
                        new Vector2D(pos.X, pos.Y)
                    )
                );
            }
        }

        private void OnPointerReleased(object? sender, global::Avalonia.Input.PointerReleasedEventArgs e)
        {
            var state = e.GetCurrentPoint(control).Properties;

            if (!state.IsLeftButtonPressed)
            {
                MouseButtonUp?.Invoke(
                    this, 
                    new MouseButtonUpEventArgs(MouseButton.Left)
                );
            }

            if (!state.IsRightButtonPressed)
            {
                MouseButtonUp?.Invoke(
                    this, 
                    new MouseButtonUpEventArgs(MouseButton.Right)
                );
            }

            if (!state.IsMiddleButtonPressed)
            {
                MouseButtonUp?.Invoke(
                    this, 
                    new MouseButtonUpEventArgs(MouseButton.Middle)
                );
            }
        }

        private void OnPointerWheelChanged(object? sender, global::Avalonia.Input.PointerWheelEventArgs e)
        {
            MouseWheelMoved?.Invoke(
                this, 
                new MouseWheelEventArgs(e.Delta.Y)
            );
        }
    }
}
