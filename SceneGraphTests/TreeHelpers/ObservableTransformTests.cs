using FluentAssertions;
using JSim.Core.Common;
using JSim.Core.Maths;
using Xunit;

namespace SceneGraphTests.TreeHelpers
{
    public class ObservableTransformTests
    {
        const double eps = 0.0000001;

        [Fact]
        public void Can_Construct()
        {
            var observer = new ObservableTransform();
            observer.X.Should().BeApproximately(0.0, eps);
            observer.Y.Should().BeApproximately(0.0, eps);
            observer.Z.Should().BeApproximately(0.0, eps);
            observer.Rx.Should().BeApproximately(0.0, eps);
            observer.Ry.Should().BeApproximately(0.0, eps);
            observer.Rz.Should().BeApproximately(0.0, eps);

            var t1 = new Transform3D(1.0, 2.0, 3.0, 4.0, 5.0, 6.0);

            observer = new ObservableTransform(t1);
            observer.X.Should().BeApproximately(1.0, eps);
            observer.Y.Should().BeApproximately(2.0, eps);
            observer.Z.Should().BeApproximately(3.0, eps);
            observer.Rx.Should().BeApproximately(4.0, eps);
            observer.Ry.Should().BeApproximately(5.0, eps);
            observer.Rz.Should().BeApproximately(6.0, eps);
        }

        [Fact]
        public void Does_Raise_Changed_Events()
        {
            var observer = new ObservableTransform();
            var monitor = observer.Monitor();

            observer.X = 1.0;
            observer.X.Should().BeApproximately(1.0, eps);
            monitor.Should()
                .Raise(nameof(ObservableTransform.TransformModified))
                .WithSender(observer)
                .WithArgs<TransformModifiedEventArgs>(t => Utils.AreApproxTheSame(t.ModifiedTransform, observer.GetTransformCopy()));
            monitor.Clear();

            observer.Y = 2.0;
            observer.Y.Should().BeApproximately(2.0, eps);
            monitor.Should()
                .Raise(nameof(ObservableTransform.TransformModified))
                .WithSender(observer)
                .WithArgs<TransformModifiedEventArgs>(t => Utils.AreApproxTheSame(t.ModifiedTransform, observer.GetTransformCopy()));
            monitor.Clear();

            observer.Z = 3.0;
            observer.Z.Should().BeApproximately(3.0, eps);
            monitor.Should()
                .Raise(nameof(ObservableTransform.TransformModified))
                .WithSender(observer)
                .WithArgs<TransformModifiedEventArgs>(t => Utils.AreApproxTheSame(t.ModifiedTransform, observer.GetTransformCopy()));
            monitor.Clear();

            observer.Rx = 4.0;
            observer.Rx.Should().BeApproximately(4.0, eps);
            monitor.Should()
                .Raise(nameof(ObservableTransform.TransformModified))
                .WithSender(observer)
                .WithArgs<TransformModifiedEventArgs>(t => Utils.AreApproxTheSame(t.ModifiedTransform, observer.GetTransformCopy()));
            monitor.Clear();

            observer.Ry = 5.0;
            observer.Ry.Should().BeApproximately(5.0, eps);
            monitor.Should()
                .Raise(nameof(ObservableTransform.TransformModified))
                .WithSender(observer)
                .WithArgs<TransformModifiedEventArgs>(t => Utils.AreApproxTheSame(t.ModifiedTransform, observer.GetTransformCopy()));
            monitor.Clear();

            observer.Rz = 6.0;
            observer.Rz.Should().BeApproximately(6.0, eps);
            monitor.Should()
                .Raise(nameof(ObservableTransform.TransformModified))
                .WithSender(observer)
                .WithArgs<TransformModifiedEventArgs>(t => Utils.AreApproxTheSame(t.ModifiedTransform, observer.GetTransformCopy()));
            monitor.Clear();

            var t2 = new Transform3D(-12.343, 49.2312, 142.2, -2, -92.234, 0.0042);
            observer.SetTransform(t2);
            Utils.AreApproxTheSame(t2, observer.GetTransformCopy()).Should().BeTrue();
            monitor.Should()
                .Raise(nameof(ObservableTransform.TransformModified))
                .WithSender(observer)
                .WithArgs<TransformModifiedEventArgs>(t => Utils.AreApproxTheSame(t.ModifiedTransform, t2));
            monitor.Clear();
        }
    }
}
