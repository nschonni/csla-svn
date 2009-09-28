using System;

namespace Csla.Test.LazyLoad
{
  [Serializable]
  public class AChildList : Csla.BusinessListBase<AChildList, AChild>
  {
    public AChildList()
    {
      MarkAsChild();
      this.Add(new AChild());
    }

    public int EditLevel
    {
      get { return base.EditLevel; }
    }
  }
}
