using Shiva.Core.Identities;
using System;

namespace Shiva.Ressources
{
    /// <summary>
    /// Group Information
    /// </summary>
    public interface IGroupInformation : IIdentifiable
    {
        #region Public Properties

        /// <summary>
        /// Gets the type of the ressource target.
        /// </summary>
        /// <value>
        /// The type of the ressource target.
        /// </value>
        Type RessourceTargetType { get; }

        #endregion Public Properties
    }
}