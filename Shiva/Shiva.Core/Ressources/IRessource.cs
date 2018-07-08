﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Shiva.Core.Identities;
using System.Xml.Linq;
using System.Xml;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource
    /// </summary>
    public interface IRessource:IIdentifiable
    {
        /// <summary>
        /// Gets the culture of ressource.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        CultureInfo Culture { get; }

       

        /// <summary>
        /// Change culture of ressource
        /// </summary>
        /// <param name="culture">The culture.</param>
        void SetCulture(CultureInfo culture);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        IRessource Clone();

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <returns></returns>
        void Serialize(XmlWriter writer);

        /// <summary>
        /// Unserialize.
        /// </summary>
        /// <param name="reader">The value.</param>
        void UnSerialize(XmlReader reader, Identity id, CultureInfo info);

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        bool IsEmptyRessource { get; }
    }

    /// <summary>
    /// REssource typed
    /// </summary>
    /// <typeparam name="TValue">Type of value</typeparam>
    public interface IRessource<TValue> : IRessource
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        TValue Value { get; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        new IRessource<TValue> Clone();
    }
}