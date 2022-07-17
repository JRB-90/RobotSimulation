using JSim.Core.Maths;

namespace SceneGraphTests.TreeHelpers
{
    internal static class Utils
    {
        const double eps = 0.0000001;

        public static bool AreApproxTheSame(Transform3D t1, Transform3D t2)
        {
            return
                AreApproxTheSame(t1.Translation, t2.Translation) &&
                AreApproxTheSame(t1.Rotation, t2.Rotation);
        }

        public static bool AreApproxTheSame(Vector3D v1, Vector3D v2)
        {
            return
                AreApproxTheSame(v1.X, v2.X) &&
                AreApproxTheSame(v1.Y, v2.Y) &&
                AreApproxTheSame(v1.Z, v2.Z);
        }

        public static bool AreApproxTheSame(Rotation3D r1, Rotation3D r2)
        {
            var fix1 = r1.AsFixed();
            var fix2 = r2.AsFixed();

            return
                AreApproxTheSame(fix1.Rx, fix2.Rx) &&
                AreApproxTheSame(fix1.Ry, fix2.Ry) &&
                AreApproxTheSame(fix1.Rz, fix2.Rz);
        }

        public static bool AreApproxTheSame(double d1, double d2)
        {
            if (d1 < d2 + eps &&
                d1 > d2 - eps)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
