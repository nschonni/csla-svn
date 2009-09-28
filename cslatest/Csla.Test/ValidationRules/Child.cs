using System;

namespace Csla.Test.ValidationRules
{
  [Serializable]
  public class Child : BusinessBase<Child>
  {
    private static PropertyInfo<int> IdProperty = RegisterProperty(new PropertyInfo<int>("Id", "Id"));
    public int Id
    {
      get { return GetProperty(IdProperty); }
      set { SetProperty(IdProperty, value); }
    }
  }
}
