using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using XD = Shiva.Ressources.Xml.XmlRessourceDefinitions;

namespace Shiva.Ressources.Xml
{
    internal class XmlRessourcesParser : XmlRessourceNodeParser
    {
        public XmlRessourcesParser(XmlReader reader, XmlWriter writer) : base(reader, writer)
        {
        }

        public override void Parse(RessourcesEditInfo info)
        {
            this.Writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCES, XD.NAMESPACE);
            this.WriteToNextElement();

            if(this.Reader.EOF || this.Reader.NodeType == XmlNodeType.EndElement)
            {
                this.WriteNewRessources(info);
            }
            else
            {
                this.WriteToNextElement();
                while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.Name != XD.ELEMENT_RESSOURCES)
                {
                    
                    var id = this.Reader.GetAttribute(XD.ATTRIBUTE_ID);
                    var type = this.Reader.GetAttribute(XD.ATTRIBUTE_TYPE);

                    if(!info.RemovedRessources.Any(x=>x.Key.FullName == type && x.Value == id))
                    {
                        this.Writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCE, XD.NAMESPACE);
                        this.Writer.WriteStartAttribute(XD.PREFIX,XD.ATTRIBUTE_ID,XD.NAMESPACE);
                        this.Writer.WriteValue(id);
                        this.Writer.WriteEndAttribute();
                        this.Writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_TYPE, XD.NAMESPACE);
                        this.Writer.WriteValue(type);
                        this.Writer.WriteEndAttribute();

                        if(info.AddedRessources.Any(x=>x.Id == id))
                        {
                            var ressource = info.AddedRessources.First(x => x.Id == id);
                            while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.Name != XD.ELEMENT_RESSOURCE)
                            {
                                var lang = this.Reader.GetAttribute(XD.ATTRIBUTE_LANG);
                                this.Writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_VALUE, XD.NAMESPACE);                                
                                this.Writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_LANG, XD.NAMESPACE);
                                this.Writer.WriteValue(lang);
                                this.Writer.WriteEndAttribute();

                                if (lang == ressource.Culture.TwoLetterISOLanguageName)
                                {
                                    ressource.Serialize(this.Writer);
                                       
                                }
                                else
                                {
                                    this.Writer.WriteNode(this.Reader, false);
                                }
                                this.Writer.WriteEndElement();
                            }
                        }
                        else
                        {
                            this.WriteAllChildren(XD.ELEMENT_RESSOURCES);
                        }
                        this.Writer.WriteEndElement();
                    }                
                }
                this.WriteNewRessources(info);
            }

            this.Writer.WriteEndElement();
        }

        public void WriteNewRessources(RessourcesEditInfo info)
        {
            foreach (var ressource in info.AddedRessources)
            {
                this.WriteRessource(ressource);
            }
        }

        public void WriteRessource(IRessource ressource)
        {
            this.Writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCE, XD.NAMESPACE);
            //id
            this.Writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_ID, XD.NAMESPACE);
            this.Writer.WriteValue(ressource.Id);
            this.Writer.WriteEndAttribute();
            //type
            this.Writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_TYPE, XD.NAMESPACE);
            this.Writer.WriteValue(ressource.GetType().FullName);
            this.Writer.WriteEndAttribute();
            //value
            this.Writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_VALUE, XD.NAMESPACE);
            //lang
            this.Writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_LANG, XD.NAMESPACE);
            this.Writer.WriteValue(ressource.Culture.TwoLetterISOLanguageName);
            this.Writer.WriteEndAttribute();
            //inner value
            ressource.Serialize(this.Writer);

            this.Writer.WriteEndElement();
        }
    }
}
