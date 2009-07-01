#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif 

namespace Csla.Test.ObjectFactory
{
  [TestClass]
  public class ObjectFactoryTests
  {
    [TestMethod]
    public void Create()
    {
      Csla.ApplicationContext.User = new Csla.Security.UnauthenticatedPrincipal();
      Csla.ApplicationContext.DataPortalProxy = "Csla.Testing.Business.TestProxies.AppDomainProxy, Csla.Testing.Business";
      try
      {
        Csla.Server.FactoryDataPortal.FactoryLoader =
          new ObjectFactoryLoader();
        var root = Csla.DataPortal.Create<Root>();
        Assert.AreEqual("Create", root.Data, "Data should match");
        Assert.AreEqual(Csla.ApplicationContext.ExecutionLocations.Server, root.Location, "Location should match");
        Assert.IsTrue(root.IsNew, "Should be new");
        Assert.IsTrue(root.IsDirty, "Should be dirty");
      }
      finally
      {
        Csla.ApplicationContext.DataPortalProxy = "Local";
        Csla.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(
          new System.Security.Principal.GenericIdentity(string.Empty), new string[] { });
      }
    }

    [Test]
    public void CreateLocal()
    {
      Csla.ApplicationContext.DataPortalProxy = "Csla.Testing.Business.TestProxies.AppDomainProxy, Csla.Testing.Business";
      try
      {
        Csla.Server.FactoryDataPortal.FactoryLoader =
          new ObjectFactoryLoader();
        var root = Csla.DataPortal.Create<Root>(new SingleCriteria<Root, string>("abc"));
        Assert.AreEqual("Create abc", root.Data, "Data should match");
        Assert.AreEqual(Csla.ApplicationContext.ExecutionLocations.Client, root.Location, "Location should match");
        Assert.IsTrue(root.IsNew, "Should be new");
        Assert.IsTrue(root.IsDirty, "Should be dirty");
      }
      finally
      {
        Csla.ApplicationContext.DataPortalProxy = "Local";
        Csla.DataPortal.ResetProxyType();
      }
    }

    [Test]
    public void CreateMissing()
    {
      Csla.ApplicationContext.User = new Csla.Security.UnauthenticatedPrincipal();
      Csla.ApplicationContext.DataPortalProxy = "Csla.Testing.Business.TestProxies.AppDomainProxy, Csla.Testing.Business";
      try
      {
        Csla.Server.FactoryDataPortal.FactoryLoader =
          new ObjectFactoryLoader(1);
        var root = Csla.DataPortal.Create<Root>(new SingleCriteria<Root, string>("abc"));
        Assert.AreEqual("Create abc", root.Data, "Data should match");
        Assert.AreEqual(Csla.ApplicationContext.ExecutionLocations.Server, root.Location, "Location should match");
        Assert.IsTrue(root.IsNew, "Should be new");
        Assert.IsTrue(root.IsDirty, "Should be dirty");
      }
      finally
      {
        Csla.ApplicationContext.DataPortalProxy = "Local";
        Csla.DataPortal.ResetProxyType();
        Csla.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(
          new System.Security.Principal.GenericIdentity(string.Empty), new string[] { });
      }
    }

    [Test]
    public void FetchNoCriteria()
    {
      Csla.Server.FactoryDataPortal.FactoryLoader =
        new ObjectFactoryLoader();
      var root = Csla.DataPortal.Fetch<Root>();
      Assert.AreEqual("Fetch", root.Data, "Data should match");
      Assert.IsFalse(root.IsNew, "Should not be new");
      Assert.IsFalse(root.IsDirty, "Should not be dirty");
    }

    [Test]
    public void FetchCriteria()
    {
      Csla.Server.FactoryDataPortal.FactoryLoader =
        new ObjectFactoryLoader();
      var root = Csla.DataPortal.Fetch<Root>(new SingleCriteria<Root, string>("abc"));
      Assert.AreEqual("abc", root.Data, "Data should match");
      Assert.IsFalse(root.IsNew, "Should not be new");
      Assert.IsFalse(root.IsDirty, "Should not be dirty");
    }

    [Test]
    public void Update()
    {
      Csla.Server.FactoryDataPortal.FactoryLoader =
        new ObjectFactoryLoader();
      var root = new Root();
      root.Data = "abc";
      root = Csla.DataPortal.Update<Root>(root);
      Assert.AreEqual(TransactionalTypes.Manual, root.TransactionalType, "Transactional type should match");
      Assert.AreEqual("Update", root.Data, "Data should match");
      Assert.IsFalse(root.IsNew, "Should not be new");
      Assert.IsFalse(root.IsDirty, "Should not be dirty");
    }

    [Test]
    public void UpdateEnterpriseServices()
    {
      try
      {
        Csla.Server.FactoryDataPortal.FactoryLoader =
          new ObjectFactoryLoader(2);
        var root = new Root();
        root.Data = "abc";
        root = Csla.DataPortal.Update<Root>(root);
        Assert.AreEqual(TransactionalTypes.EnterpriseServices, root.TransactionalType, "Transactional type should match");
        Assert.AreEqual("Update", root.Data, "Data should match");
        Assert.IsFalse(root.IsNew, "Should not be new");
        Assert.IsFalse(root.IsDirty, "Should not be dirty");
      }
      catch (Csla.DataPortalException ex)
      {
        if (ex.InnerException.GetType().FullName == "System.EnterpriseServices.RegistrationException")
          Assert.Ignore("COM+ not accessible");
        else
          throw;
      }
    }

    [Test]
    public void UpdateTransactionScope()
    {
      Csla.Server.FactoryDataPortal.FactoryLoader =
        new ObjectFactoryLoader(1);
      var root = new Root();
      root.Data = "abc";
      root = Csla.DataPortal.Update<Root>(root);
      Assert.AreEqual(TransactionalTypes.TransactionScope, root.TransactionalType, "Transactional type should match");
      Assert.AreEqual("Update", root.Data, "Data should match");
      Assert.IsFalse(root.IsNew, "Should not be new");
      Assert.IsFalse(root.IsDirty, "Should not be dirty");
    }

    [Test]
    public void Delete()
    {
      Csla.ApplicationContext.GlobalContext.Clear();

      Csla.Server.FactoryDataPortal.FactoryLoader =
        new ObjectFactoryLoader();

      Csla.DataPortal.Delete<Root>(new SingleCriteria<Root, string>("abc"));

      Assert.AreEqual("Delete", Csla.ApplicationContext.GlobalContext["ObjectFactory"].ToString(), "Data should match");
    }

    [Test]
    public void FetchLoadProperty()
    {
      Csla.Server.FactoryDataPortal.FactoryLoader =
        new ObjectFactoryLoader(3);
      var root = Csla.DataPortal.Fetch<Root>();
      Assert.AreEqual("Fetch", root.Data, "Data should match");
      Assert.IsFalse(root.IsNew, "Should not be new");
      Assert.IsFalse(root.IsDirty, "Should not be dirty");
    }
  }
}
