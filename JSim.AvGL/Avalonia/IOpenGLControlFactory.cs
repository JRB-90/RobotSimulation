namespace JSim.AvGL
{
    public interface IOpenGLControlFactory : IDisposable
    {
        OpenGLControl CreateControl();

        void Destroy(OpenGLControl control);
    }
}
