﻿using Shiva.Core.Identities;
using Shiva.Xml;
using System.Globalization;
using System.Xml;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource
    /// </summary>
    public interface IRessource : IIdentifiable, IInclusiveSerializable
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
        /// Change culture of ressource
        /// </summary>
        /// <param name="culture">
        /// The culture.
        /// </param>
        void SetCulture(CultureInfo culture);

        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void SetId(Identity id);
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