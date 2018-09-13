using Shiva.Core.IO;
using Shiva.Permission;
using Shiva.Ressources;
using Shiva.Ressources.Xml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiva.Performance.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var streamsource = new FileSource("./RessourceXml.xml"))
            {
                using (var managerEn = new RessourceXmlService(null))
                {
                    var total = 100;

                    managerEn.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    var currrentdate = DateTime.Now;
                    Console.WriteLine($"{currrentdate} Start Generation Ressource string");
                    for (var i = 1; i <= total; i++)
                    {
                        managerEn.SetRessource(new RessourceString($"Test.Ressource{i}", $"Test value {i}"));
                    }
                    Console.WriteLine($"{DateTime.Now} End Generation Ressource string duration {(DateTime.Now - currrentdate).TotalSeconds}");

                    currrentdate = DateTime.Now;
                    managerEn.Flush();
                    Console.WriteLine($"{DateTime.Now} flush Ressource string duration {(DateTime.Now - currrentdate).TotalSeconds}");

                    currrentdate = DateTime.Now;
                    var r = managerEn.GetRessource<RessourceString>($"Test.Ressource{total}");
                    Console.WriteLine($"{DateTime.Now} read Ressource string duration {(DateTime.Now - currrentdate).TotalSeconds}");

                    currrentdate = DateTime.Now;
                    for (var i = 1; i <= total; i++)
                    {
                        if(i%10 == 0)
                        managerEn.AttachRessourceToGroup(new RessourceString($"Test.Ressource{i}", $"Test value {i}"),"group");
                    }
                    Console.WriteLine($"{DateTime.Now} Attach ressource to group without flush {(DateTime.Now - currrentdate).TotalSeconds}");

                    currrentdate = DateTime.Now;
                    managerEn.Flush();
                    Console.WriteLine($"{DateTime.Now} Flush group {(DateTime.Now - currrentdate).TotalSeconds}");

                    currrentdate = DateTime.Now;
                    var grp = managerEn.GetGroupRessources<RessourceString>("group");
                    Console.WriteLine($"{DateTime.Now} get group with {grp.Ressources.Count} elements {(DateTime.Now - currrentdate).TotalSeconds}");                   

                    Console.Read();                   
                }
            }

            using (var streamsource = new FileSource("./Permission.xml"))
            {
                using (var managerPermission = new Shiva.Permission.Xml.PermissionManagerXml(null))
                {
                    var total = 100;
                    managerPermission.Initialize(streamsource);

                    Console.WriteLine("test Permission");
                    var currrentdate = DateTime.Now;

                    for (var i = 0; i < total; i++)
                    {
                        var role = new Role($"test{i}");
                        for (var j = 0; j < total; j++)
                        {
                            role.SetPermission(new PermissionAccess($"A{j}") { Acces = true });
                        }
                        for (var j = 0; j < total; j++)
                        {
                            role.SetPermission(new PermissionData($"D{j}") { Mode = PermissionDataEnum.EDITABLE });
                        }
                        managerPermission.SetRole(role);
                    }
                    Console.WriteLine($"Write {total} role with {total} permission Acces and {total} permission mode duration {(DateTime.Now - currrentdate).TotalSeconds}");
                    currrentdate = DateTime.Now;
                    var r = managerPermission.GetRole<Role>($"test{total - 1}");
                    if (r != null)
                        Console.WriteLine($"get Role with {r.GetPermissions().Count()} permission duration {(DateTime.Now - currrentdate).TotalSeconds}");
                    else
                        Console.WriteLine("Fail");

                    Console.Read();
                }
            }
            Console.Read();
        }
    }
}
