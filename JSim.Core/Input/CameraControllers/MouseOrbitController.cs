using JSim.Core.Maths;

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
            oldMousePos = Vector2D.Origin;
            deltaPos = Vector2D.Origin;

            mouse.MouseMoved += OnMouseMoved;
            mouse.MouseButtonDown += OnMouseButtonDown;
            mouse.MouseButtonUp += OnMouseButtonUp;
            mouse.MouseWheelMoved += OnMouseWheelMoved;
        }

        public MouseOrbitController(
            IMouseInputProvider mouse,
            Transform3D initialCameraPosition)
          : 
            base(initialCameraPosition)
        {
            this.mouse = mouse;
            oldMousePos = Vector2D.Origin;
            deltaPos = Vector2D.Origin;
        }

        protected override void OnParametersChanged()
        {
        }

        private void OnMouseMoved(object sender, MouseMovedEventArgs e)
        {
            if (orbitState == OrbitState.Orbiting ||
                orbitState == OrbitState.Panning)
            {
                deltaPos = e.NewPosition - oldMousePos;
                oldMousePos = e.NewPosition;
                
                if (orbitState == OrbitState.Orbiting)
                {
                    Rotate(-deltaPos.X, deltaPos.Y);
                }
                if (orbitState == OrbitState.Panning)
                {
                    Pan(deltaPos.X, deltaPos.Y);
                }
            }
        }

        private void OnMouseButtonDown(object sender, MouseButtonDownEventArgs e)
        {
            if (e.Button == MouseButton.Right &&
                orbitState == OrbitState.Idle)
            {
                orbitState = OrbitState.Orbiting;
                oldMousePos = e.Position;
            }
            else if (e.Button == MouseButton.Left &&
                     orbitState == OrbitState.Idle)
            {
                orbitState = OrbitState.Panning;
                oldMousePos = e.Position;
            }
        }

        private void OnMouseButtonUp(object sender, MouseButtonUpEventArgs e)
        {
            if (e.Button == MouseButton.Right &&
                orbitState == OrbitState.Orbiting)
            {
                orbitState = OrbitState.Idle;
            }
            else if (e.Button == MouseButton.Left &&
                     orbitState == OrbitState.Panning)
            {
                orbitState = OrbitState.Idle;
            }
        }

        private void OnMouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            ZoomExponential(e.WheelDelta);
        }

        private Vector2D oldMousePos;
        private Vector2D deltaPos;
    }
}
