namespace JSim.Core.Input
{
    public delegate void KeyDownEventHandler(object sender, KeyDownEventArgs e);
    public class KeyDownEventArgs : EventArgs
    {
        public KeyDownEventArgs(Keys key)
        {
            Key = key;
        }

        public Keys Key { get; }
    }
}
