#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif 

namespace Csla.Test.Basic
{
  [TestClass]
  public class CollectionTests
  {
    [TestMethod]
    public void SetLast()
    {
      TestCollection list = new TestCollection();
      list.Add(new TestItem());
      list.Add(new TestItem());
      TestItem oldItem = new TestItem();
      list.Add(oldItem);
      TestItem newItem = new TestItem();
      list[2] = newItem;
      Assert.AreEqual(3, list.Count, "List should have 3 items");
      Assert.AreEqual(newItem, list[2], "Last item should be newItem");
      Assert.AreEqual(true, list.ContainsDeleted(oldItem), "Deleted list should have old item");
    }

    [Test]
    public void RootListGetRuleDescriptions()
    {
      RootList list = new RootList();
      RootListChild child = list.AddNew();
      string[] rules = child.GetRuleDescriptions();
    }

    [Test]
    public void DoubleAdd()
    {
      var someObj = new TestIndexableItem();
      var someCol = new TestBusinessListBaseCollection(1);
      someCol.Add(someObj);
      someCol.Add(someObj);
      Assert.IsTrue(true);
    }

    [Test]
    public void IndexOnReadOnlyWorks()
    {
      var sampleSize = 100000;
      Console.WriteLine("Creating " + sampleSize + " element collection...");
      var readOnlyCollection = new TestReadOnlyCollection(sampleSize);
      Console.WriteLine("Collection established.");
      
      //first query establishes the index
      var controlSet = readOnlyCollection.ToList();

      var primeQuery = from i in readOnlyCollection where i.IndexedString == "42" select i;
      var forcedPrimeITeration = primeQuery.ToArray();

      Stopwatch watch = new Stopwatch();

      watch.Start();
      var indexedQuery = from i in readOnlyCollection where i.IndexedString == "42" select i;
      var forcedIterationIndexed = indexedQuery.ToArray();
      watch.Stop();

      var indexedRead = watch.ElapsedMilliseconds;

      watch.Reset();
      
      watch.Start();
      var nonIndexedQuery = from i in readOnlyCollection where i.NonIndexedString == "42" select i;
      var forcedIterationNonIndexed = nonIndexedQuery.ToArray();
      watch.Stop();
      
      var nonIndexedRead = watch.ElapsedMilliseconds;

      watch.Reset();

      watch.Start();
      var controlQuery = from i in controlSet where i.IndexedString == "42" select i;
      var forcedControlIteration = controlQuery.ToArray();
      watch.Stop();

      var controlRead = watch.ElapsedMilliseconds;

      
      Console.WriteLine("Sample size = " + sampleSize);
      Console.WriteLine("Indexed Read = " + indexedRead + "ms");
      Console.WriteLine("Non-Indexed Read = " + nonIndexedRead + "ms");
      Console.WriteLine("Standard Linq-to-objects Read = " + controlRead + "ms");
      //Assert.IsTrue(indexedRead < nonIndexedRead);
      Assert.IsTrue(forcedIterationIndexed.Count() == forcedIterationNonIndexed.Count());
    }

    [Test]
    public void TestRangedWhereGreaterThan()
    {
      var blbCollection = new TestBusinessListBaseCollection(50,1,true);
      blbCollection.Add(new TestIndexableItem() { IndexedInt = 499, IndexedString = "499", NonIndexedString = "499" });
      blbCollection.Add(new TestIndexableItem() { IndexedInt = 500, IndexedString = "500", NonIndexedString = "500" });
      blbCollection.Add(new TestIndexableItem() { IndexedInt = 600, IndexedString = "600", NonIndexedString = "600" });
      var partsOver500 = from i in blbCollection where i.IndexedInt > 499 select i;
      var forcedIter = partsOver500.ToArray();
      Assert.IsTrue(forcedIter.Length == 2);
    }

    [Test]
    public void TestRangedWhereGreaterThanOrEqualTo()
    {
      var blbCollection = new TestBusinessListBaseCollection(50, 1, true);
      blbCollection.Add(new TestIndexableItem() { IndexedInt = 500, IndexedString = "500", NonIndexedString = "500" });
      blbCollection.Add(new TestIndexableItem() { IndexedInt = 600, IndexedString = "600", NonIndexedString = "600" });
      var partsOver500 = from i in blbCollection where i.IndexedInt >= 500  select i;
      var forcedIter = partsOver500.ToArray();
      Assert.IsTrue(forcedIter.Length == 2);
    }

    [Test]
    public void TestRangedWhereLessThan()
    {
      var blbCollection = new TestBusinessListBaseCollection(50, 1, true);
      blbCollection.Add(new TestIndexableItem() { IndexedInt = -1, IndexedString = "-1", NonIndexedString = "-1" });
      blbCollection.Add(new TestIndexableItem() { IndexedInt = -2, IndexedString = "-2", NonIndexedString = "-2" });
      var partsUnderZero = from i in blbCollection where i.IndexedInt < 0 select i;
      var forcedIter = partsUnderZero.ToArray();
      Assert.IsTrue(forcedIter.Length == 2);
    }

    [Test]
    public void TestRangedWhereLessThanOrEqualTo()
    {
      var blbCollection = new TestBusinessListBaseCollection(50, 1, true);
      blbCollection.Add(new TestIndexableItem() { IndexedInt = -1, IndexedString = "-1", NonIndexedString = "-1" });
      blbCollection.Add(new TestIndexableItem() { IndexedInt = -2, IndexedString = "-2", NonIndexedString = "-2" });
      var partsUnderZero = from i in blbCollection where i.IndexedInt <= -1 select i;
      var forcedIter = partsUnderZero.ToArray();
      Assert.IsTrue(forcedIter.Length == 2);
    }

