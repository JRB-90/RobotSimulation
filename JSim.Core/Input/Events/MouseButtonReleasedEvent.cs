namespace JSim.Core.Input
{
    public delegate void MouseButtonReleasedEventHandler(object sender, MouseButtonReleasedEventArgs e);
    public class MouseButtonReleasedEventArgs : EventArgs
    {
        public MouseButtonReleasedEventArgs(MouseButton button)
        {
            Button = button;
        }

        public MouseButton Button { get; }
    }
}
