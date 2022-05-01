using JSim.Core.Render;

namespace JSim.Core.Input
{
    /// <summary>
    /// Provides an implementation of an orbitting camera using a mouse as the input.
    /// </summary>
    public class MouseOrbitController : OrbitControllerBase
    {
        readonly IMouseInputProvider mouse;

        public MouseOrbitController(IMouseInputProvider mouse)
          :
            base()
        {
            this.mouse = mouse;
            mouse.MouseWheelMoved += OnMouseMoved;
            mouse.MouseButtonPressed += OnMouseButtonPressed;
            mouse.MouseButtonReleased += OnMouseButtonReleased;
            mouse.MouseWheelMoved += OnMouseWheelMoved;
        }

        public MouseOrbitController(
            IMouseInputProvider mouse,
            ICamera? camera)
          : 
            base(camera)
        {
            this.mouse = mouse;
        }

        protected override void OnCameraChanged(ICamera? camera)
        {
            CalculateCameraPosition();
        }

        protected override void OnParametersChanged()
        {
            CalculateCameraPosition();
        }

        private void OnMouseMoved(object sender, MouseWheelEventArgs e)
        {
            
        }

        private void OnMouseButtonPressed(object sender, MouseButtonPressedEventArgs e)
        {
            
        }

        private void OnMouseButtonReleased(object sender, MouseButtonReleasedEventArgs e)
        {
            
        }

        private void OnMouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            
        }

        private void CalculateCameraPosition()
        {

        }
    }
}
