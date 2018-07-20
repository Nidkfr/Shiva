using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource definition base
    /// </summary>
    public abstract class RessourceBase : IRessource
    {
        private  Identity _id;
        private CultureInfo _culture;

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceBase"/> class.
        /// </summary>
        protected RessourceBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceBase"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="culture">The culture.</param>
        protected RessourceBase(Identity id, CultureInfo culture = null)
        {
            this._id = id ?? throw new ArgumentNullException(nameof(id));
            this._culture = culture;
        }

        /// <summary>
        /// Gets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public CultureInfo Culture => this._culture;

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
        ///   <c>true</c> if this instance is empty ressource; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmptyRessource => this._id == null && this._culture == null;

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public abstract bool HasValue { get; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public abstract IRessource Clone();

        /// <summary>
        /// Serializes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public abstract void Serialize(XmlWriter writer);


        /// <summary>
        /// Sets the culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public void SetCulture(CultureInfo culture)
        {
            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
        }

        /// <summary>
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="culture">The culture.</param>
        public virtual void UnSerialize(XmlReader reader, Identity id, CultureInfo culture)
        {
            this._id = id;
            this._culture = culture;
        }        
    }
}
