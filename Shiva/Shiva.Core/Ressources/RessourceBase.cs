using Shiva.Core.Identities;
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
        /// <param name="writer">
        /// The writer.
        /// </param>
        public abstract void Serialize(XmlWriter writer);

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
        public virtual void UnSerialize(XmlReader reader, Identity id, CultureInfo culture)
        {
            this._id = id;
            this._culture = culture;
        }

        #endregion Public Methods

    }
}