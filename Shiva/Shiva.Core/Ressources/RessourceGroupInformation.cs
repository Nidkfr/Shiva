using Shiva.Core.Identities;
using System;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource group Information
    /// </summary>
    /// <seealso cref="Shiva.Ressources.IGroupInformation" />
    ///
    internal sealed class RessourceGroupInformation : IGroupInformation
    {
        #region Private Fields

        private readonly Identity _id;
        private readonly Type _type;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceGroupInformation" /> class.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <param name="ressourceTargetType">
        /// Type of the ressource target.
        /// </param>
        public RessourceGroupInformation(Identity id, Type ressourceTargetType)
        {
            this._id = id ?? throw new ArgumentNullException(nameof(id));
            this._type = ressourceTargetType ?? throw new ArgumentNullException(nameof(ressourceTargetType));
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Identity Id => this._id;

        /// <summary>
        /// Gets the type of the ressource target.
        /// </summary>
        /// <value>
        /// The type of the ressource target.
        /// </value>
        public Type RessourceTargetType => this._type;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="id1">
        /// The NS1.
        /// </param>
        /// <param name="id2">
        /// The NS2.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(RessourceGroupInformation id1, IGroupInformation id2)
        {
            if (!(id1 is null))
                return !id1.Equals(id2);

            if (id1 is null && !(id2 is null))
                return !id2.Equals(id2);

            return false;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="id1">
        /// The NS1.
        /// </param>
        /// <param name="id2">
        /// The NS2.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(RessourceGroupInformation id1, IGroupInformation id2)
        {
            if (!(id1 is null))
                return id1.Equals(id2);

            if (id1 is null && !(id2 is null))
                return id2.Equals(id2);

            return true;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object" /> to compare with this instance.
        /// </param>
        /// <returns>
        /// <c> true </c> if the specified <see cref="System.Object" /> is equal to this instance;
        /// otherwise, <c> false </c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;

            if (obj is IGroupInformation grp)
            {
                return this.Id == grp.Id && this.RessourceTargetType == grp.RessourceTargetType;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures
        /// like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Group(id:{this.Id} target:{this._type})";
        }

        #endregion Public Methods
    }
}