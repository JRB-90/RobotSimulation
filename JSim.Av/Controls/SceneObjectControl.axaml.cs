using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using JSim.Core.SceneGraph;

namespace JSim.Av.Controls
{
    public partial class SceneObjectControl : UserControl
    {
        readonly CheckBox isVisibleCheckBox;
        readonly CheckBox isHighlightedCheckBox;
        readonly TransformControl transformControl;

        public SceneObjectControl()
        {
            InitializeComponent();

            isVisibleCheckBox = this.FindControl<CheckBox>("IsVisibleCheckBox");
            isHighlightedCheckBox = this.FindControl<CheckBox>("IsHighlightedCheckBox");
            transformControl = this.FindControl<TransformControl>("TransformControl");
            isVisibleCheckBox.PropertyChanged += OnIsVisibleChanged;
            isHighlightedCheckBox.PropertyChanged += OnIsHighlightedChanged;
            transformControl.TransformUpdated += OnWorldTransformUpdated;

            UpdateDisplayedValues();
        }

        public static readonly DirectProperty<SceneObjectControl, ISceneObject?> SceneObjectProperty =
            AvaloniaProperty.RegisterDirect<SceneObjectControl, ISceneObject?>(
                nameof(SceneObject),
                o => o.SceneObject,
                (o, v) => o.SceneObject = v
            );

        public ISceneObject? SceneObject
        {
            get => sceneObject;
            set
            {
                SetAndRaise(SceneObjectProperty, ref sceneObject, value);
                UpdateDisplayedValues();
                if (sceneObject != null)
                {
                    sceneObject.SceneObjectModified += SceneObject_SceneObjectModified;
                }
            }
        }

        public static readonly DirectProperty<SceneObjectControl, string?> SceneObjectNameProperty =
            AvaloniaProperty.RegisterDirect<SceneObjectControl, string?>(
                nameof(SceneObjectName),
                o => o.SceneObjectName
            );

        public string? SceneObjectName =>
            SceneObject?.Name;

        public static readonly DirectProperty<SceneObjectControl, string?> SceneObjectIDProperty =
            AvaloniaProperty.RegisterDirect<SceneObjectControl, string?>(
                nameof(SceneObjectID),
                o => o.SceneObjectID
            );

        public string? SceneObjectID =>
            SceneObject?.ID.ToString();

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void UpdateDisplayedValues()
        {
            if (sceneObject != null)
            {
                isVisibleCheckBox.IsEnabled = true;
                //isVisibleCheckBox.IsChecked = sceneObject.IsVisible; TODO
                isHighlightedCheckBox.IsEnabled = true;
                //isHighlightedCheckBox.IsChecked = sceneObject.IsSelected; TODO

                transformControl.Transform = sceneObject.WorldFrame;
            }
            else
            {
                isVisibleCheckBox.IsEnabled = false;
                isVisibleCheckBox.IsChecked = false;
                isHighlightedCheckBox.IsChecked = false;
                isHighlightedCheckBox.IsEnabled = false;
            }

            RaisePropertyChanged(SceneObjectNameProperty, Optional<string?>.Empty, BindingValue<string?>.DoNothing);
            RaisePropertyChanged(SceneObjectIDProperty, Optional<string?>.Empty, BindingValue<string?>.DoNothing);
        }

        private void OnIsVisibleChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(CheckBox.IsChecked))
            {
                if (SceneObject != null &&
                    isVisibleCheckBox.IsChecked != null)
                {
                    // TODO
                    //SceneObject.IsVisible = isVisibleCheckBox.IsChecked.Value;
                }
            }
        }

        private void OnIsHighlightedChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(CheckBox.IsChecked))
            {
                if (SceneObject != null &&
                    isHighlightedCheckBox.IsChecked != null)
                {
                    // TODO
                    //SceneObject.IsHighlighted = isHighlightedCheckBox.IsChecked.Value;
                }
            }
        }

        private void SceneObject_SceneObjectModified(object sender, SceneObjectModifiedEventArgs e)
        {
            UpdateDisplayedValues();
        }

        private void OnWorldTransformUpdated(object sender, TransformUpdatedEventArgs e)
        {
            if (e.Transform != null &&
                SceneObject != null)
            {
                SceneObject.WorldFrame = e.Transform;
            }
        }

        private ISceneObject? sceneObject;
    }
}
