using Shiva.Core.Identities;
using System;
using System.Globalization;
using System.Xml;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource String
    /// </summary>
    public sealed class RessourceString : RessourceBase, IRessource<string>
    {
        #region Private Fields

        private string _value;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceString" /> class.
        /// </summary>
        public RessourceString()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceString" /> class.
        /// </summary>
        /// <param name="id">
        /// id
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="culture">
        /// culture
        /// </param>
        public RessourceString(Identity id, string value, CultureInfo culture = null) : base(id, culture)
        {
            this._value = value;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is initialized; otherwise, <c> false </c>.
        /// </value>
        public override bool HasValue => !string.IsNullOrEmpty(this._value);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value => !this.HasValue && !this.IsEmptyRessource ? $"[{this.GetType().FullName}]{{{this.Id}:{this.Culture.TwoLetterISOLanguageName}}}" : this._value;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        public override IRessource Clone()
        {
            return ((IRessource<string>)this).Clone();
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
            writer.WriteStartElement("String");
            writer.WriteValue(this.Value);
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
            return $"{{{this.GetType().FullName}::{this.Value}}}";
        }

        /// <summary>
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="id">
        /// </param>
        /// <param name="culture">
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// Invalid Xml Ressource File
        /// </exception>
        public override void UnSerialize(XmlReader reader, Identity id, CultureInfo culture)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            base.UnSerialize(reader, id, culture);
            if (reader.ReadToDescendant("String"))
            {
                this._value = reader.ReadElementContentAsString();
            }
            else
                throw new InvalidOperationException("Invalid Xml Ressource File");
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        IRessource<string> IRessource<string>.Clone()
        {
            return new RessourceString(this.Id, this.Value, this.Culture);
        }

        #endregion Public Methods
    }
}