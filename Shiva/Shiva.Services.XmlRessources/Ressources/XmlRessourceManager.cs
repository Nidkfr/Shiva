using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Shiva.Core.Identities;
using System.IO;
using System.Xml;
using Shiva.IO;

namespace Shiva.Ressources
{
    /// <summary>
    /// Xml ressource Manager
    /// </summary>
    public class XmlRessourceManager : RessourceManagerBase
    {
        private readonly CultureInfo _culture;
        private readonly StreamSource _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlRessourceManager"/> class.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public XmlRessourceManager(CultureInfo culture, StreamSource dataXml)
        {
            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
            this._source = dataXml ?? throw new ArgumentNullException(nameof(dataXml));
        }

        /// <summary>
        /// Gets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public override CultureInfo Culture => this._culture;


        public override void AttachRessourceToGroup(Identity ressourceId, Identity groupRessourceId)
        {
            throw new NotImplementedException();
        }

        public override bool ContainsRessource<TRessource>(Identity idRessource)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Identity> GetAllGroups()
        {
            throw new NotImplementedException();
        }

        public override IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Identity groupRessourceId)
        {
            throw new NotImplementedException();
        }

        public override IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Namespace groupNamespaceRessource)
        {
            throw new NotImplementedException();
        }

        public override TRessource GetRessource<TRessource>(Identity ressourceID)
        {
            throw new NotImplementedException();
        }

        public override void RemoveGroup(Identity groupRessourceId)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRessource<TRessource>(Identity idRessource)
        {
            throw new NotImplementedException();
        }

        public override void SaveRessource<TRessource>(TRessource ressource)
        {
            throw new NotImplementedException();
        }
    }
}
