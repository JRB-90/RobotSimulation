namespace JSim.Core.Display
{
    public delegate void SurfaceRequiresRenderEventHandler(object sender, SurfaceRequiresRenderEventArgs e);
    public class SurfaceRequiresRenderEventArgs : EventArgs
    {
        public SurfaceRequiresRenderEventArgs(IRenderingSurface surface)
        {
            Surface = surface;
        }

        public IRenderingSurface Surface { get; }
    }
}
