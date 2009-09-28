using System;
using Csla;

namespace cslalighttest.ReadOnly
{
  [Serializable]
  public class MockReadOnlyList : ReadOnlyListBase<MockReadOnlyList, MockReadOnly>
  {
    public MockReadOnlyList() { }

    public MockReadOnlyList(MockReadOnly mock)
    {
      IsReadOnly = false;
      Add(mock);
      IsReadOnly = true;
    }
  }
}
