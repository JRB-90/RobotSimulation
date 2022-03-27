using FluentAssertions;
using JSim.Core.Maths;
using Xunit;

namespace SceneGraphTests
{
    public class MathTests
    {
        [Fact]
        public void CanConstructVector3D()
        {
            Vector3D vector = new Vector3D();
        }

        [Fact]
        public void DoVector3DConstructorsInitCorrectly()
        {
            Vector3D v1 = new Vector3D();
            v1.X.Should().Be(0.0);
            v1.Y.Should().Be(0.0);
            v1.Z.Should().Be(0.0);

            Vector3D v2 = Vector3D.Origin;
            v2.X.Should().Be(0.0);
            v2.Y.Should().Be(0.0);
            v2.Z.Should().Be(0.0);

            Vector3D v3 = new Vector3D(1.0, 2.0, 3.0);
            v3.X.Should().Be(1.0);
            v3.Y.Should().Be(2.0);
            v3.Z.Should().Be(3.0);

            Vector3D v4 = new Vector3D(-1.0, -2.0, -3.0);
            v4.X.Should().Be(-1.0);
            v4.Y.Should().Be(-2.0);
            v4.Z.Should().Be(-3.0);

            Vector3D v5 = new Vector3D(v3);
            v5.X.Should().Be(v3.X);
            v5.Y.Should().Be(v3.Y);
            v5.Z.Should().Be(v3.Z);

            Vector3D v6 = new Vector3D();
            v6.X = 6.0;
            v6.X.Should().Be(6.0);
            v6.Y = 5.0;
            v6.Y.Should().Be(5.0);
            v6.Z = 4.0;
            v6.Z.Should().Be(4.0);

            Vector3D v7 = new Vector3D(v4.Vector);
            v7.X.Should().Be(v4.X);
            v7.Y.Should().Be(v4.Y);
            v7.Z.Should().Be(v4.Z);
        }

        [Fact]
        public void DoVector3DOperationsFunctionCorrectly()
        {
            Vector3D v1 = new Vector3D(1.0, 2.0, 3.0);
            Vector3D v2 = new Vector3D(2.0, 4.0, 6.0);

            Vector3D res1 = v1 + v2;
            res1.X.Should().BeApproximately(3.0, double.Epsilon);
            res1.Y.Should().BeApproximately(6.0, double.Epsilon);
            res1.Z.Should().BeApproximately(9.0, double.Epsilon);

            Vector3D res2 = v1 - v2;
            res2.X.Should().BeApproximately(-1.0, double.Epsilon);
            res2.Y.Should().BeApproximately(-2.0, double.Epsilon);
            res2.Z.Should().BeApproximately(-3.0, double.Epsilon);

            Vector3D res3 = v1.Cross(v2);
            res3.X.Should().BeApproximately(0.0, double.Epsilon);
            res3.Y.Should().BeApproximately(0.0, double.Epsilon);
            res3.Z.Should().BeApproximately(0.0, double.Epsilon);

            double res4 = v1.Dot(v2);
            res4.Should().BeApproximately(28.0, double.Epsilon);
        }

        [Fact]
        public void CanConstructRotation3D()
        {
            Rotation3D rotation = new Rotation3D();
            FixedRotation3D fixedRotation = new FixedRotation3D();
            AxisAngleRotation3D axisAngleRotation3D = new AxisAngleRotation3D();
        }

