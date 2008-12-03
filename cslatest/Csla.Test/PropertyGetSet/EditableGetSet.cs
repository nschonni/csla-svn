﻿using System;
using System.Collections.Generic;
using System.Text;
using Csla;

namespace Csla.Test.PropertyGetSet
{
  [Serializable]
  public class EditableGetSet : EditableGetSetBase<EditableGetSet>
  {
    private static Csla.PropertyInfo<string> F01Property = RegisterProperty<string>(typeof(EditableGetSet), new Csla.PropertyInfo<string>("F01"));
    private string _f01 = F01Property.DefaultValue;
    public string F01
    {
      get { return GetProperty<string>(F01Property, _f01); }
      set { SetProperty<string>(F01Property, ref _f01, value); }
    }

    private static Csla.PropertyInfo<int> F02Property = RegisterProperty<int>(typeof(EditableGetSet), new Csla.PropertyInfo<int>("F02"));
    private int _f02 = F02Property.DefaultValue;
    public int F02
    {
      get { return GetProperty<int>(F02Property, _f02); }
      set { SetProperty<int>(F02Property, ref _f02, value); }
    }

    private static Csla.PropertyInfo<string> F03Property = RegisterProperty<string>(typeof(EditableGetSet), new Csla.PropertyInfo<string>("F03", "field 3", "n/a"));
    private string _f03 = F03Property.DefaultValue;
    public string F03
    {
      get { return GetProperty<string>(F03Property, _f03); }
      set { SetProperty<string>(F03Property, ref _f03, value); }
    }

    private static Csla.PropertyInfo<Csla.SmartDate> F04Property = RegisterProperty<Csla.SmartDate>(typeof(EditableGetSet), new Csla.PropertyInfo<Csla.SmartDate>("F04"));
    private Csla.SmartDate _F04 = F04Property.DefaultValue;
    public string F04
    {
      get { return GetProperty<Csla.SmartDate, string>(F04Property, _F04); }
      set { SetProperty<Csla.SmartDate, string>(F04Property, ref _F04, value); }
    }

    private static Csla.PropertyInfo<bool> F05Property = RegisterProperty<bool>(typeof(EditableGetSet), new Csla.PropertyInfo<bool>("F05", "field 5"));
    private bool _f05 = F05Property.DefaultValue;
    public bool F05
    {
      get { return GetProperty<bool>(F05Property, _f05); }
      set { SetProperty<bool>(F05Property, ref _f05, value); }
    }

    private static Csla.PropertyInfo<object> F06Property = RegisterProperty<object>(typeof(EditableGetSet), new Csla.PropertyInfo<object>("F06", "field 6"));
    private object _F06 = string.Empty;
    public string F06
    {
      get { return GetProperty<object, string>(F06Property, _F06); }
      set { SetProperty<object, string>(F06Property, ref _F06, value); }
    }

    private static Csla.PropertyInfo<string> M01Property = RegisterProperty<string>(typeof(EditableGetSet), new Csla.PropertyInfo<string>("M01"));
    public string M01
    {
      get { return GetProperty<string>(M01Property); }
      set { SetProperty<string>(M01Property, value); }
    }

    public bool M01Dirty
    {
      get { return FieldManager.IsFieldDirty(M01Property); }
    }

    private static Csla.PropertyInfo<int> M02Property = RegisterProperty<int>(typeof(EditableGetSet), new Csla.PropertyInfo<int>("M02"));
    public int M02
    {
      get { return GetProperty<int>(M02Property); }
      set { SetProperty<int>(M02Property, value); }
    }

    private static Csla.PropertyInfo<string> M03Property = RegisterProperty<string>(typeof(EditableGetSet), new Csla.PropertyInfo<string>("M03", "field 3", "n/a"));
    public string M03
    {
      get { return GetProperty<string>(M03Property); }
      set { SetProperty<string>(M03Property, value); }
    }

    private static Csla.PropertyInfo<Csla.SmartDate> M04Property = RegisterProperty<Csla.SmartDate>(typeof(EditableGetSet), new Csla.PropertyInfo<Csla.SmartDate>("M04"));
    public string M04
    {
      get { return GetProperty<Csla.SmartDate, string>(M04Property); }
      set { SetProperty<Csla.SmartDate, string>(M04Property, value); }
    }

    private static Csla.PropertyInfo<bool> M05Property = RegisterProperty<bool>(typeof(EditableGetSet), new Csla.PropertyInfo<bool>("M05", "field 5"));
    public bool M05
    {
      get { return GetProperty<bool>(M05Property); }
      set { SetProperty<bool>(M05Property, value); }
    }

    private static Csla.PropertyInfo<Guid> M06Property = RegisterProperty<Guid>(typeof(EditableGetSet), new Csla.PropertyInfo<Guid>("M06", "field 6"));
    public Guid M06
    {
      get { return GetProperty<Guid>(M06Property); }
      set { SetProperty<Guid>(M06Property, value); }
    }

    private static Csla.PropertyInfo<object> M07Property = RegisterProperty<object>(typeof(EditableGetSet), new Csla.PropertyInfo<object>("M07", "field 7"));
    public string M07
    {
      get { return GetProperty<object, string>(M07Property); }
      set { SetProperty<object, string>(M07Property, value); }
    }

    private static Csla.PropertyInfo<EditableGetSet> C01Property = 
      RegisterProperty(new Csla.PropertyInfo<EditableGetSet>("C01"));
    public EditableGetSet C01
    {
      get 
      { 
        if (!FieldManager.FieldExists(C01Property))
          SetProperty<EditableGetSet>(C01Property, new EditableGetSet(true));
        return GetProperty<EditableGetSet>(C01Property); 
      }
    }

    private static Csla.PropertyInfo<ChildList> L01Property = RegisterProperty<ChildList>(typeof(EditableGetSet), new Csla.PropertyInfo<ChildList>("L01"));
    public ChildList L01
    {
      get
      {
        if (!FieldManager.FieldExists(L01Property))
          LoadProperty<ChildList>(L01Property, new ChildList(true));
        return GetProperty<ChildList>(L01Property);
      }
    }

    public int EditLevel
    {
      get { return base.EditLevel; }
    }

    public void MarkClean()
    {
      base.MarkClean();
    }

    public EditableGetSet()
    {
      MarkNew();
      MarkClean();
    }

    public EditableGetSet(bool isChild)
    {
      if (isChild)
      {
        MarkAsChild();
        MarkNew();
      }
    }

    #region Factory Methods

    public static EditableGetSet GetObject()
    {
      return Csla.DataPortal.Fetch<EditableGetSet>();
    }

    #endregion

    #region Data Access

    private void DataPortal_Fetch()
    {
      LoadProperty(M06Property, null);
    }

    protected override void DataPortal_Insert()
    {
      //FieldManager.UpdateChildren();
      if (FieldManager.FieldExists(C01Property))
        C01.Insert();
      if (FieldManager.FieldExists(L01Property))
        L01.Update();
    }

    protected override void DataPortal_Update()
    {
      //FieldManager.UpdateChildren();
      if (FieldManager.FieldExists(C01Property))
        C01.Update();
      if (FieldManager.FieldExists(L01Property))
        L01.Update();
    }

    internal void Insert()
    {
      MarkOld();
    }

    internal void Update()
    {
      MarkOld();
    }

    #endregion
  }
}
