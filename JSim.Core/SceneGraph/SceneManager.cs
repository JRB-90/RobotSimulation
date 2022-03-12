using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Standard implementation class for a JSim scene manager.
    /// </summary>
    public class SceneManager : ISceneManager
    {
        readonly ILogger logger;

        public SceneManager(
            ILogger logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            // TODO - Dispose scene graph
        }
    }
}
