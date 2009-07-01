using System;
using Csla.Core;

namespace Csla.Security
{
  /// <summary>
  /// Implementation of a .NET identity object representing
  /// an unauthenticated user. Used by the
  /// UnauthenticatedPrincipal class.
  /// </summary>
  [Serializable()]
  public sealed class UnauthenticatedIdentity : CslaIdentity
  {
    /// <summary>
    /// Creates an instance of the object.
    /// </summary>
    public UnauthenticatedIdentity()
    {
      IsAuthenticated = false;
      Name = string.Empty;
      AuthenticationType = string.Empty;
      Roles = new MobileList<string>();
    }
  }
}
