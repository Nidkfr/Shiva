using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Permission.Xml
{    
    static class PermissionXmlDefinition
    {
        public const string RESSOURCE_SCHEMA = "Shiva.Permission.Xml.XmlPermission.xsd";

        public const string ELEMENT_ROOT = "PermissionsDefinitions";

        public const string ELEMENT_ROLES = "Roles";

        public const string ELEMENT_ROLE = "Role";

        public const string ELEMENT_PERMISSIONS = "Permissions";

        public const string ATTRIBUTE_ID = "id";

        public const string ATTRIBUTE_TYPE = "type";

        public const string NAMESPACE = "http://shiva.org/Permissions";

        public const string PREFIX = "xp";
    }
}
