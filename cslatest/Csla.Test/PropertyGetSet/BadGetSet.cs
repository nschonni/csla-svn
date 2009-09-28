using System;

namespace Csla.Test.PropertyGetSet
{
#if TESTING
  [System.Diagnostics.DebuggerNonUserCode]
#endif
  [Serializable]
  public class BadGetSet : BusinessBase<BadGetSet>
  {
    // the registering class is intentionally incorrect for this test
    private static PropertyInfo<int> IdProperty = RegisterProperty<int>(typeof(EditableGetSet), new PropertyInfo<int>("Id", "Id"));
    public int Id
    {
      get { return GetProperty<int>(IdProperty); }
      set { SetProperty<int>(IdProperty, value); }
    }
  }
}
