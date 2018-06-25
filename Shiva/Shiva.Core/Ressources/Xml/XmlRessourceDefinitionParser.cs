using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XD = Shiva.Ressources.Xml.XmlRessourceDefinitions;

namespace Shiva.Ressources.Xml
{
    class XmlRessourceDefinitionParser : XmlRessourceNodeParser
    {
        public XmlRessourceDefinitionParser(XmlReader reader, XmlWriter writer) : base(reader, writer)
        {
        }

        public override void Parse(RessourcesEditInfo info)
        {
            this.Writer.WriteStartDocument();

            this.WriteToNextElement();

            if (this.Reader.EOF)
            {
                this.WriteNewFile(info);
            }
            else
            {
                if (this.Reader.LocalName == XD.ELEMENT_ROOT)
                {
                    this.Writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_ROOT, XD.NAMESPACE);
                    var parser = new XmlRessourcesParser(this.Reader, this.Writer);
                    parser.Parse(info);
                    this.Writer.WriteEndElement();
                }
                else
                    throw new InvalidOperationException("Invalid Xml File");
            }

            this.Writer.WriteEndDocument();
        }

        protected void WriteNewFile(RessourcesEditInfo info)
        {
            this.Writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_ROOT, XD.NAMESPACE);
            var parser = new XmlRessourcesParser(this.Reader, this.Writer);
            parser.Parse(info);
            this.Writer.WriteEndElement();
        }
    }
}
