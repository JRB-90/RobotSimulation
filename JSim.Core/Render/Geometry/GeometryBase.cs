using JSim.Core.Common;
using JSim.Core.Maths;
using JSim.Core.SceneGraph;
using System.Collections;

namespace JSim.Core.Render
{
    /// <summary>
    /// Abstract base class for all geometry types.
    /// </summary>
    public abstract class GeometryBase : IGeometry
    {
        readonly INameRepository nameRepository;
        readonly IMessageCollator messageCollator;
        readonly IGeometryCreator creator;

        public GeometryBase(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            IGeometryCreator creator)
        {
            this.nameRepository = nameRepository;
            this.messageCollator = messageCollator;
            this.creator = creator;
            name = nameRepository.GenerateUniqueName("Geometry");
            ID = Guid.NewGuid();
            isVisible = true;
            isHighlighted = false;
            isSelectable = true;
            isSelected = false;
            parentGeometry = null;
            worldFrame = new Transform3D();
            localFrame = new Transform3D();
            children = new List<IGeometry>();
            vertices = new List<Vertex>();
            indices = new List<uint>();
            geometryType = GeometryType.Solid;
            material = new Material();
            material.MaterialModified += OnMaterialModified;
        }

        public GeometryBase(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            IGeometryCreator creator,
            IGeometry? parentGeometry)
        {
            this.nameRepository = nameRepository;
            this.messageCollator = messageCollator;
            this.creator = creator;
            this.parentGeometry = parentGeometry;

            name = nameRepository.GenerateUniqueName("Geometry");
            ID = Guid.NewGuid();
            isVisible = true;
            isHighlighted = false;
            isSelectable = true;
            isSelected = false;
            children = new List<IGeometry>();
            localFrame = Transform3D.Identity;
            vertices = new List<Vertex>();
            indices = new List<uint>();
            geometryType = GeometryType.Solid;
            material = new Material();
            material.MaterialModified += OnMaterialModified;

            if (parentGeometry != null)
            {
                worldFrame = parentGeometry.WorldFrame;
                parentGeometry.GeometryModified += OnParentGeometryModified;
            }
            else
            {
                worldFrame = Transform3D.Identity;
            }
        }

        public GeometryBase(
            INameRepository nameRepository,
            IMessageCollator messageCollator,
            IGeometryCreator creator,
            Guid id,
            string name,
            bool isVisible,
            bool isHighlighted,
            bool isSelectable,
            IGeometry? parentGeometry,
            Transform3D worldFrame,
            Transform3D localFrame,
            IReadOnlyList<Vertex> vertices,
            IReadOnlyList<uint> indices,
            IMaterial material,
            GeometryType geometryType)
        {
            if (!nameRepository.IsUniqueName(name))
            {
                throw new ArgumentException("Name is not unique");
            }

            this.nameRepository = nameRepository;
            this.messageCollator = messageCollator;
            this.creator = creator;
            this.name = name;
            this.isVisible = isVisible;
            this.isHighlighted = isHighlighted;
            this.isSelectable = isSelectable;
            this.worldFrame = worldFrame;
            this.localFrame = localFrame;
            this.vertices = vertices;
            this.indices = indices;
            this.material = material;
            this.geometryType = geometryType;
            material.MaterialModified += OnMaterialModified;

            ID = id;
            isSelected = false;
            children = new List<IGeometry>();

            if (parentGeometry != null)
            {
                this.parentGeometry = parentGeometry;
                parentGeometry.GeometryModified += OnParentGeometryModified;
            }
        }

        public Guid ID { get; }

