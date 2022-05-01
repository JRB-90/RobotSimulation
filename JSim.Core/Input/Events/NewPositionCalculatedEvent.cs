using JSim.Core.Maths;

namespace JSim.Core.Input
{
    public delegate void NewPositionCalculatedEventHandler(object sender, NewPositionCalculatedEventArgs e);
    public class NewPositionCalculatedEventArgs : EventArgs
    {
        public NewPositionCalculatedEventArgs(Transform3D newPosition)
        {
            NewPosition = newPosition;
        }

        public Transform3D NewPosition { get; }
    }
}
