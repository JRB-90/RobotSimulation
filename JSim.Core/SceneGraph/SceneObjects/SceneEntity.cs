﻿using JSim.Core.Common;
using JSim.Core.Render;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a Scene entity.
    /// </summary>
    public class SceneEntity : SceneObjectBase, ISceneEntity
    {
        public SceneEntity(
            INameRepository nameRepository,
            IMessageCollator collator,
            IGeometryContainer geometryContainer,
            ISceneAssembly parentAssembly,
            string nameRoot = "Entity")
          :
            base(
                nameRepository,
                collator,
                parentAssembly,
                nameRoot)
        {
            GeometryContainer = geometryContainer;
            GeometryContainer.Root.GeometryModified += Root_GeometryModified;
            SceneObjectMoved += SceneEntity_SceneObjectMoved;
        }

        public SceneEntity(
            INameRepository nameRepository,
            IMessageCollator collator,
            IGeometryContainer geometryContainer,
            Guid id,
            string name,
            ISceneAssembly parentAssembly)
          :
            base(
                nameRepository,
                collator,
                id,
                name,
                parentAssembly)
        {
            GeometryContainer = geometryContainer;
            GeometryContainer.Root.GeometryModified += Root_GeometryModified;
        }

        public IGeometryContainer GeometryContainer { get; }

        private void Root_GeometryModified(object sender, GeometryModifiedEventArgs e)
        {
            RaiseSceneObjectChangedEvent();
        }

        private void SceneEntity_SceneObjectMoved(object sender, SceneObjectMovedEventArgs e)
        {
            GeometryContainer.UpdateWorldPosition(WorldFrame);
        }
    }
}
