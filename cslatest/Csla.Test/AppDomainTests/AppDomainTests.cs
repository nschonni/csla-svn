#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using System;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif 

namespace Csla.Test.AppDomainTests
{
    [TestClass]
    public class  AppDomainTestClass
    {
        [TestMethod]
        public void AppDomainTestIsCalled()
        {
            Csla.ApplicationContext.GlobalContext.Clear();
            int local= AppDomain.CurrentDomain.Id;
            Basic.Root r = Basic.Root.NewRoot();
            int remote = r.CreatedDomain;

            if (System.Configuration.ConfigurationManager.AppSettings["CslaDataPortalProxy"] == null)
              Assert.AreEqual(local, remote, "Local and Remote AppDomains should be the same");
            else
              Assert.IsFalse((local == remote), "Local and Remote AppDomains should be different");
  
        }

    }
}
