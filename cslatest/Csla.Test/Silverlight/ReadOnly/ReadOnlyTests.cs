#if NUNIT
using System;
using NUnit.Framework;
using UnitDriven;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#elif MSTEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace cslalighttest.ReadOnly
{
#if TESTING
  [DebuggerStepThrough]
#endif
  [TestClass]
  public class ReadOnlyTests : TestBase
  {
      [TestMethod]
      [ExpectedException(typeof(NotSupportedException))]
      public void AddItemToReadOnlyFail()
      {
          UnitTestContext context = GetContext();
          MockReadOnlyList list = new MockReadOnlyList();
          MockReadOnly mock = new MockReadOnly();
          context.Assert.Try((Action)(() => list.Add(mock)));
          context.Complete();
      }

    [Test]
    [ExpectedException(typeof(NotSupportedException))]
    public void InsertItemToReadOnlyFail()
    {
      UnitTestContext context = GetContext();
      MockReadOnlyList list = new MockReadOnlyList();
      MockReadOnly mock = new MockReadOnly();
      context.Assert.Try((Action)(() => list.Insert(0, mock)));
      context.Complete();
    }

    [Test]
    [ExpectedException(typeof(NotSupportedException))]
    public void IndexInsertItemToReadOnlyFail()
    {
      UnitTestContext context = GetContext();
      MockReadOnlyList list = new MockReadOnlyList(new MockReadOnly());
      context.Assert.Try((Action)(() => list[0] = new MockReadOnly()));
      context.Complete();
    }

    [Test]
    [ExpectedException(typeof(NotSupportedException))]
    public void AddNewToReadOnlyFail()
    {
      UnitTestContext context = GetContext();
      MockReadOnlyList list = new MockReadOnlyList();
      context.Assert.Try((Action)(() => list.AddNew()));
      context.Complete();
    }

    [Test]
    [ExpectedException(typeof(NotSupportedException))]
    public void ClearReadOnlyFail()
    {
      UnitTestContext context = GetContext();
      MockReadOnlyList list = new MockReadOnlyList();
      context.Assert.Try((Action)(() => list.Clear()));
      context.Complete();
    }
  }
}
