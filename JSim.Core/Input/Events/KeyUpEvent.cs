namespace JSim.Core.Input
{
    public delegate void KeyUpEventHandler(object sender, KeyUpEventArgs e);
    public class KeyUpEventArgs : EventArgs
    {
        public KeyUpEventArgs(Keys key)
        {
            Key = key;
        }

        public Keys Key { get; }
    }
}
