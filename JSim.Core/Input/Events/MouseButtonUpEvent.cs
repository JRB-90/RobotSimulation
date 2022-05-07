namespace JSim.Core.Input
{
    public delegate void MouseButtonUpEventHandler(object sender, MouseButtonUpEventArgs e);
    public class MouseButtonUpEventArgs : EventArgs
    {
        public MouseButtonUpEventArgs(MouseButton button)
        {
            Button = button;
        }

        public MouseButton Button { get; }
    }
}
