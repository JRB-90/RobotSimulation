using JSim.Avalonia.Models;
using ReactiveUI;
using System.ComponentModel;

namespace JSim.Avalonia.ViewModels
{
    internal class SceneObjectDataViewModel : ViewModelBase, ISceneObjectTypeDataVM
    {
        readonly SceneObjectModel sceneObject;

        public SceneObjectDataViewModel(SceneObjectModel sceneObject)
        {
            this.sceneObject = sceneObject;
            this.sceneObject.PropertyChanged += OnSceneObjectModelPropertyChanged;
            worldTransform = new Transform3DViewModel(this.sceneObject.WorldTransform);
            localTransform = new Transform3DViewModel(this.sceneObject.LocalTransform);
        }

        public string Name
        {
            get => sceneObject.Name;
            set => sceneObject.SceneObject.Name = value;
        }

        public string ID =>
            sceneObject.ID;

        public Transform3DViewModel WorldTransform
        {
            get => worldTransform;
            set => this.RaiseAndSetIfChanged(ref worldTransform, value, nameof(WorldTransform));
        }

        public Transform3DViewModel LocalTransform
        {
            get => localTransform;
            set => this.RaiseAndSetIfChanged(ref localTransform, value, nameof(LocalTransform));
        }

        private void OnSceneObjectModelPropertyChanged(
            object? sender, 
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SceneObjectModel.WorldTransform))
            {
                WorldTransform.Transform = sceneObject.WorldTransform;
                this.RaisePropertyChanged(nameof(WorldTransform));
                this.RaisePropertyChanged(nameof(LocalTransform));
            }

            if (e.PropertyName == nameof(SceneObjectModel.LocalTransform))
            {
                LocalTransform.Transform = sceneObject.LocalTransform;
                this.RaisePropertyChanged(nameof(WorldTransform));
                this.RaisePropertyChanged(nameof(LocalTransform));
            }
        }

        private Transform3DViewModel worldTransform;
        private Transform3DViewModel localTransform;
    }
}
