using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shiva.Core.Caches
{
    /// <summary>
    /// Class who implement this interface may control cache loading
    /// </summary>
    public interface ICachable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is cached.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is cached; otherwise, <c>false</c>.
        /// </value>
        bool IsCached { get; }

        /// <summary>
        /// Occurs when [cached].
        /// </summary>
        event EventHandler<CachableArg> Cached;

        /// <summary>
        /// Loads the cache.
        /// </summary>
        /// <returns></returns>
        Task LoadCache();

    }
}
