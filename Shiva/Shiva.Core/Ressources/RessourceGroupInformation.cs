using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource group Information
    /// </summary>
    /// <seealso cref="Shiva.Ressources.IGroupInformation" />
    class RessourceGroupInformation : IGroupInformation
    {
        private readonly Identity _id;
        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceGroupInformation"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ressourceTargetType">Type of the ressource target.</param>
        public RessourceGroupInformation(Identity id,Type ressourceTargetType)
        {
            this._id = id ?? throw new ArgumentNullException(nameof(id));
            this._type = ressourceTargetType ?? throw new ArgumentNullException(nameof(ressourceTargetType));
        }

        public Identity Id => this._id;

        public Type RessourceTargetType => this._type;
    }
}
