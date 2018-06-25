using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Shiva.Ressources.Xml
{
    abstract class XmlRessourceNodeParser
    {
        private readonly XmlReader _reader;
        private readonly XmlWriter _writer;

        protected XmlRessourceNodeParser(XmlReader reader,XmlWriter writer)
        {
            this._reader = reader;
            this._writer = writer;
        }

        protected void WriteToNextElement()
        {
            while(this._reader.Read())
            {
                switch(this._reader.NodeType)
                {
                    case XmlNodeType.Comment:
                        this._writer.WriteComment(this._reader.Value);
                        break;
                    case XmlNodeType.CDATA:
                        this._writer.WriteCData(this._reader.Value);
                        break;
                    case XmlNodeType.EndElement:
                    case XmlNodeType.Element:return;
                }
            }            
        }

        protected void WriteAllChildren(string elementName)
        {
            while (true)
            {
                this.WriteToNextElement();
                if (this._reader.NodeType == XmlNodeType.EndElement && this._reader.LocalName == elementName)
                    return;
            }
        }

        protected XmlReader Reader => this._reader;

        protected XmlWriter Writer => this._writer;

        public abstract void Parse(RessourcesEditInfo info);
    }
}
