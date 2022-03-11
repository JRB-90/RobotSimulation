using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSim.Core.Render
{
    /// <summary>
    /// Interface to define the behaviour of all rendering managers.
    /// Rendering managers are responsible for rendering scene graphs using
    /// their specific rendering implementation. This may involve creating and
    /// maintaining GPU contexts/GPU memory management.
    /// </summary>
    public interface IRenderingManager
    {
    }
}
