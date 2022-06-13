using JSim.Core.Common;
using JSim.Core.Render;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace JSim.Av.Models
{
    internal class GeometryModel : ReactiveObject
    {
        public GeometryModel(IGeometry geometry)
        {
            Geometry = geometry;
            Name = geometry.Name;

            Children =
                new ObservableCollection<GeometryModel>(
                    Geometry
                    .Children
                    .Select(c => new GeometryModel(c))
                );

            Geometry.GeometryModified += Geometry_GeometryModified;
        }

        public IGeometry Geometry { get; }

        public string Name { get; }

        public bool IsExpanded
        {
            get => isExpanded;
            set => this.RaiseAndSetIfChanged(ref isExpanded, value);
        }

        public ObservableCollection<GeometryModel> Children { get; }

        public bool MoveGeometry(GeometryModel parentGeometry)
        {
            if (Geometry.MoveGeometry(parentGeometry.Geometry))
            {
                SetExpanded(parentGeometry);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Remove()
        {
            Geometry.ParentGeometry?.DetachGeometry(Geometry);
        }

        public bool CanRemove(object paramater)
        {
            return Geometry.ParentGeometry != null;
        }

        public void CreateCube()
        {
            // TODO
        }

        public void CreateFrame()
        {
            // TODO
        }

        public override string ToString()
        {
            return Geometry.Name;
        }

        private void Geometry_GeometryModified(
            object sender, 
            GeometryModifiedEventArgs e)
        {
            var childrenToRemove = new List<GeometryModel>();

            foreach (var child in Children)
            {
                if (!Geometry.Children.Contains(child.Geometry))
                {
                    childrenToRemove.Add(child);
                }
            }

            foreach (var child in childrenToRemove)
            {
                Children.Remove(child);
            }

            var geometries =
                Children
                .Select(o => o.Geometry);

            foreach (var child in Geometry.Children)
            {
                if (!geometries.Contains(child))
                {
                    Children.Add(new GeometryModel(child));
                    IsExpanded = true;
                }
            }
        }

        private void SetExpanded(GeometryModel parentGeometry)
        {
            var flatListBefore =
                Children
                .Flatten(g => g.Children)
                .ToList();

            flatListBefore.Add(this);

            var flatListAfter =
                parentGeometry
                .Children
                .Flatten(g => g.Children)
                .ToDictionary(g => g.Geometry, g => g);

            foreach (var child in flatListBefore)
            {
                if (flatListAfter.ContainsKey(child.Geometry))
                {
                    flatListAfter[child.Geometry].IsExpanded = child.IsExpanded;
                }
            }
        }

        private bool isExpanded;
    }
}
