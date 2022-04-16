using Avalonia.Controls;
using Avalonia.Input;

namespace JSim.Avalonia.Shared
{
    /// <summary>
    /// Used to track global Input events and allow VM's to observe them through
    /// this one object.
    /// Also capable of handling cursor changes.
    /// </summary>
    public class InputManager
    {
        readonly Window window;

        public InputManager(Window window)
        {
            this.window = window;
        }

        public Cursor? Cursor
        {
            get => window.Cursor;
            set => window.Cursor = value;
        }

        public event EventHandler<PointerEventArgs> PointerEnter
        {
            add => window.PointerEnter += value;
            remove => window.PointerEnter -= value;
        }

        public event EventHandler<PointerEventArgs> PointerLeave
        {
            add => window.PointerLeave += value;
            remove => window.PointerLeave -= value;
        }

        public event EventHandler<PointerEventArgs> PointerMoved
        {
            add => window.PointerMoved += value;
            remove => window.PointerMoved -= value;
        }

        public event EventHandler<PointerWheelEventArgs> PointerWheelChanged
        {
            add => window.PointerWheelChanged += value;
            remove => window.PointerWheelChanged -= value;
        }

        public event EventHandler<PointerPressedEventArgs> PointerPressed
        {
            add => window.PointerPressed += value;
            remove => window.PointerPressed -= value;
        }

        public event EventHandler<PointerReleasedEventArgs> PointerReleased
        {
            add => window.PointerReleased += value;
            remove => window.PointerReleased -= value;
        }

        public event EventHandler<PointerCaptureLostEventArgs> PointerCaptureLost
        {
            add => window.PointerCaptureLost += value;
            remove => window.PointerCaptureLost -= value;
        }
    }
}
