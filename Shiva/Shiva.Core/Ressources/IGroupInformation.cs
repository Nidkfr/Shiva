using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Group Information
    /// </summary>
    public interface IGroupInformation : IIdentifiable
    {       
        /// <summary>
        /// Gets the type of the ressource target.
        /// </summary>
        /// <value>
        /// The type of the ressource target.
        /// </value>
        Type RessourceTargetType { get; }
    }
}
