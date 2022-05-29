﻿namespace JSim.Core.Render
{
    /// <summary>
    /// Tracks the lighting within a Scene.
    /// </summary>
    public class SceneLighting
    {
        public SceneLighting()
        {
            ambientLight = new AmbientLight();
            lights = new List<ILight>();
        }

        public AmbientLight AmbientLight
        {
            get => ambientLight;
            set
            {
                ambientLight = value;
                LightingChanged?.Invoke(this, new LightingChangedEventArgs());
            }
        }

        /// <summary>
        /// Gets all of the lights in the scene.
        /// </summary>
        public IReadOnlyList<ILight> Lights =>
            lights;

        /// <summary>
        /// Adds a new light to the scene.
        /// </summary>
        /// <param name="light">Light object to add.</param>
        /// <returns>True if successful.</returns>
        public bool AddLight(ILight light)
        {
            if (!lights.Contains(light))
            {
                lights.Add(light);
                LightingChanged?.Invoke(this, new LightingChangedEventArgs());

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a light from the scene.
        /// </summary>
        /// <param name="light">Light to remove from the scene.</param>
        /// <returns>True if successfully removed.</returns>
        public bool RemoveLight(ILight light)
        {
            if (lights.Contains(light))
            {
                lights.Remove(light);
                LightingChanged?.Invoke(this, new LightingChangedEventArgs());

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// event fired when the scene lighting has changed.
        /// </summary>
        public event LightingChangedEventHandler? LightingChanged;

        /// <summary>
        /// Gets the default lighting scenario.
        /// </summary>
        public static SceneLighting Default
        {
            get
            {
                var lighting = new SceneLighting();

                lighting.AddLight(
                    new DirectionalLight(
                        new Maths.Vector3D(-1, -1, -1),
                        new Color(0.8f, 0.2f, 0.2f, 1.0f),
                        new Attenuation(1.0, 0.0, 0.0)
                    )
                );

                lighting.AddLight(
                    new PointLight(
                        new Maths.Vector3D(0, 0, 5),
                        new Color(1.0f, 0.2f, 0.8f, 0.2f),
                        new Attenuation(1.0, 0.0, 0.0)
                    )
                );

                lighting.AddLight(
                    new SpotLight(
                        new Maths.Vector3D(0, 0, 5),
                        new Maths.Vector3D(0, 0, -1),
                        new Color(1.0f, 0.2f, 0.2f, 0.8f),
                        new Attenuation(1.0, 0.0, 0.0),
                        8.0,
                        10.0
                    )
                );

                lighting.Lights[2].IsEnabled = false;

                return lighting;
            }
        }

        private AmbientLight ambientLight;
        private List<ILight> lights;
    }
}
