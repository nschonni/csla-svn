using System.Collections.Generic;

namespace Csla.Core.FieldManager
{
  internal class PropertyInfoList : List<IPropertyInfo>
  {
    public bool IsLocked { get; set; }

    public PropertyInfoList()
    { }

    public PropertyInfoList(IList<IPropertyInfo> list)
      : base(list)
    { }
  }
}
