using Shiva.Core.Identities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource Edition information
    /// </summary>
    public class RessourcesEditInfo
    {
        /// <summary>
        /// Gets or sets the removed ressources.
        /// </summary>
        /// <value>
        /// The removed ressources.
        /// </value>
        public Dictionary<Type,Identity> RemovedRessources
        {
            get;
            set;
        }


    }
}
