﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface to define the behavior of all scene managers.
    /// A scene manager is used to create scenes, interact with them and clean up
    /// resources after use.
    /// </summary>
    public interface ISceneManager : IDisposable
    {
    }
}