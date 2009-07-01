using System;

namespace Csla.Test.ChildChanged
{
  [Serializable]
  public class SingleRoot : BusinessBase<SingleRoot>
  {
    public SingleRoot()
    { }

    public SingleRoot(bool child)
    {
      if (child)
        MarkAsChild();
    }

    private static PropertyInfo<string> NameProperty = RegisterProperty(new PropertyInfo<string>("Name", "Name"));
    public string Name
    {
      get { return GetProperty(NameProperty); }
      set { SetProperty(NameProperty, value); }
    }
  }
}
