using Shiva.Core.Identities;
using System.Globalization;
using System.Xml;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource
    /// </summary>
    public interface IRessource : IIdentifiable
    {
        #region Public Properties

        /// <summary>
        /// Gets the culture of ressource.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is initialized; otherwise, <c> false </c>.
        /// </value>
        bool IsEmptyRessource { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        IRessource Clone();

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <param name="writer">
        /// writer
        /// </param>
        /// <returns>
        /// </returns>
        void Serialize(XmlWriter writer);

        /// <summary>
        /// Change culture of ressource
        /// </summary>
        /// <param name="culture">
        /// The culture.
        /// </param>
        void SetCulture(CultureInfo culture);

        /// <summary>
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <param name="info">
        /// The information.
        /// </param>
        void UnSerialize(XmlReader reader, Identity id, CultureInfo info);

        #endregion Public Methods
    }

    /// <summary>
    /// REssource typed
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of value
    /// </typeparam>
    public interface IRessource<TValue> : IRessource
    {
        #region Public Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        TValue Value { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        new IRessource<TValue> Clone();

        #endregion Public Methods
    }
}