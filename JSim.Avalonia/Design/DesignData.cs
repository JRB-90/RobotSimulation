using JSim.Avalonia.Models;
using JSim.Avalonia.ViewModels;
using JSim.Core.Maths;

namespace JSim.Avalonia.Design
{
    internal static class DesignData
    {
        static DesignData()
        {
            Transform =
                new Transform3D(
                    1.1111, -202.01223, 891234.87632,
                    0.999999, -123.02434, -122.64
                );

            TransformModel = new TransformModel(Transform);
            Transform3DVM = new Transform3DViewModel(TransformModel);
        }

        public static Transform3D Transform { get; }

        public static TransformModel TransformModel { get; }

        public static Transform3DViewModel Transform3DVM { get; }
    }
}
