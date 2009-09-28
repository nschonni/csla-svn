using System;

namespace Csla.Test.ValidationRules
{
  [Serializable]
  public class ChildList : BusinessListBase<ChildList, Child>
  {
    public static ChildList NewList()
    {
      return Csla.DataPortal.CreateChild<ChildList>();
    }
  }
}