        [Fact]
        public void DoRotation3DConstructorsInitCorrectly()
        {
            Rotation3D r1 = new Rotation3D();
            r1.Matrix[0, 0].Should().Be(1.0);
            r1.Matrix[0, 1].Should().Be(0.0);
            r1.Matrix[0, 2].Should().Be(0.0);
            r1.Matrix[1, 0].Should().Be(0.0);
            r1.Matrix[1, 1].Should().Be(1.0);
            r1.Matrix[1, 2].Should().Be(0.0);
            r1.Matrix[2, 0].Should().Be(0.0);
            r1.Matrix[2, 1].Should().Be(0.0);
            r1.Matrix[2, 2].Should().Be(1.0);

            FixedRotation3D r2 = new FixedRotation3D();
            r2.Rx.Should().Be(0.0);
            r2.Ry.Should().Be(0.0);
            r2.Rz.Should().Be(0.0);
            r2.Matrix[0, 0].Should().Be(1.0);
            r2.Matrix[0, 1].Should().Be(0.0);
            r2.Matrix[0, 2].Should().Be(0.0);
            r2.Matrix[1, 0].Should().Be(0.0);
            r2.Matrix[1, 1].Should().Be(1.0);
            r2.Matrix[1, 2].Should().Be(0.0);
            r2.Matrix[2, 0].Should().Be(0.0);
            r2.Matrix[2, 1].Should().Be(0.0);
            r2.Matrix[2, 2].Should().Be(1.0);

            FixedRotation3D r3 = new FixedRotation3D(1.0, 2.0, 3.0);
            r3.Rx.Should().Be(1.0);
            r3.Ry.Should().Be(2.0);
            r3.Rz.Should().Be(3.0);

            AxisAngleRotation3D r4 = new AxisAngleRotation3D(new Vector3D(1.0, 2.0, 3.0), -4.0);
            r4.Axis.X.Should().BeApproximately(1.0, double.Epsilon);
            r4.Axis.Y.Should().BeApproximately(2.0, double.Epsilon);
            r4.Axis.Z.Should().BeApproximately(3.0, double.Epsilon);
            r4.Angle.Should().BeApproximately(-4.0, double.Epsilon);
        }

        [Fact]
        public void DoRotation3DOperationsFunctionCorrectly()
        {
            FixedRotation3D r1 = new FixedRotation3D(10.0, 20.0, 30.0);
            FixedRotation3D r2 = new FixedRotation3D(r1.Matrix);

            r2.Rx.Should().BeApproximately(r1.Rx, 0.00001);
            r2.Ry.Should().BeApproximately(r1.Ry, 0.00001);
            r2.Rz.Should().BeApproximately(r1.Rz, 0.00001);

            FixedRotation3D r3 = new FixedRotation3D(new Rotation3D(r1.Matrix));
            r3.Rx.Should().BeApproximately(r1.Rx, 0.00001);
            r3.Ry.Should().BeApproximately(r1.Ry, 0.00001);
            r3.Rz.Should().BeApproximately(r1.Rz, 0.00001);

            FixedRotation3D r4 = new FixedRotation3D(Rotation3D.Identity * r1);
            r4.Rx.Should().BeApproximately(r1.Rx, 0.00001);
            r4.Ry.Should().BeApproximately(r1.Ry, 0.00001);
            r4.Rz.Should().BeApproximately(r1.Rz, 0.00001);

            FixedRotation3D r5 = new FixedRotation3D(Rotation3D.Identity * r1 * r1.Inverse);
            r5.Rx.Should().BeApproximately(0.0, 0.00001);
            r5.Ry.Should().BeApproximately(0.0, 0.00001);
            r5.Rz.Should().BeApproximately(0.0, 0.00001);

            AxisAngleRotation3D r6 = new AxisAngleRotation3D(new Vector3D(30.0, 20.0, 10.0), -45.0);
            AxisAngleRotation3D r7 = new AxisAngleRotation3D(Rotation3D.Identity * r6 * r6.Inverse);
            r7.Axis.X.Should().BeApproximately(1.0, 0.0001);
            r7.Axis.Y.Should().BeApproximately(0.0, 0.0001);
            r7.Axis.Z.Should().BeApproximately(0.0, 0.0001);
            r7.Angle.Should().BeApproximately(0.0, 0.0001);
        }

        [Fact]
        public void CanConstructTransform3D()
        {
            Transform3D transform = new Transform3D();
        }

