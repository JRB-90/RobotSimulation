using JSim.Avalonia.Models;
using ReactiveUI;

namespace JSim.Avalonia.ViewModels
{
    public class Transform3DViewModel : ViewModelBase
    {
        public Transform3DViewModel(TransformModel transform)
        {
            Transform = transform;
            Transform.TransformModified += OnTransformModified;
        }

        public TransformModel Transform { get; }

        public double X
        {
            get => Transform.X;
            set => Transform.X = value;
        }

        public double Y
        {
            get => Transform.Y;
            set => Transform.Y = value;
        }

        public double Z
        {
            get => Transform.Z;
            set => Transform.Z = value;
        }

        public double Rx
        {
            get => Transform.Rx;
            set => Transform.Rx = value;
        }

        public double Ry
        {
            get => Transform.Ry;
            set => Transform.Ry = value;
        }

        public double Rz
        {
            get => Transform.Rz;
            set => Transform.Rz = value;
        }

        private void RefreshValues()
        {
            this.RaisePropertyChanged(nameof(Transform));
            this.RaisePropertyChanged(nameof(X));
            this.RaisePropertyChanged(nameof(Y));
            this.RaisePropertyChanged(nameof(Z));
            this.RaisePropertyChanged(nameof(Rx));
            this.RaisePropertyChanged(nameof(Ry));
            this.RaisePropertyChanged(nameof(Rz));
        }

        private void OnTransformModified(object? sender, EventArgs e)
        {
            RefreshValues();
        }
    }
}
