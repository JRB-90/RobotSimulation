using JSim.Core.Common;
using JSim.Core.Maths;
using JSim.Core.Render;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Represents the basic implementation of a linkage.
    /// </summary>
    public class Linkage : ILinkage
    {
        public Linkage(
            IGeometryContainer geometryContainer)
        {
            name = "Link";
            id = Guid.NewGuid();
            parent = null;
            children = new List<ITreeObject>();
            GeometryContainer = geometryContainer;
            worldFrame = Transform3D.Identity;
            localFrame = Transform3D.Identity;
        }
        
        public string Name
        {
            get => name;
            set => name = value;
        }

        public Guid ID => id;

        public bool IsTreeRoot => Parent == null;

        public IHierarchicalTreeObject<ITreeObject>? Parent => parent;

        public IChildContainer<ILinkage> Children => Children;

        public IGeometryContainer GeometryContainer { get; }

        Transform3D IPositionable.WorldFrame
        {
            get => worldFrame;
            set => worldFrame = value;
        }
        Transform3D IPositionable.LocalFrame
        {
            get => localFrame;
            set => localFrame = value;
        }

        public event PositionModifiedEventHandler? PositionModified;

        public event TreeObjectModifiedEventHandler? ObjectModified;

        public bool MoveContainer(IHierarchicalTreeObject<ITreeObject> newContainer)
        {
            if (IsTreeRoot)
            {
                return false;
            }

            //if (!Parent.DetachGeometry(this))
            //{
            //    return false;
            //}

            //if (newContainer.AttachGeometry(this))
            //{
            //    parent = newContainer;
            //    FireGeometryModifiedEvent();

            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return false;
        }

        private void RaiseObjectModified()
        {
            ObjectModified?.Invoke(this, new TreeObjectModifiedEventArgs(this));
        }

        private string name;
        private Guid id;
        private IHierarchicalTreeObject<ITreeObject>? parent;
        private List<ITreeObject> children;
        private Transform3D worldFrame;
        private Transform3D localFrame;
    }
}
