﻿#if NUNIT
using NUnit.Framework;
using UnitDriven;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestSetup = NUnit.Framework.SetUpAttribute;
#elif MSTEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace cslalighttest.NameValueList
{
  [TestClass]
  public class NameValueListTests : TestBase
  {
    [TestMethod]
    public void TestNameValueList()
    {
#if SILVERLIGHT
      UnitTestContext context = GetContext();
      DataPortal.ProxyTypeName = typeof(SynchronizedWcfProxy<>).AssemblyQualifiedName;
      WcfProxy.DefaultUrl = cslalighttest.Properties.Resources.RemotePortalUrl;
      BasicNameValueList.GetBasicNameValueList((o, e) =>
        {
          context.Assert.AreEqual(10, e.Object.Count, "Items are not caught via fetch");
          context.Assert.AreEqual("element_2", e.Object.GetItemByKey(2).Value, "NameValue collection fetch failed.");
          context.Assert.Success();
        });
      context.Complete();
#else

      BasicNameValueList list = BasicNameValueList.GetBasicNameValueList();
      Assert.AreEqual(10, list.Count, "Items are not caught via fetch");
      Assert.AreEqual("element_2", list.GetItemByKey(2).Value, "NameValue collection fetch failed.");
#endif
    }
  }
}
