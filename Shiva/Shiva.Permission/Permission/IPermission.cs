using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Shiva.Core.Identities;
using Shiva.Xml;

namespace Shiva.Permission
{
    /// <summary>
    /// Permission
    /// </summary>
    /// <seealso cref="Shiva.Core.Identities.IIdentifiable" />
    public interface IPermission : IIdentifiable, IInclusiveSerializable
    {
        
    }
}
