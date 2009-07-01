using System;
using Csla;

namespace cslalighttest.ReadOnly
{
  [Serializable]
  public class MockReadOnly : BusinessBase<MockReadOnly>
  {
    public MockReadOnly() { }

    private static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(
      typeof(MockReadOnly),
      new PropertyInfo<int>("Id"));

    public int Id
    {
      get { return GetProperty<int>(IdProperty); }
    }
  }
}
