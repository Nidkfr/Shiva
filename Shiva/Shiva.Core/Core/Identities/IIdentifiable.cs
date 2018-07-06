using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Core.Identities
{
    /// <summary>
    /// Object is identidiable
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Identity Id { get; }
    }
}
