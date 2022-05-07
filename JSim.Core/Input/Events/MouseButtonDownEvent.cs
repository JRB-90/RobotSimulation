using JSim.Core.Maths;

namespace JSim.Core.Input
{
    public delegate void MouseButtonDownEventHandler(object sender, MouseButtonDownEventArgs e);
    public class MouseButtonDownEventArgs : EventArgs
    {
        public MouseButtonDownEventArgs(
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
