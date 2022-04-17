using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSim.Avalonia.Models
{
    internal abstract class SceneObjectModelBase : ReactiveObject
    {
        public SceneObjectModelBase(
            ISceneObject sceneObject,
            string iconName)
        {
            SceneObject = sceneObject;
            IconName = iconName;
            worldTransform = new TransformModel(sceneObject.WorldFrame);
            localTransform = new TransformModel(sceneObject.LocalFrame);
            worldTransform.TransformModified += OnWorldTransformModified;
            localTransform.TransformModified += OnLocalTransformModified;
            sceneObject.SceneObjectModified += OnSceneObjectModified;
        }

        public ISceneObject SceneObject { get; }

        public string IconName { get; }

        public string Name =>
            SceneObject.Name;

        public string ID =>
            SceneObject.ID.ToString();

        public TransformModel WorldTransform
        {
            get => worldTransform;
            set
            {
                if (this.RaiseAndSetIfChanged(ref worldTransform, value, nameof(WorldTransform)) != null)
                {
                    worldTransform.TransformModified += OnWorldTransformModified;
                }
            }
        }

        public TransformModel LocalTransform
        {
            get => localTransform;
            set
            {
                if (this.RaiseAndSetIfChanged(ref localTransform, value, nameof(LocalTransform)) != null)
                {
                    localTransform.TransformModified += OnLocalTransformModified;
                }
            }
        }

        public void Remove()
        {
            SceneObject.ParentAssembly?.DetachObject(SceneObject);
        }

        public void Move(ISceneAssembly assembly)
        {
            SceneObject.MoveAssembly(assembly);
        }

        private void OnWorldTransformModified(object sender, Events.TransformModifiedEventArgs e)
        {
            SceneObject.WorldFrame = WorldTransform.Transform;
            RefreshTransformModels();
        }

        private void OnLocalTransformModified(object sender, Events.TransformModifiedEventArgs e)
        {
            SceneObject.LocalFrame = LocalTransform.Transform;
            RefreshTransformModels();
        }

        private void OnSceneObjectModified(object sender, SceneObjectModifiedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Name));
            RefreshTransformModels();
        }

        private void RefreshTransformModels()
        {
            WorldTransform = new TransformModel(SceneObject.WorldFrame);
            LocalTransform = new TransformModel(SceneObject.LocalFrame);
        }

        private TransformModel worldTransform;
        private TransformModel localTransform;
    }
}
