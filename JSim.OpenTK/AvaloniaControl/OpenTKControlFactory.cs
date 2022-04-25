namespace JSim.OpenTK
{
    public class OpenTKControlFactory : IOpenTKControlFactory
    {
        readonly ISharedGlContextFactory glContextFactory;

        public OpenTKControlFactory(ISharedGlContextFactory glContextFactory)
        {
            this.glContextFactory = glContextFactory;
        }

        public void Dispose()
        {
        }

        public OpenTKControl CreateControl()
        {
            return new OpenTKControl(glContextFactory);
        }

        public void Destroy(OpenTKControl control)
        {
            control.Dispose();
        }
    }
}
