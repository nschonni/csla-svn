using System;

namespace Csla.Test.ChildChanged
{
  [Serializable]
  public class ContainsList : BusinessBase<ContainsList>
  {
    public ContainsList()
    { }

    public ContainsList(bool child)
    {
      if (child)
        MarkAsChild();
    }

    private static PropertyInfo<SingleList> ListProperty = RegisterProperty(new PropertyInfo<SingleList>("List", "List"));
    public SingleList List
    {
      get 
      {
        if (!FieldManager.FieldExists(ListProperty))
          LoadProperty(ListProperty, new SingleList(true));
        return GetProperty(ListProperty); 
      }
    }
  }
}