        [Fact]
        public void DoTransform3DConstructorsInitCorrectly()
        {
            Transform3D t1 = new Transform3D();
            t1.Translation.X.Should().BeApproximately(0.0, double.Epsilon);
            t1.Translation.Y.Should().BeApproximately(0.0, double.Epsilon);
            t1.Translation.Z.Should().BeApproximately(0.0, double.Epsilon);
            t1.Rotation.Matrix[0, 0].Should().BeApproximately(1.0, double.Epsilon);
            t1.Rotation.Matrix[0, 1].Should().BeApproximately(0.0, double.Epsilon);
            t1.Rotation.Matrix[0, 2].Should().BeApproximately(0.0, double.Epsilon);
            t1.Rotation.Matrix[1, 0].Should().BeApproximately(0.0, double.Epsilon);
            t1.Rotation.Matrix[1, 1].Should().BeApproximately(1.0, double.Epsilon);
            t1.Rotation.Matrix[1, 2].Should().BeApproximately(0.0, double.Epsilon);
            t1.Rotation.Matrix[2, 0].Should().BeApproximately(0.0, double.Epsilon);
            t1.Rotation.Matrix[2, 1].Should().BeApproximately(0.0, double.Epsilon);
            t1.Rotation.Matrix[2, 2].Should().BeApproximately(1.0, double.Epsilon);

            Transform3D t2 = 
                new Transform3D(
                    new Vector3D(1.0, 2.0, 3.0), 
                    new FixedRotation3D(10.0, 20.0, 30.0)
                );

            Transform3D t3 = new Transform3D() * t2;
            t3.Translation.X.Should().BeApproximately(1.0, 0.0001);
            t3.Translation.Y.Should().BeApproximately(2.0, 0.0001);
            t3.Translation.Z.Should().BeApproximately(3.0, 0.0001);

            FixedRotation3D r1 = new FixedRotation3D(t3.Rotation);
            r1.Rx.Should().BeApproximately(10.0, 0.0001);
            r1.Ry.Should().BeApproximately(20.0, 0.0001);
            r1.Rz.Should().BeApproximately(30.0, 0.0001);
        }

        [Fact]
        public void DoTransform3DOperationsFunctionCorrectly()
        {
            Transform3D t1 = new Transform3D() * new Vector3D(1.0, 2.0, 3.0);
            t1.Translation.X.Should().BeApproximately(1.0, 0.0001);
            t1.Translation.Y.Should().BeApproximately(2.0, 0.0001);
            t1.Translation.Z.Should().BeApproximately(3.0, 0.0001);

            Transform3D t2 = new Transform3D() * new FixedRotation3D(10.0, 20.0, 30.0);
            FixedRotation3D r1 = new FixedRotation3D(t2.Rotation);
            r1.Rx.Should().BeApproximately(10.0, 0.0001);
            r1.Ry.Should().BeApproximately(20.0, 0.0001);
            r1.Rz.Should().BeApproximately(30.0, 0.0001);

            Transform3D t3 = t1 * t2;
            t3.Translation.X.Should().BeApproximately(1.0, 0.0001);
            t3.Translation.Y.Should().BeApproximately(2.0, 0.0001);
            t3.Translation.Z.Should().BeApproximately(3.0, 0.0001);
            FixedRotation3D r2 = new FixedRotation3D(t3.Rotation);
            r2.Rx.Should().BeApproximately(10.0, 0.0001);
            r2.Ry.Should().BeApproximately(20.0, 0.0001);
            r2.Rz.Should().BeApproximately(30.0, 0.0001);

            Transform3D t4 = new Transform3D() * t3 * t3.Inverse;
            t4.Translation.X.Should().BeApproximately(0.0, 0.0001);
            t4.Translation.Y.Should().BeApproximately(0.0, 0.0001);
            t4.Translation.Z.Should().BeApproximately(0.0, 0.0001);
            t4.Rotation.Matrix[0, 0].Should().BeApproximately(1.0, 0.0001);
            t4.Rotation.Matrix[0, 1].Should().BeApproximately(0.0, 0.0001);
            t4.Rotation.Matrix[0, 2].Should().BeApproximately(0.0, 0.0001);
            t4.Rotation.Matrix[1, 0].Should().BeApproximately(0.0, 0.0001);
            t4.Rotation.Matrix[1, 1].Should().BeApproximately(1.0, 0.0001);
            t4.Rotation.Matrix[1, 2].Should().BeApproximately(0.0, 0.0001);
            t4.Rotation.Matrix[2, 0].Should().BeApproximately(0.0, 0.0001);
            t4.Rotation.Matrix[2, 1].Should().BeApproximately(0.0, 0.0001);
            t4.Rotation.Matrix[2, 2].Should().BeApproximately(1.0, 0.0001);
        }
    }
}
