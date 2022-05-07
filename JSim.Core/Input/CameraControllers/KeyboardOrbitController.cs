using JSim.Core.Maths;

namespace JSim.Core.Input
{
    public class KeyboardOrbitController : OrbitControllerBase
    {
        readonly IKeyboardProvider keyboard;

        public KeyboardOrbitController(IKeyboardProvider keyboard)
          :
            base()
        {
            this.keyboard = keyboard;            
            keyboard.KeyDown += OnKeyDown;
            keyboard.KeyUp += OnKeyUp;

            right = false;
            left = false;
            up = false;
            down = false;
            forward = false;
            backward = false;

            timer = new Timer(new TimerCallback(Update), null, 0, (int)(1000.0 / 60));
        }

        public KeyboardOrbitController(
            IKeyboardProvider keyboard,
            Transform3D initialCameraPosition)
          :
            base(initialCameraPosition)
        {
            this.keyboard = keyboard;
            keyboard.KeyDown += OnKeyDown;
            keyboard.KeyUp += OnKeyUp;

            right = false;
            left = false;
            up = false;
            down = false;
            forward = false;
            backward = false;

            timer = new Timer(new TimerCallback(Update), null, 0, 50);
        }

        protected override void OnParametersChanged()
        {
            Update(null);
        }

        private void OnKeyDown(object sender, KeyDownEventArgs e)
        {
            lock (keyLock)
            {
                if (e.Key == Keys.D)
                {
                    right = true;
                }
                else if (e.Key == Keys.A)
                {
                    left = true;
                }
                else if (e.Key == Keys.W)
                {
                    up = true;
                }
                else if (e.Key == Keys.S)
                {
                    down = true;
                }
                else if (e.Key == Keys.Z)
                {
                    forward = true;
                }
                else if (e.Key == Keys.X)
                {
                    backward = true;
                }
            }
        }

        private void OnKeyUp(object sender, KeyUpEventArgs e)
        {
            lock (keyLock)
            {
                if (e.Key == Keys.D)
                {
                    right = false;
                }
                else if (e.Key == Keys.A)
                {
                    left = false;
                }
                else if (e.Key == Keys.W)
                {
                    up = false;
                }
                else if (e.Key == Keys.S)
                {
                    down = false;
                }
                else if (e.Key == Keys.Z)
                {
                    forward = false;
                }
                else if (e.Key == Keys.X)
                {
                    backward = false;
                }
            }
        }

        private void Update(object? state)
        {
            lock (keyLock)
            {
                if (right || left || up || down)
                {
                    orbitState = OrbitState.Orbiting;
                }
                else
                {
                    orbitState = OrbitState.Idle;
                }

                double zoom =
                    (forward ? 1 : 0) +
                    (backward ? -1 : 0);

                ZoomExponential(zoom);

                double horizontal =
                    (right ? -1 : 0) +
                    (left ? 1 : 0);

                double vertical =
                    (up ? -1 : 0) +
                    (down ? 1 : 0);

                Rotate(horizontal * 5, vertical * 5);
            }
        }

        private bool right;
        private bool left;
        private bool up;
        private bool down;
        private bool forward;
        private bool backward;
        private Timer timer;

        private static object keyLock = new object();
    }
}
