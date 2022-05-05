using JSim.Core.Maths;

namespace JSim.Core.Input
{
    public delegate void MouseButtonPressedEventHandler(object sender, MouseButtonPressedEventArgs e);
    public class MouseButtonPressedEventArgs : EventArgs
    {
        public MouseButtonPressedEventArgs(
            MouseButton button,
            Vector2D position)
        {
            Button = button;
            Position = position;
        }

        public MouseButton Button { get; }

        public Vector2D Position { get; }
    }
}
