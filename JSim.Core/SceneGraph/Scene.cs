﻿namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation of a scene.
    /// </summary>
    public class Scene : IScene
    {
        readonly ISceneObjectCreator creator;

        public Scene(ISceneObjectCreator creator)
        {
            this.creator = creator;
            Name = "Scene";
            Root = creator.CreateSceneAssembly();
        }

        public string Name { get; set; }

        public ISceneAssembly Root { get; }
    }
}
