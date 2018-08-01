using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Ressources.Xml
{
    /// <summary>
    /// Ressource Element xml definition
    /// </summary>
    static class RessourceXmlDefinitions
    {
        public const string ELEMENT_RESSOURCE = "Ressource";
        public const string ELEMENT_RESSOURCES = "Ressources";
        public const string ELEMENT_VALUE = "Value";
        public const string ELEMENT_GROUPS = "Groups";
        public const string ELEMENT_GROUP = "Group";
        public const string ATTRIBUTE_ID = "id";
        public const string ATTRIBUTE_TYPE = "type";
        public const string ATTRIBUTE_LANG = "lang";
        public const string RESSOURCE_SCHEMA = "Shiva.Ressources.Xml.XmlRessource.xsd";
        public const string ELEMENT_ROOT = "RessourcesDefinitions";
        public const string NAMESPACE = "http://shiva.org/Ressources";
        public const string PREFIX = "xr";
    }
}
