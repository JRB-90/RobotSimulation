using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace JSim.Av.Shared
{
    internal class DragAndDropHandler<T> where T : class
    {
        readonly Control control;

        public DragAndDropHandler(
            Control control,
            Func<T, bool> canDrag,
            Func<T, T, bool> canDrop,
            Func<T, T, bool> dropAction)
        {
            this.control = control;
            this.canDrag = canDrag;
            this.canDrop = canDrop;
            this.dropAction = dropAction;

            control.AddHandler(
                Control.PointerPressedEvent,
                OnMousePressed,
                RoutingStrategies.Tunnel
            );

            control.AddHandler(
                Control.PointerReleasedEvent,
                OnMouseReleased,
                RoutingStrategies.Tunnel
            );

            control.AddHandler(
                Control.PointerMovedEvent,
                OnMouseMoved,
                RoutingStrategies.Tunnel
            );

            control.AddHandler(
                Control.PointerCaptureLostEvent,
                OnMouseLost,
                RoutingStrategies.Tunnel
            );
        }

        private void OnMousePressed(
            object? sender,
            PointerPressedEventArgs e)
        {
            if (e.Source is Control control &&
                control.DataContext is T draggedData &&
                canDrag(draggedData))
            {
                isDragging = true;
                draggedObject = draggedData;
            }
        }

        private void OnMouseReleased(
            object? sender,
            PointerReleasedEventArgs e)
        {
            if (e.Source is Control draggedControl &&
                draggedControl.DataContext is T draggedData)
            {
                var pos = e.GetPosition(control);
                var elements = control.GetInputElementsAt(pos).ToList();

                if (elements.FirstOrDefault() is Control droppedControl &&
                    droppedControl.DataContext is T droppedObject &&
                    draggedObject != null &&
                    droppedObject != draggedObject)
                {
                    if (canDrop(draggedObject, droppedObject))
                    {
                        dropAction(draggedObject, droppedObject);
                    }
                }

                isDragging = false;
                draggedObject = null;
                control.Cursor = Cursor.Default;
            }
        }

        private void OnMouseMoved(
            object? sender,
            PointerEventArgs e)
        {
            if (isDragging &&
                e.Source is Control draggedControl &&
                draggedControl.DataContext is T draggedData)
            {
                var pos = e.GetPosition(control);
                var elements = control.GetInputElementsAt(pos).ToList();

                if (elements.FirstOrDefault() is Control droppedControl &&
                    droppedControl.DataContext is T droppedObject &&
                    draggedObject != null &&
                    droppedObject != draggedObject)
                {
                    if (canDrop(draggedObject, droppedObject))
                    {
                        control.Cursor = new Cursor(StandardCursorType.DragMove);
                    }
                    else
                    {
                        control.Cursor = new Cursor(StandardCursorType.No);
                    }
                }
                else
                {
                    control.Cursor = new Cursor(StandardCursorType.No);
                }
            }
        }

        private void OnMouseLost(object? sender, PointerCaptureLostEventArgs e)
        {
            isDragging = false;
            draggedObject = null;
            control.Cursor = Cursor.Default;
        }

        private bool isDragging;
        private T? draggedObject;
        private Func<T, bool> canDrag;
        private Func<T, T, bool> canDrop;
        private Func<T, T, bool> dropAction;
    }
}
