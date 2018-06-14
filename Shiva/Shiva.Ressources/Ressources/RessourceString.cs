using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource String
    /// </summary>    
    public class RessourceString :RessourceBase, IRessource<string>
    {
        private readonly string _value;       

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceString"/> class.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="value">The value.</param>
        /// <param name="culture">culture</param>
        public RessourceString(Identity id, string value,CultureInfo culture=null):base(id,culture)
        {
            this._value = value;           
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value => this._value;

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override IRessource Clone()
        {
            return ((IRessource<string>)this).Clone();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        IRessource<string> IRessource<string>.Clone()
        {
            return new RessourceString(this.Id, this.Value, this.Culture);
        }
    }
}