    [Test]
    public void IndexOnBusinessListBaseWorks()
    {
      var sampleSize = 100000;
      //var sampleSize = 375;
      Console.WriteLine("Creating " + sampleSize + " element collection...");
      var blbCollection = new TestBusinessListBaseCollection(sampleSize);
      Console.WriteLine("Collection established.");

      //first query establishes the index
      var controlSet = blbCollection.ToList();

      var primeQuery = from i in blbCollection where i.IndexedString == "42" select i;
      var forcedPrimeITeration = primeQuery.ToArray();

      Stopwatch watch = new Stopwatch();

      watch.Start();
      var indexedQuery = from i in blbCollection where i.IndexedString == "42" select i;
      var forcedIterationIndexed = indexedQuery.ToArray();
      watch.Stop();

      var indexedRead = watch.ElapsedMilliseconds;

      watch.Reset();

      watch.Start();
      var nonIndexedQuery = from i in blbCollection where i.NonIndexedString == "42" select i;
      var forcedIterationNonIndexed = nonIndexedQuery.ToArray();
      watch.Stop();

      var nonIndexedRead = watch.ElapsedMilliseconds;

      watch.Reset();

      watch.Start();
      var controlQuery = from i in controlSet where i.IndexedString == "42" select i;
      var forcedControlIteration = controlQuery.ToArray();
      watch.Stop();

      var controlRead = watch.ElapsedMilliseconds;


      Console.WriteLine("Sample size = " + sampleSize);
      Console.WriteLine("Indexed Read = " + indexedRead + "ms");
      Console.WriteLine("Non-Indexed Read = " + nonIndexedRead + "ms");
      Console.WriteLine("Standard Linq-to-objects Read = " + controlRead + "ms");
      // Not using indexset so response time should be equal 
      //Assert.IsTrue(indexedRead == nonIndexedRead);
      Assert.IsTrue(forcedIterationIndexed.Count() == forcedIterationNonIndexed.Count());
    }

    [Test]
    public void QueryOnIndexedFieldThatCantUseIndexWorks()
    {
      var sampleSize = 1000;
      Console.WriteLine("Creating " + sampleSize + " element collection...");
      var blbCollection = new TestBusinessListBaseCollection(sampleSize);
      Console.WriteLine("Collection established.");
      var someQuery = from i in blbCollection where i.IndexedInt <= 1000 select i;
      //it should bring back everything 
      Assert.IsTrue(someQuery.Count() == 1000);
    }

    [Test]
    public void QueryWithComplexWhere()
    {
      var sampleSize = 1000;
      Console.WriteLine("Creating " + sampleSize + " element collection...");
      var blbCollection = new TestBusinessListBaseCollection(sampleSize,1,false);
      Console.WriteLine("Collection established.");
      var someQuery = from i in blbCollection where i.IndexedInt >= 0 && i.IndexedInt <= 1000 select i;
      //it should bring back everything 
      Assert.IsTrue(someQuery.Count() == 1000);

      var firstItem = blbCollection.First();

      var anotherQuery = from i in blbCollection where i.IndexedInt.Equals(firstItem.IndexedInt) select i;
      //it should bring back 1 item 
      Assert.IsTrue(anotherQuery.Count() == 1);
    }
  }

  [Serializable]
  public class TestReadOnlyCollection : ReadOnlyListBase<TestReadOnlyCollection, TestIndexableItem>
  {
    public TestReadOnlyCollection(int sampleSize)
      : this(sampleSize, 100, true) { }
    public TestReadOnlyCollection(int sampleSize, int sparsenessFactor, bool randomize)
    {
      //allow adds
      IsReadOnly = false;
      Random rnd = new Random();
      for(int i = 0; i < sampleSize; i++)
      {
        int nextRnd;
        if (randomize)
          nextRnd = rnd.Next(sampleSize / sparsenessFactor);
        else
          nextRnd = i / sparsenessFactor;
        Add(
          new TestIndexableItem 
          { 
            IndexedString = nextRnd.ToString(), 
            IndexedInt = nextRnd, 
            NonIndexedString = nextRnd.ToString() 
          }
          );
      }
      IsReadOnly = true;
    }
  }

  [Serializable]
  public class TestBusinessListBaseCollection : BusinessListBase<TestBusinessListBaseCollection, TestIndexableItem>
  {
    public TestBusinessListBaseCollection(int sampleSize)
      : this(sampleSize, 100, true) { }
    public TestBusinessListBaseCollection(int sampleSize, int sparsenessFactor, bool randomize)
    {
      Random rnd = new Random();
      for(int i = 0; i < sampleSize; i++)
      {
        int nextRnd;
        if (randomize)
          nextRnd = rnd.Next(sampleSize / sparsenessFactor);
        else
          nextRnd = i / sparsenessFactor;
        Add(
          new TestIndexableItem 
          { 
            IndexedString = nextRnd.ToString(), 
            IndexedInt = nextRnd, 
            NonIndexedString = nextRnd.ToString() 
          }
          );
      }
    }
  }

  [Serializable]
  public class TestCollection : BusinessListBase<TestCollection, TestItem>
  {
  }

  [Serializable]
  public class TestItem : BusinessBase<TestItem>
  {
    protected override object GetIdValue()
    {
      return 0;
    }

    public TestItem()
    {
      MarkAsChild();
    }
  }

  [Serializable]
  public class TestIndexableItem : BusinessBase<TestIndexableItem>, Csla.Core.IReadOnlyObject 
  {
    [Indexable]
    public string IndexedString{get; set;}
    [Indexable]
    public int IndexedInt{get; set;}
    public string NonIndexedString{get; set;}


    #region IReadOnlyObject Members

    bool Csla.Core.IReadOnlyObject.CanReadProperty(string propertyName)
    {
        throw new NotImplementedException();
    }

    #endregion
  }
}
