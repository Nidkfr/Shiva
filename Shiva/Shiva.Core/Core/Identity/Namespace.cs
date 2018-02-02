using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Core.Identity
{
    /// <summary>
    /// Namespace representation with tool
    /// </summary>
    public sealed class Namespace
    {
        private  IList<string> _nodes = new List<string>();

        /// <summary>
        /// The null Namespace
        /// </summary>
        public static Namespace Null = new Namespace();

        /// <summary>
        /// Prevents a default instance of the <see cref="Namespace"/> class from being created.
        /// </summary>
        private Namespace()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        public Namespace(string ns):this()
        {
            if (string.IsNullOrWhiteSpace(ns))
                throw new ArgumentNullException(nameof(ns));

            this._decodeString(ns);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Namespace"/> to <see cref="string"/>.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(Namespace ns)
        {
            return ns.ToString();
        }

        public static implicit operator Namespace(string ns)
        {
            if (string.IsNullOrWhiteSpace(ns))
                return Namespace.Null;
        }

        public static explicit operator String(Namespace ns)
        {

        }

        public static explicit operator Namespace(string ns)
        {
            if (string.IsNullOrWhiteSpace(ns))
                return Namespace.Null;
        }

        public static bool operator ==(Namespace ns1, string ns2)
        {


        }

        public static bool operator !=(Namespace ns1, string ns2)
        {


        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private void _decodeString(string ns)
        {
            foreach (var node in ns.Split('.'))
            {
                this._nodes.Add(node);
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
