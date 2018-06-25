using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Shiva.Core.Identities;
using System.Globalization;
using System.Xml.Linq;
using System.Xml;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource Stream
    /// </summary>    
    public class RessourceBinary : RessourceBase, IRessource<byte[]>
    {       
        private readonly byte[] _data;       

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceBinary"/> class.
        /// </summary>
        /// <param name="idRessource">The identifier ressource.</param>
        /// <param name="data">The stream.</param>
        /// <param name="culture">The culture.</param>
        public RessourceBinary(Identity idRessource, byte[] data, CultureInfo culture=null):base(idRessource,culture)
        {            
            this._data = data ?? throw new ArgumentNullException(nameof(data));            
        }        

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public byte[] Value => this._data;

       

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override IRessource Clone()
        {
            return ((IRessource<byte[]>)this).Clone();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        IRessource<byte[]> IRessource<byte[]>.Clone()
        {
            return new RessourceBinary(this.Id, this.Value, this.Culture);
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
        /// Serializes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public override void Serialize(XmlWriter writer)
        {
            writer.WriteStartElement("Data");
            writer.WriteValue(Convert.ToBase64String(this.Value));
            writer.WriteEndElement();
        }

        /// <summary>
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override void UnSerialize(XmlReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
