namespace JSim.OpenTK
{
    public interface IOpenTKControlFactory : IDisposable
    {
        OpenTKControl CreateControl();

        void Destroy(OpenTKControl control);
    }
}
