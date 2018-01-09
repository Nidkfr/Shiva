using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shiva.Core.Services;
using log4net;
using System.IO;
using Shiva.Services;

namespace Shiva
{
    public class BaseTest
    {
        private static ILogManager _logm;

        static BaseTest()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Log.xml"));
            _logm = new Log4NetLogManager();
        }

        public ILogManager LogManager => _logm;
    }
}
