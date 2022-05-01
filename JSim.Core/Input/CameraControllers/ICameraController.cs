using JSim.Core.Render;

namespace JSim.Core.Input
{
    /// <summary>
    /// Defines the bahaviour of all camera controllers.
    /// Camera controllers are used to manipulate a camera in response to inputs.
    /// </summary>
    public interface ICameraController
    {
        /// <summary>
        /// The camera this controller is manipulating.
        /// </summary>
        ICamera? AttachedCamera { get; set; }
    }
}
