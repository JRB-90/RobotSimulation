using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSim.Core.Common
{
    /// <summary>
    /// Defines an object that will be rendered in a 3D scene.
    /// A renderable object contains properties that help in
    /// a tree to decide how to render the object.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Flag to indicate if the scene object should not be rendered.
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// Flag to indicate if the scene object should be highlighted.
        /// </summary>
        bool IsHighlighted { get; set; }
    }
}
