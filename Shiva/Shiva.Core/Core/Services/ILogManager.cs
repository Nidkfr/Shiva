using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Core.Services
{
    /// <summary>
    /// Abstraction for Log Management
    /// </summary>
    public interface ILogManager
    {
        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <param name="name">The name of logger.</param>
        /// <returns>a logger</returns>
        ILogger CreateLogger(string name);

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <typeparam name="Type">The type name for logger.</typeparam>
        /// <returns>a logger</returns>
        ILogger CreateLogger<Type>();

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        ILogger CreateLogger(Type type);
    }
}
