using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shiva.IO
{
    /// <summary>
    /// Stream Source
    /// </summary>
    public abstract class StreamSource
    {
        
        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsReadOnly { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is open.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is open; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsOpen { get; }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>
        /// The stream.
        /// </value>
        public abstract Stream Stream { get; }


        /// <summary>
        /// Closes this instance.
        /// </summary>
        public abstract void Close();



        /// <summary>
        /// Opens this instance.
        /// </summary>
        public abstract void Open();
        
    }
}
