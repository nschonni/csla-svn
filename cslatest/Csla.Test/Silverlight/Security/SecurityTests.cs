﻿#if NUNIT
using UnitDriven;
using TestClass = NUnit.Framework.TestFixtureAttribute;

#elif MSTEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Csla.Test.Silverlight.Security
{
  [TestClass]
  public partial class SecurityTests : TestBase
  {
    protected string AdminRoleName = "Admin Role";
    protected string WcfProxyTypeName = "Csla.DataPortalClient.SynchronizedWcfProxy`1, Csla, Version=3.6.0.0, Culture=neutral, PublicKeyToken=93be5fdc093e4c30";

  }
}