        public string Name
        {
            get => name;
            set
            {
                if (nameRepository.IsUniqueName(value))
                {
                    nameRepository.AddName(value);
                    nameRepository.RemoveName(name);
                    name = value;
                    FireGeometryModifiedEvent();
                }
            }
        }

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                FireGeometryModifiedEvent();
            }
        }

        public bool IsHighlighted
        {
            get => isHighlighted;
            set
            {
                isHighlighted = value;
                FireGeometryModifiedEvent();
            }
        }

        public bool IsSelectable
        {
            get => isSelectable;
            set
            {
                isSelectable = value;
                FireGeometryModifiedEvent();

                if (!isSelectable)
                {
                    IsSelected = false;
                }
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelectable)
                {
                    isSelected = value;
                }
                else
                {
                    isSelected = false;
                }
                FireSelectionStateChangedEvent();
            }
        }

        public IGeometry? ParentGeometry
        {
            get => parentGeometry;
            private set
            {
                parentGeometry = value;

                if (parentGeometry != null)
                {
                    LocalFrame = new Transform3D(localFrame);
                    parentGeometry.GeometryModified += OnParentGeometryModified;
                }
                else
                {
                    WorldFrame = Transform3D.Identity;
                }

                FireGeometryModifiedEvent();
            }
        }

        public Transform3D WorldFrame
        {
            get => worldFrame;
            set
            {
                worldFrame = value;

                if (parentGeometry != null)
                {
                    localFrame =
                        Transform3D.RelativeTransform(
                            parentGeometry.WorldFrame,
                            worldFrame
                        );
                }
                else
                {
                    localFrame = worldFrame;
                }

                FireGeometryModifiedEvent();
            }
        }

        public Transform3D LocalFrame
        {
            get => localFrame;
            set
            {
                localFrame = value;

                if (parentGeometry != null)
                {
                    worldFrame = parentGeometry.WorldFrame * localFrame;
                }
                else
                {
                    worldFrame = localFrame;
                }

                FireGeometryModifiedEvent();
            }
        }

        public IReadOnlyCollection<IGeometry> Children => children;

        public IReadOnlyList<Vertex> Vertices
        {
            get => vertices;
            protected set
            {
                vertices = value;
                Rebuild();
                FireGeometryRebuiltEvent();
            }
        }

        public IReadOnlyList<uint> Indices
        {
            get => indices;
            protected set
            {
                indices = value;
                Rebuild();
                FireGeometryRebuiltEvent();
            }
        }

        public IMaterial Material
        {
            get => material;
            set
            {
                material = value;
                material.MaterialModified += OnMaterialModified;
                FireGeometryModifiedEvent();
            }
        }

        public GeometryType GeometryType
        {
            get => geometryType;
            set
            {
                geometryType = value;
                FireGeometryModifiedEvent();
            }
        }

        public event GeometryRebuiltEventHandler? GeometryRebuilt;

        public event GeometryModifiedEventHandler? GeometryModified;

        public event SelectionStateChangedEventHandler? SelectionStateChanged;

        public void SetDrawingData(
            IReadOnlyList<Vertex> vertices,
            IReadOnlyList<uint> indices)
        {
            this.vertices = vertices;
            this.indices = indices;
            Rebuild();
            FireGeometryRebuiltEvent();
        }

        public bool MoveGeometry(IGeometry newParent)
        {
            if (ParentGeometry == null)
            {
                return false;
            }

            if (!ParentGeometry.DetachGeometry(this))
            {
                return false;
            }

            if (newParent.AttachGeometry(this))
            {
                ParentGeometry = newParent;
                FireGeometryModifiedEvent();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AttachGeometry(IGeometry geometry)
        {
            if (Children.Contains(geometry))
            {
                return false;
            }
            else
            {
                children.Add(geometry);
                FireGeometryModifiedEvent();

                return true;
            }
        }

        public bool DetachGeometry(IGeometry geometry)
        {
            if (children.Contains(geometry))
            {
                children.Remove(geometry);
                FireGeometryModifiedEvent();

                return true;
            }
            else
            {
                return false;
            }
        }

        public IGeometry CreateChildGeometry()
        {
            IGeometry geometry = creator.CreateGeometry(this);
            children.Add(geometry);
            FireGeometryModifiedEvent();

            return geometry;
        }

        public IGeometry CreateChildGeometry(string name)
        {
            IGeometry geometry = creator.CreateGeometry(this);
            geometry.Name = name;
            children.Add(geometry);
            FireGeometryModifiedEvent();

            return geometry;
        }

        public void RecalculateWorldPosition(Transform3D parentWorldPosition)
        {
            worldFrame =
                parentWorldPosition *
                localFrame;

            foreach (IGeometry child in Children)
            {
                child.RecalculateWorldPosition(worldFrame);
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{Name}";
        }

        protected void FireGeometryRebuiltEvent()
        {
            GeometryRebuilt?.Invoke(this, new GeometryRebuiltEventArgs());
        }

        protected void FireGeometryModifiedEvent()
        {
            GeometryModified?.Invoke(this, new GeometryModifiedEventArgs());
            messageCollator.Publish(new GeometryModified());
        }

        protected void FireSelectionStateChangedEvent()
        {
            SelectionStateChanged?.Invoke(this, new SelectionStateChangedEventArgs(isSelected));
        }

        /// <summary>
        /// Causes all rendering engine specific resources to be rebuilt.
        /// </summary>
        protected abstract void Rebuild();

        public IEnumerator<IGeometry> GetEnumerator()
        {
            foreach (IGeometry geometry in IterateGeometry(this))
            {
                yield return geometry;
            }
        }

        private IEnumerable<IGeometry> IterateGeometry(IGeometry parentGeometry)
        {
            foreach (IGeometry childGeometry in parentGeometry.Children)
            {
                foreach (IGeometry geometry in IterateGeometry(childGeometry))
                {
                    yield return geometry;
                }

                yield return childGeometry;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void OnParentGeometryModified(object sender, GeometryModifiedEventArgs e)
        {
            LocalFrame = LocalFrame;
        }

        private void OnMaterialModified(object sender, MaterialModifiedEventArgs e)
        {
            FireGeometryModifiedEvent();
        }

        private string name;
        private bool isVisible;
        private bool isHighlighted;
        private bool isSelectable;
        private bool isSelected;
        private IGeometry? parentGeometry;
        private Transform3D worldFrame;
        private Transform3D localFrame;
        private List<IGeometry> children;
        private IReadOnlyList<Vertex> vertices;
        private IReadOnlyList<uint> indices;
        private IMaterial material;
        private GeometryType geometryType;
    }
}
