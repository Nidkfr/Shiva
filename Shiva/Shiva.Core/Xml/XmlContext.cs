using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Xml
{
    /// <summary>
    /// Xml context for reading ou wrintting
    /// </summary>
    public class XmlContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlContext"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="prefix">The prefix.</param>
        public XmlContext(string ns,string prefix)
        {
            this.Namespace = ns;
            this.Prefix = prefix;
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public string Namespace { get; private set; }

        /// <summary>
        /// Gets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        public string Prefix { get; private set; }
    }
}
