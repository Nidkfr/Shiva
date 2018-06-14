using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shiva.Core.Services;
using log4net;
using System.IO;
using Shiva.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shiva
{
    [TestClass]
    [DeploymentItem("Log.xml")]
    public abstract class BaseTest
    {
        private static ILogManager _logm;
        private TestContext _testContextInstance;        
        private const string _SEPARATORTEST = "*****************************************************************************************************************";
        private const string _SEPARATORCLASS = "----------------------------------------------------------------------------------------------------------------";

        static BaseTest()
        {
            
        }

        public TestContext TestContext
        {
            get
            {
                return this._testContextInstance;
            }
            set
            {
                this._testContextInstance = value;
            }
        }

        public ILogManager LogManager => _logm;
              
        public static void ClassInit(TestContext context)
        {
            log4net.GlobalContext.Properties["LogName"] = context.FullyQualifiedTestClassName;
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Log.xml"));            
            _logm = new Log4NetLogManager();

            var _logger = _logm.CreateLogger(context.FullyQualifiedTestClassName);
            if (_logger.DebugIsEnabled)
            {
                _logger.Debug(_SEPARATORTEST);
                _logger.Debug(_SEPARATORCLASS);
                _logger.Debug($" Initialisation of {context.FullyQualifiedTestClassName} class Test.");
                _logger.Debug(_SEPARATORCLASS);
                _logger.Debug(_SEPARATORTEST);
            }
        }

        [TestInitialize]
        public void InitTest()
        {
            var _logger = _logm.CreateLogger(this.GetType());
            if (_logger.DebugIsEnabled)
            {
                _logger.Debug(_SEPARATORTEST);             
                _logger.Debug($" Initialisation of {this.TestContext.TestName} Test.");
                _logger.Debug($" DeployDirectory: {this.TestContext.DeploymentDirectory} TestDir{this.TestContext.TestDir}.");                
                _logger.Debug(_SEPARATORTEST);
             
            }
        }
    }
}
