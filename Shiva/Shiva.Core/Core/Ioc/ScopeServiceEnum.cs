using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Core.Ioc
{
    /// <summary>
    /// Scope mode for service container
    /// </summary>
    public enum ScopeServiceEnum : byte
    {
        /// <summary>
        /// Return a new Instance of object at each Resolve
        /// </summary>
        Transient = 0,

        /// <summary>
        /// Return the same instance of object at each Resolve
        /// </summary>
        Singleton = 1,
    }
}
