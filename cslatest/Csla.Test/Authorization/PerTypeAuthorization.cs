#if NUNIT
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#elif MSTEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif


namespace Csla.Test.Authorization
{
#if TESTING
  [DebuggerNonUserCode]
  [DebuggerStepThrough]
#endif
  [TestClass()]
  public class PerTypeAuthorizationTests
  {
    [TestMethod()]
    [ExpectedException(typeof(System.Security.SecurityException))]
    public void DenyWritePerType()
    {
      Csla.ApplicationContext.User = new Csla.Security.UnauthenticatedPrincipal();
      PerTypeAuthorization root = new PerTypeAuthorization();
      root.Test = "test";
    }

    [Test()]
    [ExpectedException(typeof(System.Security.SecurityException))]
    public void DenyWritePerInstance()
    {
      Csla.ApplicationContext.User = new Csla.Security.UnauthenticatedPrincipal();
      PerTypeAuthorization root = new PerTypeAuthorization();
      root.Name = "test";
    }
  }

}
