using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Core.Services
{
    /// <summary>
    /// No logger
    /// </summary>
    /// <seealso cref="Shiva.Core.Services.ILogger" />
    public sealed class NoLogger : ILogger
    {
        /// <summary>
        /// Gets a value indicating whether [information is enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [information is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool InfoIsEnabled => false;

        /// <summary>
        /// Gets a value indicating whether [debug is enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [debug is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugIsEnabled => false;

        /// <summary>
        /// Gets a value indicating whether [warn is enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [warn is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool WarnIsEnabled => false;

        /// <summary>
        /// Gets a value indicating whether [error is enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [error is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool ErrorIsEnabled => false;

        /// <summary>
        /// Log debugs specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Debug(string message, IFormatProvider format = null, params object[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Debug(string message, params object[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Log Errors specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Error(string message, IFormatProvider format = null, params object[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Log errors  specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Error(Exception exception)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Error(string message, params object[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Log informations specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The culture.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Info(string message, IFormatProvider format = null, params object[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Info(string message, params object[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Log warning specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Warn(string message, IFormatProvider format = null, params object[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Warn(string message, params object[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Warn(string message)
        {
            throw new NotImplementedException();
        }
    }
}
