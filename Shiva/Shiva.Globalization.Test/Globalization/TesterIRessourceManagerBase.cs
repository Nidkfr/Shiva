using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shiva.Globalization
{
    /// <summary>
    /// 
    /// </summary>
    public class TesterIRessourceManagerBase
    {
        public void TestChangeCulture(IRessourceManager manager)
        {
            var old = CultureInfo.CurrentCulture;
            var oldui = CultureInfo.CurrentUICulture;

            manager.MonitorEvents();
            manager.SetCulture(CultureInfo.GetCultureInfo(1));
            manager.ShouldRaise("CultureChanged").WithSender(manager).WithArgs<RessourceManagerCultureArgs>(x => x.NewCulture == CultureInfo.GetCultureInfo(1) && x.OldCulture == old);
            Assert.IsTrue(manager.CurrentCulture == CultureInfo.GetCultureInfo(1));

            manager.MonitorEvents();
            manager.SetUICulture(CultureInfo.GetCultureInfo(5));
            manager.ShouldRaise("CultureUIChanged").WithSender(manager).WithArgs<RessourceManagerCultureArgs>(x => x.NewCulture == CultureInfo.GetCultureInfo(1) && x.OldCulture == old);
            Assert.IsTrue(manager.CurrentUICulture == CultureInfo.GetCultureInfo(5));

        }
    }

    
    public interface ITesterIRessourceManagerBase
    {
        TesterIRessourceManagerBase Tester { get; }

        void TestChangeCulture();
    }

}
