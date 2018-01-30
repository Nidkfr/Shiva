using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using Shiva.Core.Services;

namespace Shiva.Services
{
    /// <summary>
    /// Log 4 net log manager Implementation
    /// </summary>
    public sealed class Log4NetLogManager : ILogManager
    {
        private log4net.Repository.ILoggerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLogManager"/> class.
        /// </summary>
        public Log4NetLogManager()
        {
            this._repository = LogManager.GetRepository(this.GetType().Assembly);
        }

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <param name="name">The name of logger.</param>
        /// <returns>
        /// a logger
        /// </returns>
        public ILogger CreateLogger(string name)
        {            
           return new Log4NetLogger(LogManager.GetLogger(this._repository.Name,name));
        }

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <typeparam name="Type">The type name for logger.</typeparam>
        /// <returns>
        /// a logger
        /// </returns>
        public ILogger CreateLogger<Type>()
        {
            return this.CreateLogger(typeof(Type));
        }

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public ILogger CreateLogger(Type type)
        {
            return new Log4NetLogger(LogManager.GetLogger(type));
        }
    }
}
