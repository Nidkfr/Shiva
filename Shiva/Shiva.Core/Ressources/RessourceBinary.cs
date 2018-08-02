using Shiva.Core.Identities;
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
        /// Serializes the specified writer.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void Serialize(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            writer.WriteStartElement("Data");
            writer.WriteValue(Convert.ToBase64String(this.Value));
            writer.WriteEndElement();
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
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        public override void UnSerialize(XmlReader reader, Identity id, CultureInfo culture)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (reader.ReadToDescendant("Data"))
            {
                var val = reader.ReadElementContentAsString();
                this._data = Convert.FromBase64String(val);
            }
            else
                throw new InvalidOperationException("Invalid Xml Ressource File");

            base.UnSerialize(reader, id, culture);
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