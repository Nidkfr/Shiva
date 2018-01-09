using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using log4net;
using Shiva.Core.Services;
using System.Threading.Tasks;

namespace Shiva.Services
{
    /// <summary>
    /// Log4net logger implementation
    /// </summary>
    /// <seealso cref="Shiva.Core.Services.ILogger" />
    public sealed class Log4NetLogger : ILogger
    {
        private ILog _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLogger"/> class.
        /// </summary>
        internal Log4NetLogger(log4net.ILog logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets a value indicating whether[information is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [information is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool InfoIsEnabled => this._logger.IsInfoEnabled;

        /// <summary>
        /// Gets a value indicating whether[debug is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugIsEnabled => this._logger.IsDebugEnabled;

        /// <summary>
        /// Gets a value indicating whether[warn is enabled].
        /// </summary>
        /// <value>
        ///  <c>true</c> if [warn is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool WarnIsEnabled => this._logger.IsWarnEnabled;

        /// <summary>
        /// Gets a value indicating whether [error is enabled].
        /// </summary>
        /// <value>
        ///<c>true</c> if [error is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool ErrorIsEnabled => this._logger.IsErrorEnabled;

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        public void Debug(string message, IFormatProvider format = null, params object[] value)
        {
            this._logger.DebugFormat(format ?? CultureInfo.CurrentCulture, message, value);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        public void Debug(string message, params object[] value)
        {
            this.Debug(message, CultureInfo.CurrentCulture, value);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            this._logger.Debug(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        public void Error(string message, IFormatProvider format = null, params object[] value)
        {
            this._logger.ErrorFormat(format ?? CultureInfo.CurrentCulture, message, value);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        public void Error(string message, params object[] value)
        {
            this.Error(message, CultureInfo.CurrentCulture, value);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            this._logger.Error(message);
        }

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Error(Exception exception)
        {
            if (exception is AggregateException aexception)
            {
                foreach (var ex in aexception.InnerExceptions)
                {
                    this.Error(ex);
                }
            }
            else
            {
                this._logger.Error(exception.Message);
                this.Error("Stacktrace : {0}.",exception.StackTrace);
                if (exception.InnerException != null)
                    this.Error(exception.InnerException);
            }
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        public void Info(string message, IFormatProvider format = null, params object[] value)
        {
            this._logger.InfoFormat(format??CultureInfo.CurrentCulture, message, value);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            this._logger.Info(message);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        public void Info(string message, params object[] value)
        {
            this.Info(message, CultureInfo.CurrentCulture, value);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="format">The format.</param>
        /// <param name="value">The value.</param>
        public void Warn(string message, IFormatProvider format = null, params object[] value)
        {
            this._logger.WarnFormat(format ?? CultureInfo.CurrentCulture, message, value);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        public void Warn(string message, params object[] value)
        {
            this.Warn(message, CultureInfo.CurrentCulture, value);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            this._logger.Warn(message);
        }
    }
}
