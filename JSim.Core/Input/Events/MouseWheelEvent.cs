namespace JSim.Core.Input
{
    public delegate void MouseWheelEventHandler(object sender, MouseWheelEventArgs e);
    public class MouseWheelEventArgs : EventArgs
    {
        public MouseWheelEventArgs(double wheelDelta)
        {
            WheelDelta = wheelDelta;
        }

        public double WheelDelta { get; }
    }
}
