using System;
using System.Linq;

namespace Shiva.Core.Identities
{
    /// <summary>
    /// ID class
    /// </summary>
    public sealed class Identity
    {
        #region Public Constructors

        /// <summary>
        /// Initialize Id class
        /// </summary>
        /// <param name="fullid">
        /// Id string based
        /// </param>
        public Identity(string fullid) : this()
        {
            if (string.IsNullOrWhiteSpace(fullid))
                throw new ArgumentNullException(nameof(fullid));

            var nodes = fullid.Split(Namespace.NAMESPACESEPARATOR[0]);

            if (nodes.Length == 1)
            {
                this.Namespace = Namespace.Null;
                this.Key = fullid.Trim();
            }
            else
            {
                this.Key = nodes.Last().Trim();
                this.Namespace = fullid.Substring(0, (fullid.LastIndexOf(this.Key) - 1));
            }
        }

        /// <summary>
        /// Initilize Id Class
        /// </summary>
        /// <param name="ns">
        /// namespace
        /// </param>
        /// <param name="id">
        /// id
        /// </param>
        public Identity(Namespace ns, string id) : this()
        {
            this.Namespace = ns ?? Namespace.Null;
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            this.Key = id;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Identity" /> class from being created.
        /// </summary>
        private Identity()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Id value
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Id Namespace
        /// </summary>
        public Namespace Namespace
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Implicit operator Identity string
        /// </summary>
        /// <param name="id">
        /// string
        /// </param>
        public static implicit operator Identity(string id)
        {
            return new Identity(id);
        }

        /// <summary>
        /// Implicit operator string Identity
        /// </summary>
        /// <param name="id">
        /// Identity
        /// </param>
        public static implicit operator string(Identity id)
        {
            return id.ToString();
        }

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
        public static bool operator !=(Identity id1, Identity id2)
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
        public static bool operator ==(Identity id1, Identity id2)
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

            if (obj is string idstring)
            {
                return this.Equals(new Identity(idstring));
            }

            if (obj is Identity id)
            {
                return this.ToString() == id.ToString();
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
            if (this.Namespace == null)
                return this.Key;
            else
                return $"{this.Namespace}.{this.Key}";
        }

        #endregion Public Methods
    }
}