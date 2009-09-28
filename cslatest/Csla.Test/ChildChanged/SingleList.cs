using System;

namespace Csla.Test.ChildChanged
{
  [Serializable]
  public class SingleList : BusinessListBase<SingleList, SingleRoot>
  {
    public SingleList()
    {
    }

    public SingleList(bool child)
      : this()
    {
      if (child)
        MarkAsChild();
    }
  }
}
