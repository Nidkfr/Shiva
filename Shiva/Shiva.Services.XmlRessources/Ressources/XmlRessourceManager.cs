using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Shiva.Core.Identities;
using System.IO;
using System.Xml;


namespace Shiva.Ressources
{
    /// <summary>
    /// Xml ressource Manager
    /// </summary>
    public class XmlRessourceManager : RessourceManagerBase, IDisposable
    {
        private readonly CultureInfo _culture;
        private readonly Stream _stream;
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlRessourceManager"/> class.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public XmlRessourceManager(CultureInfo culture, Stream dataXml)
        {
            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
            this._stream = dataXml ?? throw new ArgumentNullException(nameof(dataXml));
        }

        public override CultureInfo Culture =>this._culture;

        ~XmlRessourceManager()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this._stream.Close();
        }

        public override IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Identity groupRessourceId)
        {
            throw new NotImplementedException();
        }

        public override IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Namespace groupNamespaceRessource)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRessource<TRessource>(Identity idRessource)
        {
            throw new NotImplementedException();
        }

        public override void Save(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected override bool ContainsRessourceInternal<TRessource>(Identity ressourceID)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Identity> GetAllGroupsInternal()
        {
            throw new NotImplementedException();
        }

        protected override TRessource GetRessourceInternal<TRessource>(Identity ressourceID)
        {
            throw new NotImplementedException();
        }
    }
}
