﻿using JSim.Core.Display;
using JSim.Core.Maths;
using MathNet.Numerics.LinearAlgebra;

namespace JSim.Core.Render
{
    /// <summary>
    /// Interface defining the behaviour of cameras.
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// Gets and sets the position of the camera in the world.
        /// </summary>
        Transform3D PositionInWorld { get; set; }

        /// <summary>
        /// Gets and sets the camera projection.
        /// </summary>
        ICameraProjection CameraProjection { get; set; }

        /// <summary>
        /// Event fired when the camera parameters have changed.
        /// </summary>
        event CameraModifiedEventHandler? CameraModified;

        /// <summary>
        /// Gets the camera projection matrix.
        /// </summary>
        /// <returns>Camera projection matrix.</returns>
        Matrix<double> GetProjectionMatrix();

        /// <summary>
        /// Gets the camera view matrix.
        /// </summary>
        /// <returns>Camera view matrix.</returns>
        Matrix<double> GetViewMatrix();

        /// <summary>
        /// Updates the camera with information about the rendering surface.
        /// </summary>
        /// <param name="surface">Rendering surface to be used to update the camera.</param>
        void Update(IRenderingSurface surface);
    }
}