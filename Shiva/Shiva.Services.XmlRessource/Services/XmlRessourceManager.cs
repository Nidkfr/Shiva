using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shiva.Core.Identities;
using Shiva.Globalization;

namespace Shiva.Services
{
    /// <summary>
    /// Xml ressource managements
    /// </summary>
    /// <seealso cref="Shiva.Globalization.RessourceManagerBase" />
    public class XmlRessourceManager : RessourceManagerBase
    {
        public override IEnumerable<Identity> GetAllGroup()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Identity> GetAllIdRessourceFromNamespace(Namespace ns)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Namespace> GetAllNamespace()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TRessource> GetAllRessource<TRessource>(Identity idmessage)
        {
            throw new NotImplementedException();
        }

        public override IRessourceReader<TRessource> GetReader<TRessource>(CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public override IRessourceReaderFromNamespace<TRessource> GetReader<TRessource>(Namespace ns, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public override IRessourceReaderFromGroup<TRessource> GetReader<TRessource>(Identity idGroup, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public override Task<IRessourceReader<TRessource>> GetReaderAsync<TRessource>(CancellationToken? token = null, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public override Task<IRessourceReaderFromNamespace<TRessource>> GetReaderAsync<TRessource>(Namespace ns, CancellationToken? token = null, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public override Task<IRessourceReaderFromGroup<TRessource>> GetReaderAsync<TRessource>(Identity idGroup, CancellationToken? token = null, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public override IRessourceWriter GetRessourceWriter()
        {
            throw new NotImplementedException();
        }
    }
}
