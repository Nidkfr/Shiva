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
        public Dictionary<Type,IEnumerable<Identity>> RemovedRessources
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the added ressources.
        /// </summary>
        /// <value>
        /// The added ressources.
        /// </value>
        public IList<IRessource> AddedRessources
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the added groups.
        /// </summary>
        /// <value>
        /// The added groups.
        /// </value>
        public IDictionary<IGroupInformation,IEnumerable<Identity>> AddedGroups
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the removed groups.
        /// </summary>
        /// <value>
        /// The removed groups.
        /// </value>
        public IEnumerable<IGroupInformation> RemovedGroups
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the detached ressource groups.
        /// </summary>
        /// <value>
        /// The detached ressource groups.
        /// </value>
        public IDictionary<IGroupInformation,IEnumerable<Identity>> DetachedRessourceGroups
        {
            get;
            set;
        }
    }
}
