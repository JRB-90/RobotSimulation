using FluentAssertions;
using JSim.Core.Common;
using System.Collections.Generic;
using Xunit;

namespace SceneGraphTests.TreeHelpers
{
    public class ChildContainerTests
    {
        [Fact]
        public void Can_Standard_Child_Container_Pass_Tests()
        {
            ChildContainer<object> container = new ChildContainer<object>();

            RunStandardFunctionalityTests<object>(
                container,
                new object(),
                new object(),
                new object()
            );
        }

        private void RunStandardFunctionalityTests<T>(
            IChildContainer<T> childContainer,
            T item1,
            T item2,
            T item3)
        {
            childContainer.Children.Count.Should().Be(0);

            var monitor = childContainer.Monitor();
            childContainer.AttachChild(item1);
            childContainer.Children.Count.Should().Be(1);
            childContainer.Children.Should().Contain(item1);

            monitor.Should()
                .Raise(nameof(IChildContainer<T>.ChildAdded))
                .WithSender(childContainer)
                .WithArgs<ChildAddedEventArgs<T>>(args => args.Child.Equals(item1));

            monitor.Should()
                .Raise(nameof(IChildContainer<T>.ChildContainerModified))
                .WithSender(childContainer)
                .WithArgs<ChildContainerModifiedEventArgs>();

            monitor.Clear();

            childContainer.DetachChild(item1);
            childContainer.Children.Count.Should().Be(0);
            childContainer.Children.Should().NotContain(item1);

            monitor.Should()
                .Raise(nameof(IChildContainer<T>.ChildRemoved))
                .WithSender(childContainer)
                .WithArgs<ChildRemovedEventArgs<T>>(args => args.Child.Equals(item1));

            monitor.Should()
                .Raise(nameof(IChildContainer<T>.ChildContainerModified))
                .WithSender(childContainer)
                .WithArgs<ChildContainerModifiedEventArgs>();

            monitor.Clear();

            childContainer.AttachChild(item1);
            childContainer.AttachChild(item2);
            childContainer.AttachChild(item3);
            childContainer.Children.Count.Should().Be(3);
            childContainer.Children.Should().Contain(item1);
            childContainer.Children.Should().Contain(item2);
            childContainer.Children.Should().Contain(item3);

            var itemList = new List<T>() { item1, item2, item3 };
            
            foreach (var item in childContainer)
            {
                itemList.Should().Contain(item);
            }

            monitor.Clear();
            childContainer.ClearAllChildren();
            childContainer.Children.Count.Should().Be(0);
            childContainer.Children.Should().NotContain(item1);
            childContainer.Children.Should().NotContain(item2);
            childContainer.Children.Should().NotContain(item3);

            monitor.Should()
                .Raise(nameof(IChildContainer<T>.ChildContainerModified))
                .WithSender(childContainer)
                .WithArgs<ChildContainerModifiedEventArgs>();
        }
    }
}
