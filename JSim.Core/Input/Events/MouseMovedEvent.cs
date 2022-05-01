using JSim.Core.Maths;

namespace JSim.Core.Input
{
    public delegate void MouseMovedEventHandler(object sender, MouseMovedEventArgs e);
    public class MouseMovedEventArgs : EventArgs
    {
        public MouseMovedEventArgs(Vector2D newPosition)
        {
            NewPosition = newPosition;
        }

        public Vector2D NewPosition { get; }
    }
}
