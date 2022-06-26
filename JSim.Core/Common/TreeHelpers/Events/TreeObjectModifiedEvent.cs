namespace JSim.Core.Common
{
    public delegate void TreeObjectModifiedEventHandler(object sender, TreeObjectModifiedEventArgs e);
    public class TreeObjectModifiedEventArgs : EventArgs
    {
        public TreeObjectModifiedEventArgs(ITreeObject modifiedObject)
        {
            ModifiedObject = modifiedObject;
        }

        public ITreeObject ModifiedObject { get; }
    }
}
