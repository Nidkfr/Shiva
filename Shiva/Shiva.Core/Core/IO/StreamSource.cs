using System;
using System.IO;

namespace Shiva.Core.IO
{
    /// <summary>
    /// Stream source base
    /// </summary>
    public abstract class StreamSource : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// The save stream became the current stream
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Gets the save stream.
        /// </summary>
        /// <returns>
        /// </returns>
        public abstract Stream GetSaveStream();

        /// <summary>
        /// Opens the current stream.
        /// </summary>
        /// <returns>
        /// </returns>
        public abstract Stream GetStream();

        #endregion Public Methods

        /// <summary>
        /// Gets the stream identitfication.
        /// </summary>
        /// <value>
        /// The stream identitfication.
        /// </value>
        public abstract string StreamIdentitfication { get; }
    }
}