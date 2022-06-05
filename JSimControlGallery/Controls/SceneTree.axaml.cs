using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JSim.Core.SceneGraph;
using JSimControlGallery.Models;
using JSimControlGallery.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace JSimControlGallery.Controls
{
    public partial class SceneTree : UserControl
    {
        readonly TreeView treeView;
        readonly DragAndDropHandler<SceneObjectModel> dragDrop;

        public SceneTree()
        {
            InitializeComponent();

            treeView = this.FindControl<TreeView>("SceneTreeView");
            treeView.SelectionChanged += OnSceneObjectSelectionChanged;

            dragDrop =
                new DragAndDropHandler<SceneObjectModel>(
                    treeView,
                    a => CanDrag(a),
                    (a, o) => CanDrop(a, o),
                    (a, o) => Drop(a, o)
                );

            selectedObjects = new Subject<IReadOnlyCollection<ISceneObject>>();
            SceneRoot = new ObservableCollection<SceneAssemblyModel>();
        }

        public static readonly DirectProperty<SceneTree, IScene?> SceneProperty =
            AvaloniaProperty.RegisterDirect<SceneTree, IScene?>(
                nameof(Scene),
                o => o.Scene,
                (o, v) => o.Scene = v
            );

        public IScene? Scene
        {
            get => scene;
            set
            {
                SetAndRaise(SceneProperty, ref scene, value);
                SceneRoot.Clear();

                if (scene != null)
                {
                    SceneRoot.Add(new SceneAssemblyModel(scene.Root));
                    treeView.Items = SceneRoot;
                }
                else
                {
                    treeView.Items = null;
                }
            }
        }

        public IObservable<IReadOnlyCollection<ISceneObject>> SelectedObjects =>
            selectedObjects.AsObservable();

        internal ObservableCollection<SceneAssemblyModel> SceneRoot { get; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnSceneObjectSelectionChanged(
            object? sender, 
            Avalonia.Controls.SelectionChangedEventArgs e)
        {
            var treeSelectedObjects = new List<ISceneObject>();

            foreach (var item in treeView.SelectedItems)
            {
                if (item is SceneObjectModel sceneObjectModel)
                {
                    treeSelectedObjects.Add(sceneObjectModel.SceneObject);
                }
            }

            selectedObjects.OnNext(treeSelectedObjects);
        }

        private bool CanDrag(SceneObjectModel dragged)
        {
            return dragged.SceneObject.ParentAssembly != null;
        }

        private bool CanDrop(
            SceneObjectModel dragged,
            SceneObjectModel dropped)
        {
            if (dropped is SceneAssemblyModel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Drop(
            SceneObjectModel dragged,
            SceneObjectModel dropped)
        {
            if (dropped is SceneAssemblyModel assemblyModel)
            {
                dragged.MoveAssembly(assemblyModel);

                return true;
            }
            else
            {
                return false;
            }
        }

        private IScene? scene;
        private Subject<IReadOnlyCollection<ISceneObject>> selectedObjects;
    }
}
