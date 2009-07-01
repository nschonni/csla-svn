using System;

namespace Csla.Test.Serialization
{
  [Serializable]
  public class DCRoot : BusinessBase<DCRoot>
  {
    
    int _data;
    public int Data
    {
      [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
      get
      {
        CanReadProperty(true);
        return _data;
      }
      [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
      set
      {
        CanWriteProperty(true);
        if (!_data.Equals(value))
        {
          _data = value;
          PropertyHasChanged();
        }
      }
    }

    private NonCslaChild _child = new NonCslaChild();
    public NonCslaChild Child
    {
      get { return _child; }
    }
	

    protected override object GetIdValue()
    {
      return _data;
    }
  }

  [Serializable]
  public class NonCslaChild
  {
    
    private int _value;

    public int TheValue
    {
      get { return _value; }
      set { _value = value; }
    }
	
  }
}
