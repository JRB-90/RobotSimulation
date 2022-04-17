using JSim.Avalonia.Models;
using JSim.Core.SceneGraph;
using ReactiveUI;

namespace JSim.Avalonia.ViewModels
{
    internal class SceneObjectDataViewModel : ViewModelBase, ISceneObjectTypeDataVM
    {
        readonly ISceneObject sceneObject;

        public SceneObjectDataViewModel(ISceneObject sceneObject)
        {
            this.sceneObject = sceneObject;
            sceneObject.SceneObjectModified += OnSceneObjectModified;
            sceneObject.SceneObjectMoved += OnSceneObjectMoved;

            worldFrame = new TransformModel(sceneObject.WorldFrame);
            worldFrame.TransformModified += OnWorldFrameModified;
            WorldTransform = new Transform3DViewModel(worldFrame);

            localFrame = new TransformModel(sceneObject.LocalFrame);
            localFrame.TransformModified += OnLocalFrameModified;
            LocalTransform = new Transform3DViewModel(localFrame);
        }

        public string Name
        {
            get => sceneObject.Name;
            set => sceneObject.Name = value;
        }

        public string ID =>
            sceneObject.ID.ToString();

        public Transform3DViewModel WorldTransform { get; }

        public Transform3DViewModel LocalTransform { get; }

        private void OnSceneObjectModified(object sender, SceneObjectModifiedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Name));
        }

        private void OnSceneObjectMoved(object sender, SceneObjectMovedEventArgs e)
        {
            
        }

        private void OnLocalFrameModified(object sender, Events.TransformModifiedEventArgs e)
        {
            
        }

        private void OnWorldFrameModified(object sender, Events.TransformModifiedEventArgs e)
        {
            
        }

        private void RefreshTransformViews()
        {

        }

        private TransformModel worldFrame;
        private TransformModel localFrame;
    }
}
