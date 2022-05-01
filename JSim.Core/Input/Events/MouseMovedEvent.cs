namespace JSim.Core.Input
{
    public delegate void MouseMovedEventHandler(object sender, MouseMovedEventArgs e);
    public class MouseMovedEventArgs : EventArgs
    {
        public MouseMovedEventArgs(
            double deltaX,
            double deltaY)
        {
            DeltaX = deltaX;
            DeltaY = deltaY;
        }

        public double DeltaX { get; }

        public double DeltaY { get; }
    }
}
