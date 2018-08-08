using Shiva.Core.Identities;
using Shiva.Xml;
using System;
using System.Globalization;
using System.Xml;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource definition base
    /// </summary>
    public abstract class RessourceBase : IRessource
    {
        #region Private Fields

        private CultureInfo _culture;
        private Identity _id;

        #endregion Private Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceBase" /> class.
        /// </summary>
        protected RessourceBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceBase" /> class.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        protected RessourceBase(Identity id, CultureInfo culture = null)
        {
            this._id = id ?? throw new ArgumentNullException(nameof(id));
            this._culture = culture;
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Gets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public CultureInfo Culture => this._culture;

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance has value; otherwise, <c> false </c>.
        /// </value>
        public abstract bool HasValue { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Identity Id => this._id;

        /// <summary>
        /// Gets a value indicating whether this instance is empty ressource.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is empty ressource; otherwise, <c> false </c>.
        /// </value>
        public bool IsEmptyRessource => this._id == null && this._culture == null;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        public abstract IRessource Clone();

        /// <summary>
        /// Serializes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="ctx">The CTX.</param>
        public void Serialize(XmlWriter writer, XmlContext ctx)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (ctx != null)
                writer.WriteStartElement(ctx.Prefix, "Value", ctx.Namespace);
            else
                writer.WriteStartElement("Value");
            writer.WriteAttributeString("lang", this.Culture.TwoLetterISOLanguageName);
            this.InternalSerialize(writer, ctx);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Internals the serialize.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="ctx">xml context</param>
        protected abstract void InternalSerialize(XmlWriter writer, XmlContext ctx);

        /// <summary>
        /// Sets the culture.
        /// </summary>
        /// <param name="culture">
        /// The culture.
        /// </param>
        public void SetCulture(CultureInfo culture)
        {
            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
        }

        /// <summary>
        /// Sets the identifier of ressource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void SetId(Identity id)
        {
            this._id = id ?? throw new ArgumentNullException(nameof(id));
        }
        /// <summary>
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="ctx">xml context</param>
        public void UnSerialize(XmlReader reader, XmlContext ctx)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (reader.LocalName=="Value" || reader.ReadToFollowing("Value"))
            {
                this._culture = CultureInfo.GetCultureInfo(reader.GetAttribute("lang"));
                this.InternalUnSerialize(reader, ctx);
            }
            else
                throw new InvalidOperationException("Invalid Reader, it's not found Value element in reader.");
        }

        /// <summary>
        /// Internals the serialize.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="ctx">The CTX.</param>
        protected abstract void InternalUnSerialize(XmlReader reader, XmlContext ctx);


        #endregion Public Methods

    }
}