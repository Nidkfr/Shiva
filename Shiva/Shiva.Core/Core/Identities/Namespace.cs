﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Shiva.Core.Identities
{
    /// <summary>
    /// Namespace representation with tool
    /// </summary>
    public sealed class Namespace
    {
        #region Public Fields

        /// <summary>
        /// The namespace separator
        /// </summary>
        public const string NAMESPACESEPARATOR = ".";

        /// <summary>
        /// The null Namespace
        /// </summary>
        public static Namespace Null = new Namespace();

        #endregion Public Fields

        #region Private Fields

        private readonly LinkedList<NamespaceNode> _nodes = new LinkedList<NamespaceNode>();
        private string _ns;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace" /> class.
        /// </summary>
        /// <param name="ns">
        /// The ns.
        /// </param>
        public Namespace(string ns) : this()
        {
            if (string.IsNullOrWhiteSpace(ns))
                throw new ArgumentNullException(nameof(ns));
            this._ns = ns.TrimEnd(NAMESPACESEPARATOR[0]);
            this._decodeString(this._ns);
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Namespace" /> class from being created.
        /// </summary>
        private Namespace()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the last node.
        /// </summary>
        /// <value>
        /// The last node.
        /// </value>
        public NamespaceNode LastNode => this._nodes.Last();

        /// <summary>
        /// Gets the root node.
        /// </summary>
        /// <value>
        /// The root node.
        /// </value>
        public NamespaceNode RootNode => this._nodes.First();

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String" /> to <see cref="Namespace" />.
        /// </summary>
        /// <param name="ns">
        /// The ns.
        /// </param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Namespace(string ns)
        {
            if (string.IsNullOrWhiteSpace(ns))
                return Namespace.Null;

            return new Namespace(ns);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Namespace" /> to <see cref="string" />.
        /// </summary>
        /// <param name="ns">
        /// The ns.
        /// </param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(Namespace ns)
        {
            return ns.ToString();
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="ns1">
        /// The NS1.
        /// </param>
        /// <param name="ns2">
        /// The NS2.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Namespace ns1, Namespace ns2)
        {
            if (!(ns1 is null))
                return !ns1.Equals(ns2);

            if (ns1 is null && !(ns2 is null))
                return !ns2.Equals(ns1);

            return false;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="ns1">
        /// The NS1.
        /// </param>
        /// <param name="ns2">
        /// The NS2.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Namespace ns1, Namespace ns2)
        {
            if (!(ns1 is null))
                return ns1.Equals(ns2);

            if (ns1 is null && !(ns2 is null))
                return ns2.Equals(ns1);

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

            Namespace current = null;

            if (obj is string nsstring)
                current = new Namespace(nsstring);

            if (obj is Namespace ns)
                current = ns;

            if (current == null)
            {
                return object.ReferenceEquals(this, Namespace.Null);
            }

            return this.ToString() == current.ToString();
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
        /// Gets the next node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// </returns>
        public NamespaceNode GetNextNode(NamespaceNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            return this._nodes.Find(node)?.Next?.Value;
        }

        /// <summary>
        /// Ins the namespace.
        /// </summary>
        /// <param name="ns">
        /// The ns.
        /// </param>
        /// <returns>
        /// </returns>
        public bool InNamespace(Namespace ns)
        {
            var currentnode = this.RootNode;
            var ext_currentnode = ns.RootNode;

            while (currentnode == ext_currentnode)
            {
                if (currentnode.NextNode == null)
                    break;
                currentnode = currentnode.NextNode;

                if (ext_currentnode.NextNode == null)
                    return false;
                ext_currentnode = ext_currentnode.NextNode;
            }
            return currentnode == ext_currentnode;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this._ns;
        }

        #endregion Public Methods

        #region Private Methods

        private void _decodeString(string ns)
        {
            foreach (var node in ns.Split(NAMESPACESEPARATOR[0]))
            {
                this._nodes.AddLast(new NamespaceNode(this, node.Trim()));
            }
            this._ns = string.Join(Namespace.NAMESPACESEPARATOR, this._nodes);
        }

        #endregion Private Methods
    }
}