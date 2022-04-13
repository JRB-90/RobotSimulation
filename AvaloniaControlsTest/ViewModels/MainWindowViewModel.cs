using JSim.Avalonia.Models;
using JSim.Avalonia.ViewModels;
using JSim.Core.Maths;

namespace AvaloniaControlsTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var transform =
                new Transform3D(
                    1.1111, -202.01223, 891234.87632,
                    0.999999, -123.02434, -122.64
                );

            transformModel = new TransformModel(transform);
            transformModel.TransformModified += TransformModel_TransformModified;
            TransformVM = new Transform3DViewModel(transformModel);
        }

        public Transform3DViewModel TransformVM { get; }

        private void TransformModel_TransformModified(object? sender, System.EventArgs e)
        {
            if (transformModel.Transform.Translation.X == 55.0)
            {
                transformModel.Transform =
                    new Transform3D(
                        1.1111, -202.01223, 891234.87632,
                        0.999999, -123.02434, -122.64
                    );
            }
        }

        private TransformModel transformModel;
    }
}
