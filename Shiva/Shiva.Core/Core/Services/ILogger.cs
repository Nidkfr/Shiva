using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Shiva.Core.Services
{
    /// <summary>
    /// Abstraction for logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets a value indicating whether [information is enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [information is enabled]; otherwise, <c>false</c>.
        /// </value>
        bool InfoIsEnabled { get; }

        /// <summary>
        /// Log informations specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The culture.</param>
        /// <param name="value">The value.</param>
        void Info(string message, IFormatProvider format = null, params object[] value);

        /// <summary>
        /// Gets a value indicating whether [debug is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug is enabled]; otherwise, <c>false</c>.
        /// </value>
        bool DebugIsEnabled { get; }

        /// <summary>
        /// Log debugs specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        void Debug(string message, IFormatProvider format = null, params object[] value);

        /// <summary>
        /// Gets a value indicating whether [warn is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [warn is enabled]; otherwise, <c>false</c>.
        /// </value>
        bool WarnIsEnabled { get; }

        /// <summary>
        /// Log warning specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        void Warn(string message, IFormatProvider format = null, params object[] value);

        /// <summary>
        /// Gets a value indicating whether [error is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [error is enabled]; otherwise, <c>false</c>.
        /// </value>
        bool ErrorIsEnabled { get; }

        /// <summary>
        ///  Log Errors specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        void Error(string message, IFormatProvider format = null, params object[] value);

        /// <summary>
        /// Log errors  specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Error(Exception exception);
    }
}
