using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Shiva.Core.Identities
{
    /// <summary>
    /// ID class
    /// </summary>
    public sealed class Identity
    {


        private Identity()
        {

        }

        /// <summary>
        /// Initialize Id class
        /// </summary>
        /// <param name="fullid">Id string based</param>
        public Identity(string fullid):this()
        {
            if (string.IsNullOrWhiteSpace(fullid))
                throw new ArgumentNullException(nameof(fullid));

            var nodes = fullid.Split(Namespace.NamespaceSeparator[0]);

            if(nodes.Length == 1)
            {
                this.Namespace = Namespace.Null;
                this.Id = fullid.Trim();
            }
            else
            {
                this.Id = nodes.Last().Trim();
                this.Namespace = fullid.Substring(0,(fullid.LastIndexOf(this.Id) - 1));
            }
        }

        /// <summary>
        /// Initilize Id Class
        /// </summary>
        /// <param name="ns">namespace</param>
        /// <param name="id">id</param>
        public Identity(Namespace ns, string id):this()
        {
            this.Namespace = ns ?? Namespace.Null;
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            this.Id = id;
        }

        /// <summary>
        /// Id Namespace
        /// </summary>
        public Namespace Namespace
        {
            get;
            private set;
        }

        /// <summary>
        /// Id value
        /// </summary>
        public string Id { get; private set; }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (this.Namespace == null)
                return this.Id;
            else
                return $"{this.Namespace}.{this.Id}";
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="id1">The NS1.</param>
        /// <param name="id2">The NS2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Identity id1, Identity id2)
        {
            if (!object.ReferenceEquals(id1, null))
                return id1.Equals(id2);

            if (object.ReferenceEquals(id1, null) && !object.ReferenceEquals(id2, null))
                return id2.Equals(id2);

            return true;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="id1">The NS1.</param>
        /// <param name="id2">The NS2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Identity id1, Identity id2)
        {
            if (!object.ReferenceEquals(id1, null))
                return !id1.Equals(id2);

            if (object.ReferenceEquals(id1, null) && !object.ReferenceEquals(id2, null))
                return !id2.Equals(id2);

            return false;

        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;

            if(obj is string idstring)
            {
                return this.Equals(new Identity(idstring));
            }

            if(obj is Identity id)
            {
                return this.ToString() == id.ToString();
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Implicit operator string Identity
        /// </summary>
        /// <param name="id">Identity</param>
        public static implicit operator string(Identity id)
        {
            return id.ToString();
        }

        /// <summary>
        /// Implicit operator Identity string
        /// </summary>
        /// <param name="id">string</param>
        public static implicit operator Identity (string id)
        {
            return new Identity(id);
        }
    }
}
