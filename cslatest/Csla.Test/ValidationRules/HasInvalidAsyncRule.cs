﻿using System;
using System.Threading;
using Csla.Validation;

namespace Csla.Test.ValidationRules
{
#if TESTING
  [System.Diagnostics.DebuggerStepThrough]
#endif
  public partial class HasInvalidAsyncRule : BusinessBase<HasInvalidAsyncRule>
  {
    private static PropertyInfo<string> NameProperty = RegisterProperty(
      typeof(HasAsyncRule),
      new PropertyInfo<string>("Name"));

    public string Name
    {
      get { return GetProperty(NameProperty); }
      set { SetProperty(NameProperty, value); }
    }

    protected override void AddBusinessRules()
    {
      ValidationRules.AddRule(InvalidAsyncValidationRule, NameProperty);
      base.AddBusinessRules();
    }

    private ManualResetEvent _reset = new ManualResetEvent(false);
    public EventWaitHandle Reset
    {
      get { return _reset; }
    }

    private void InvalidAsyncValidationRule(AsyncValidationRuleContext context)
    {
      throw new NotImplementedException();
    }
  }
}
