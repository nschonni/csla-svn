#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif 

namespace Csla.Test.IO
{
    [TestClass]
    public class IOTests
    {
        [TestMethod]
        public void SaveNewRoot()
        {
            Csla.ApplicationContext.GlobalContext.Clear();
            Csla.Test.Basic.Root root = Csla.Test.Basic.Root.NewRoot();

            root.Data = "saved";
            Assert.AreEqual("saved", root.Data);
            Assert.AreEqual(true, root.IsDirty);
            Assert.AreEqual(true, root.IsValid);

            Csla.ApplicationContext.GlobalContext.Clear();
            root = root.Save();

            Assert.IsNotNull(root);
            //fails because no call is being made to DataPortal_Insert in Root.DataPortal_Update if IsDeleted == false and IsNew == true
            Assert.AreEqual("Inserted", Csla.ApplicationContext.GlobalContext["Root"]);  
            Assert.AreEqual("saved", root.Data);
            Assert.AreEqual(false, root.IsNew, "IsNew");
            Assert.AreEqual(false, root.IsDeleted, "IsDeleted");
            Assert.AreEqual(false, root.IsDirty, "IsDirty");
        }
        [Test]
        public void SaveOldRoot()
        {
            Csla.ApplicationContext.GlobalContext.Clear();
            Csla.Test.Basic.Root root = Csla.Test.Basic.Root.GetRoot("old");

            root.Data = "saved";
            Assert.AreEqual("saved", root.Data);
            Assert.AreEqual(true, root.IsDirty, "IsDirty");
            Assert.AreEqual(true, root.IsValid, "IsValid");

            Csla.ApplicationContext.GlobalContext.Clear();
            root = root.Save();

            Assert.IsNotNull(root);
            Assert.AreEqual("Updated", Csla.ApplicationContext.GlobalContext["Root"]);
            Assert.AreEqual("saved", root.Data);
            Assert.AreEqual(false, root.IsNew, "IsNew");
            Assert.AreEqual(false, root.IsDeleted, "IsDeleted");
            Assert.AreEqual(false, root.IsDirty, "IsDirty");
        }
        [Test]
        public void LoadRoot()
        {
            Csla.ApplicationContext.GlobalContext.Clear();
            Csla.Test.Basic.Root root = Csla.Test.Basic.Root.GetRoot("loaded");
            Assert.IsNotNull(root);
            Assert.AreEqual("Fetched", Csla.ApplicationContext.GlobalContext["Root"]);
            Assert.AreEqual("loaded", root.Data);
            Assert.AreEqual(false, root.IsNew);
            Assert.AreEqual(false, root.IsDeleted);
            Assert.AreEqual(false, root.IsDirty);
            Assert.AreEqual(true, root.IsValid);
        }

        [Test]
        public void DeleteNewRoot()
        {
            Csla.ApplicationContext.GlobalContext.Clear();
            Csla.Test.Basic.Root root = Csla.Test.Basic.Root.NewRoot();

            Csla.ApplicationContext.GlobalContext.Clear();
            root.Delete();
            Assert.AreEqual(true, root.IsNew);
            Assert.AreEqual(true, root.IsDeleted);
            Assert.AreEqual(true, root.IsDirty);

            root = root.Save();
            Assert.IsNotNull(root);
            Assert.AreEqual(null, Csla.ApplicationContext.GlobalContext["Root"]);
            Assert.AreEqual(true, root.IsNew);
            Assert.AreEqual(false, root.IsDeleted);
            Assert.AreEqual(true, root.IsDirty);
        }

        [Test]
        public void DeleteOldRoot()
        {
            Csla.ApplicationContext.GlobalContext.Clear();
            Csla.Test.Basic.Root root = Csla.Test.Basic.Root.GetRoot("old");

            Csla.ApplicationContext.GlobalContext.Clear();
            root.Delete();
            Assert.AreEqual(false, root.IsNew);
            Assert.AreEqual(true, root.IsDeleted);
            Assert.AreEqual(true, root.IsDirty);

            root = root.Save();
            Assert.IsNotNull(root);
            Assert.AreEqual("Deleted self", Csla.ApplicationContext.GlobalContext["Root"]);
            Assert.AreEqual(true, root.IsNew);
            Assert.AreEqual(false, root.IsDeleted);
            Assert.AreEqual(true, root.IsDirty);
        }

        [Test]
        public void DeleteRootImmediate()
        {
            Csla.ApplicationContext.GlobalContext.Clear();
            Csla.Test.Basic.Root.DeleteRoot("test");
            Assert.AreEqual("Deleted", Csla.ApplicationContext.GlobalContext["Root"]);
        }

    }
}
