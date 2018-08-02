using System;

namespace Shiva.Core.Identities
{
    /// <summary>
    /// Namespace Node
    /// </summary>
    public sealed class NamespaceNode
    {
        #region Private Fields

        private string _node;

        #endregion Private Fields

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceNode" /> class.
        /// </summary>
        /// <param name="root">
        /// The root.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        internal NamespaceNode(Namespace root, string node)
        {
            this.Root = root ?? throw new ArgumentNullException(nameof(root));
            if (string.IsNullOrWhiteSpace(node))
                throw new ArgumentNullException(nameof(node));
            this._node = node;
        }

        #endregion Internal Constructors

        #region Public Properties

        /// <summary>
        /// Gets the next node.
        /// </summary>
        /// <value>
        /// The next node.
        /// </value>
        public NamespaceNode NextNode => this.Root.GetNextNode(this);

        /// <summary>
        /// Gets the root.
        /// </summary>
        /// <value>
        /// The root.
        /// </value>
        public Namespace Root { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="Shiva.Core.Identities.NamespaceNode" />
        /// to <see cref="System.String" />.
        /// </summary>
        /// <param name="nsn">
        /// The NSN.
        /// </param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(NamespaceNode nsn)
        {
            if (nsn == null)
                return string.Empty;

            return nsn.ToString();
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="nsn1">
        /// The NSN1.
        /// </param>
        /// <param name="nsn2">
        /// The NSN2.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(NamespaceNode nsn1, string nsn2)
        {
            if (!(nsn1 is null))
                return !nsn1.Equals(nsn2);

            if (nsn1 is null && !(nsn2 is null))
                return !nsn2.Equals(nsn1);

            return false;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="nsn1">
        /// The NSN1.
        /// </param>
        /// <param name="nsn2">
        /// The NSN2.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(NamespaceNode nsn1, string nsn2)
        {
            if (!(nsn1 is null))
                return nsn1.Equals(nsn2);

            if (nsn1 is null && !(nsn2 is null))
                return nsn2.Equals(nsn1);

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

            if (obj is string nsnstring)
                return this._node == nsnstring;

            if (obj is NamespaceNode nsn)
            {
                return this.Root == nsn.Root && this._node == nsn._node;
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
            return this._node.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this._node;
        }

        #endregion Public Methods
    }
}