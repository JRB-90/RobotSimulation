namespace JSimControlGallery.GL
{
    /// <summary>
    /// Struct representing an OpenGL VBO.
    /// </summary>
    /// <remarks>
    /// VBO's are used to store vertex data on the GPU.
    /// Contains the vbo and element buffer locations and element size.
    /// </remarks>
    public struct Vbo { public int VboID, EboID, NumElements; }
}
