﻿using JSim.Core.Maths;

namespace JSim.Core.Render
{
    /// <summary>
    /// Standard implementation of a geometry container.
    /// </summary>
    public class GeometryContainer : IGeometryContainer
    {
        readonly IGeometryCreator creator;

        public GeometryContainer(IGeometryCreatorFactory geometryCreatorFactory)
        {
            creator = geometryCreatorFactory.CreateGeometryCreator();
            Root = creator.CreateGeometry(null);
        }

        public IGeometry Root { get; }

        public void UpdateWorldPosition(Transform3D worldPositionOfParent)
        {
            Root.WorldFrame = worldPositionOfParent;
        }
    }
}
