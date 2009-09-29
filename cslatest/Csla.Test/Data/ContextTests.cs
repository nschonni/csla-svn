#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;

#else
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Csla.Data;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

#endif

namespace Csla.Test.Data
{
  [TestClass]
  public class ContextTests
  {
    private const string TestDBConnection = "Csla.Test.Properties.Settings.DataPortalTestDatabaseConnectionString";
    private const string InvalidTestDBConnection = "Csla.Test.Properties.Settings.DataPortalTestDatabaseConnectionStringXXXXXXX";

    private const string ConnectionWithMissingDB = "DataPortalTestDatabaseConnectionString_with_invalid_DB_value";
    private const string EntityConnectionWithMissingDB = "DataPortalTestDatabaseEntities_with_invalid_DB_value";

    #region Invalid connection strings
    [TestMethod]
    [ExpectedException(typeof(ConfigurationErrorsException))]
    public void InvalidConnectionSetting_Throws_ConfigurationErrorsException_for_SqlConnection()
    {
      using (var objectContextManager = ConnectionManager<SqlConnection>.GetManager(InvalidTestDBConnection, true))
      {
      }
    }


    [Test]
    [ExpectedException(typeof(SqlException))]
    public void ConnectionSetting_with_Invalid_DB_Throws_ConfigurationErrorsException_for_SqlConnection()
    {
      //throws SqlException
      using (var objectContextManager = ConnectionManager<SqlConnection>.GetManager(ConnectionWithMissingDB, true))
      {
      }
    }

    #endregion

    #region Data

    [Test]
    public void ExecuteReader_on_Table2_returns_reader_with_3_fields()
    {
      using (var objectContextManager = ConnectionManager<SqlConnection>.GetManager(TestDBConnection, true))
      {        
        Assert.IsNotNull(objectContextManager);
        using (var command = new SqlCommand("Select * From Table2", objectContextManager.Connection))
        {
          command.CommandType = CommandType.Text;
          using (var reader = new Csla.Data.SafeDataReader(command.ExecuteReader()))
            Assert.IsTrue(reader.FieldCount == 3, "Did not get reader");
        }
      }
    }

    #endregion

    #region Transaction Manager

    [Test]
    public void Using_TransactionManager_Insert_of_2records_rolls_back_if_second_record_fails_insert()
    {
      ApplicationContext.LocalContext.Clear();
      var list = TransactionContextUserList.GetList();
      int counter = list.Count;

      list.Add(new TransactionContextUser
      {
        FirstName = "First",
        LastName = "Last",
        SmallColumn = "aaaa"
      });

      list.Add(new TransactionContextUser
      {
        FirstName = "First1",
        LastName = "Last1",
        SmallColumn = "bbbbbbbbbbbbbb"
      });

      bool gotError = false;
      try
      {
        list.Save();
      }
      catch (DataPortalException ex)
      {
        // will be thrown from SQL server
        gotError = true;
      }

      Assert.IsTrue(gotError, "SQL should have thrown an error");
      int tCount = 0;
      foreach (var r in ApplicationContext.LocalContext.Keys)
        if (r.ToString().StartsWith("__transaction:"))
          tCount++;
      Assert.AreEqual(0, tCount, "Transaction context should have been null");

      list = TransactionContextUserList.GetList();
      Assert.AreEqual(counter, list.Count, "Data should not have been saved.");
    }

    [Test]
    public void Using_TransactionManager_Insert_2records_increases_count_by2_then_removing_them_decreases_count_by2()
    {
      ApplicationContext.LocalContext.Clear();
      var list = TransactionContextUserList.GetList();
      int beforeInsertCount = list.Count;

      list.AddRange(new[]
                      {
                        new TransactionContextUser
                          {
                            FirstName = "First",
                            LastName = "Last",
                            SmallColumn = "aaaa"
                          },
                        new TransactionContextUser
                          {
                            FirstName = "First1",
                            LastName = "Last",
                            SmallColumn = "bbb"
                          }
                      });

      list.Save();

      int tCount = 0;
      foreach (var r in ApplicationContext.LocalContext.Keys)
        if (r.ToString().StartsWith("__transaction:"))
          tCount++;
      Assert.AreEqual(0, tCount, "Transaction context should have been null");

      list = TransactionContextUserList.GetList();
      Assert.AreEqual(beforeInsertCount + 2, list.Count, "Data should have been saved.");

      list.Remove(list.Last(o => o.LastName == "Last"));
      list.Remove(list.Last(o => o.LastName == "Last"));

      list.Save();

      tCount = 0;
      foreach (var r in ApplicationContext.LocalContext.Keys)
        if (r.ToString().StartsWith("__transaction:"))
          tCount++;
      Assert.AreEqual(0, tCount, "Transaction context should have been null");

      list = TransactionContextUserList.GetList();
      Assert.AreEqual(beforeInsertCount, list.Count, "Data should not have been saved.");
    }

    [Test]
    public void TestTransactionsManaagerConnectionProperty()
    {
      using (var manager = TransactionManager<SqlConnection, SqlTransaction>.GetManager(TestDBConnection, true))
      {
        Assert.AreSame(manager.Connection, manager.Transaction.Connection, "COnnection is not correct");
        Assert.IsNotNull(manager.Connection, "Connection should not be null.");
        Assert.IsNotNull(manager.Transaction, "Transaction should not be null.");
      }
    }

    #endregion
  }
}
