using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Shiva.Core.IO
{
    /// <summary>
    /// Stream source base
    /// </summary>
    public abstract class StreamSource:IDisposable
    {
        /// <summary>
        /// Opens the current stream.
        /// </summary>
        /// <returns></returns>
        public abstract Stream GetStream();

        /// <summary>
        /// Gets the save stream.
        /// </summary>
        /// <returns></returns>
        public abstract Stream GetSaveStream();

        /// <summary>
        /// The save stream became the current stream 
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();
        
    }
}
