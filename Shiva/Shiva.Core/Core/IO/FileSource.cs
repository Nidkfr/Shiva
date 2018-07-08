﻿using Shiva.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shiva.Core.IO
{
    /// <summary>
    /// File source
    /// </summary>
    /// <seealso cref="Shiva.Core.IO.StreamSource" />
    public class FileSource : StreamSource
    {
        private readonly FileInfo _fileinfo;
        private readonly FileInfo _fileSaveinfo;
        private Stream _currentStream;
        private Stream _saveStream;
        private bool _isDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSource"/> class.
        /// </summary>
        /// <param name="saveMode">Save Mode</param>
        /// <param name="path">The path.</param>
        public FileSource(string path, FileSourceSaveModeEnum saveMode = FileSourceSaveModeEnum.NONE)
        {
            this._fileinfo = new FileInfo(path);
            this._fileSaveinfo = new FileInfo(Path.Combine(this._fileinfo.Directory.FullName, Path.GetRandomFileName()));
            this.SaveMode = saveMode;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            this._currentStream?.Close();
            this._saveStream?.Close();
            this._currentStream = null;
            this._saveStream = null;
            this._isDisposed = true;
        }

        /// <summary>
        /// The save stream became the current stream
        /// </summary>
        public override void Flush()
        {
            this._currentStream.Close();
            this._saveStream.Close();

            this._currentStream = null;
            this._saveStream = null;


            switch (this.SaveMode)
            {
                case FileSourceSaveModeEnum.NONE:
                    File.Copy(this._fileSaveinfo.FullName, this._fileinfo.FullName,true);
                    break;
                case FileSourceSaveModeEnum.KEEPPREVIOUSVERSION:
                    File.Replace(this._fileSaveinfo.FullName, this._fileinfo.FullName, Path.ChangeExtension(this._fileSaveinfo.FullName, "backup"));
                    break;
                case FileSourceSaveModeEnum.KEEPALLPREVIOUSVERSION:                    
                    File.Replace(this._fileSaveinfo.FullName, this._fileinfo.FullName, $"{this._fileinfo.Directory.FullName}\\{Path.GetFileNameWithoutExtension(this._fileinfo.FullName)}.{DateTime.Now.ToString("ddMMyyyyhhmmssffff")}.backup");
                    break;
                default:throw new InvalidEnumOptionException(this.SaveMode.ToString());                    
            }            
            
            File.Delete(this._fileSaveinfo.FullName);
        }


        /// <summary>
        /// Gets the save mode.
        /// </summary>
        /// <value>
        /// The save mode.
        /// </value>
        public FileSourceSaveModeEnum SaveMode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the save stream.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ObjectDisposedException">FileSource</exception>
        public override Stream GetSaveStream()
        {
            
            if (!this._isDisposed)
            {
                if (this._saveStream == null || !this._saveStream.CanWrite)
                    this._saveStream = File.Open(this._fileSaveinfo.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                return this._saveStream;
            }
            else
                throw new ObjectDisposedException(nameof(FileSource));

        }

        /// <summary>
        /// Opens the current stream.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ObjectDisposedException">FileSource</exception>
        public override Stream GetStream()
        {
            if (!this._isDisposed)
            {
                if (this._currentStream == null || !this._currentStream.CanRead)
                    this._currentStream = File.Open(this._fileinfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
                return this._currentStream;
            }
            else
                throw new ObjectDisposedException(nameof(FileSource));
        }
    }

    /// <summary>
    /// File source save mode
    /// </summary>
    public enum FileSourceSaveModeEnum
    {
        /// <summary>
        /// Aucune sauvegarde 
        /// </summary>
        NONE,

        /// <summary>
        /// The keep previous version
        /// </summary>
        KEEPPREVIOUSVERSION,

        /// <summary>
        /// The keep all previouse version
        /// </summary>
        KEEPALLPREVIOUSVERSION,

    }
}