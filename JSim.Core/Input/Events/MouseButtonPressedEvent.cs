namespace JSim.Core.Input
{
    public delegate void MouseButtonPressedEventHandler(object sender, MouseButtonPressedEventArgs e);
    public class MouseButtonPressedEventArgs : EventArgs
    {
        public MouseButtonPressedEventArgs(MouseButton button)
        {
            Button = button;
        }

        public MouseButton Button { get; }
    }
}
