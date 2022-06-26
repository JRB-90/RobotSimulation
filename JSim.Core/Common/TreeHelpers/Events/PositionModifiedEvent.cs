namespace JSim.Core.Common
{
    public delegate void PositionModifiedEventHandler(object sender, PositionModifiedEventArgs e);
    public class PositionModifiedEventArgs : EventArgs
    {
        public PositionModifiedEventArgs(IPositionable movedObject)
        {
            MovedObject = movedObject;
        }

        public IPositionable MovedObject { get; }
    }
}
