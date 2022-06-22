using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JSim.Av.Models;
using JSim.Av.Shared;
using JSim.Core.Render;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace JSim.Av.Controls
{
    public partial class GeometryTree : UserControl
    {
        readonly TreeView treeView;
        readonly DragAndDropHandler<GeometryModel> dragDrop;

        public GeometryTree()
        {
            InitializeComponent();

            treeView = this.FindControl<TreeView>("GeometryTreeView");
            treeView.SelectionChanged += OnGeometrySelectionChanged;

            dragDrop =
                new DragAndDropHandler<GeometryModel>(
                    treeView,
                    a => a.Geometry.ParentGeometry != null,
                    (a, o) => true,
                    (a, o) => a.MoveGeometry(o)
                );

            selectedGeometry = new Subject<IReadOnlyCollection<IGeometry>>();
            RootGeometry = new ObservableCollection<GeometryModel>();
        }

        public static readonly DirectProperty<GeometryTree, IGeometryContainer?> GeometryContainerProperty =
            AvaloniaProperty.RegisterDirect<GeometryTree, IGeometryContainer?>(
                nameof(GeometryContainer),
                o => o.GeometryContainer,
                (o, v) => o.GeometryContainer = v
            );

        public IGeometryContainer? GeometryContainer
        {
            get => geometryContainer;
            set
            {
                SetAndRaise(GeometryContainerProperty, ref geometryContainer, value);
                
                if (geometryContainer != null)
                {
                    geometryModel = new GeometryModel(geometryContainer.Root);
                    treeView.Items = new List<GeometryModel>() { geometryModel };
                }
                else
                {
                    geometryModel = null;
                    treeView.Items = null;
                }
            }
        }

        public IObservable<IReadOnlyCollection<IGeometry>> SelectedGeometry =>
            selectedGeometry.AsObservable();


        internal ObservableCollection<GeometryModel> RootGeometry { get; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnGeometrySelectionChanged(
            object? sender, 
            SelectionChangedEventArgs e)
        {
            var treeSelectedGeometry = new List<IGeometry>();

            foreach (var item in treeView.SelectedItems)
            {
                if (item is GeometryModel geometryModel)
                {
                    treeSelectedGeometry.Add(geometryModel.Geometry);
                }
            }

            selectedGeometry.OnNext(treeSelectedGeometry);
        }

        private IGeometryContainer? geometryContainer;
        private GeometryModel? geometryModel;
        private Subject<IReadOnlyCollection<IGeometry>> selectedGeometry;
    }
}
