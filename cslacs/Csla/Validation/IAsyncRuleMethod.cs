namespace Csla.Validation
{
  internal interface IAsyncRuleMethod : IRuleMethod
  {
    AsyncRuleArgs AsyncRuleArgs { get; }

    RuleSeverity Severity { get; }

    void Invoke(object target, AsyncRuleCompleteHandler complete);
  }
}
