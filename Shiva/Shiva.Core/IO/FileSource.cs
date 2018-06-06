using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shiva.IO
{
    /// <summary>
    /// File source
    /// </summary>
    public class FileSource : StreamSource , IDisposable
    {
        private readonly string _filepath;
        private Stream _currentStream;
        private readonly bool _isReadOnly;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSource"/> class.
        /// </summary>
        /// <param name="filepath">The file URI.</param>
        /// <param name="isReadOnly"></param>
        public FileSource(string filepath, bool isReadOnly = false)
        {
            this._filepath = filepath ?? throw new ArgumentNullException(nameof(filepath));
            this._isReadOnly = isReadOnly;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FileSource" /> class.
        /// </summary>
        ~FileSource()
        {
            this.Dispose();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public override bool IsReadOnly => this._isReadOnly;

        /// <summary>
        /// Gets a value indicating whether this instance is open.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is open; otherwise, <c>false</c>.
        /// </value>
        public override bool IsOpen => this._currentStream?.CanRead ?? false;

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>
        /// The stream.
        /// </value>
        public override Stream Stream
        {
            get
            {
                if (!this.IsOpen)
                    this.Open();

                if (!this.IsOpen)
                    throw new InvalidOperationException("Fail to open Stream.");

                return this._currentStream;
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            if(this.IsOpen)
            {
                this._currentStream.Close();
                this._currentStream.Dispose();
            }
        }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public override void Open()
        {
            if (!this.IsOpen)
                if (this.IsReadOnly)
                    this._currentStream = File.OpenRead(this._filepath.ToString());
                else
                    this._currentStream = File.Open(this._filepath.ToString(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }
    }
}
