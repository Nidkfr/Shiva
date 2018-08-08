using Shiva.Core.Identities;
using Shiva.Xml;
using System;
using System.Globalization;
using System.Xml;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource Stream
    /// </summary>
    public sealed class RessourceBinary : RessourceBase, IRessource<byte[]>
    {
        #region Private Fields

        private byte[] _data;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceBinary" /> class.
        /// </summary>
        public RessourceBinary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceBinary" /> class.
        /// </summary>
        /// <param name="idRessource">
        /// The identifier ressource.
        /// </param>
        /// <param name="data">
        /// The stream.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        public RessourceBinary(Identity idRessource, byte[] data, CultureInfo culture = null) : base(idRessource, culture)
        {
            this._data = data ?? throw new ArgumentNullException(nameof(data));
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance is empty ressource.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is empty ressource; otherwise, <c> false </c>.
        /// </value>
        public override bool HasValue => this._data?.Length > 0;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public byte[] Value => this._data;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        public override IRessource Clone()
        {
            return ((IRessource<byte[]>)this).Clone();
        }       

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{{{this.GetType().FullName}::{this.Value.Length} byte}}";
        }

        /// <summary>
        /// Internals the serialize.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="ctx">xmlcontext</param>
        protected override void InternalSerialize(XmlWriter writer, XmlContext ctx)
        {
            writer.WriteValue(Convert.ToBase64String(this.Value));
        }

        /// <summary>
        /// Internals the serialize.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="ctx">xml context</param>
        protected override void InternalUnSerialize(XmlReader reader,XmlContext ctx)
        {
            var val = reader.ReadElementContentAsString();
            this._data = Convert.FromBase64String(val);
        }



        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        IRessource<byte[]> IRessource<byte[]>.Clone()
        {
            return new RessourceBinary(this.Id, this.Value, this.Culture);
        }

        #endregion Public Methods
    }
}