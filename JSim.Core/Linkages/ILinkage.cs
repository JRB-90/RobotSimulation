using JSim.Core.Common;
using JSim.Core.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSim.Core.Linkages
{
    /// <summary>
    /// Represents a mechanical linkage, used to build kinematic objects.
    /// </summary>
    public interface ILinkage : IHierarchicalTreeObject<ILinkage>, IPositionable
    {
        IGeometryContainer GeometryContainer { get; }
    }
}
