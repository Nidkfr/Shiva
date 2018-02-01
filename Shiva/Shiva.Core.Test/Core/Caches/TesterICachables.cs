using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shiva.Mocks;

namespace Shiva.Core.Caches
{
   public  class TesterICachables
    {
        public void TestLoadCaches(ICachable cache,int timeout)
        {
            Assert.IsFalse(cache.IsCached);
            cache.MonitorEvents();
            var t = cache.LoadCache();
            t.Wait(timeout);
            Assert.IsTrue(cache.IsCached);
            cache.ShouldRaise("Cached").WithSender(cache).WithArgs<CachableArg>(x => x != null);
        }
    }

    public interface ITesterICachables
    {
        TesterICachables Tester { get; }
    }
}
