using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Cached Ressource groupe
    /// </summary>
    public class RessourceCachedGroupe : IIdentifiable
    {
        private readonly List<IRessource> _ressources = new List<IRessource>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceCachedGroupe"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public RessourceCachedGroupe(Identity id)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Identity
        /// </summary>
        public Identity Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Attaches the ressource.
        /// </summary>
        /// <param name="ressource">The ressource.</param>
        public void AttachRessource(IRessource ressource)
        {
            if (ressource == null)
                throw new ArgumentNullException(nameof(ressource));

            if (!this._ressources.Contains(ressource))
                this._ressources.Add(ressource);
        }

        /// <summary>
        /// Detaches the ressource.
        /// </summary>
        /// <param name="ressource">The ressource.</param>
        public void DetachRessource(IRessource ressource)
        {
            if (ressource == null)
                throw new ArgumentNullException(nameof(ressource));

            this._ressources.Remove(ressource);
        }
    }
}
