using System;

namespace Csla.Security
{
  /// <summary>
  /// Implementation of a .NET principal object that represents
  /// an unauthenticated user. Contains an UnauthenticatedIdentity
  /// object.
  /// </summary>
  [Serializable]
  public sealed class UnauthenticatedPrincipal : BusinessPrincipalBase
  {
    /// <summary>
    /// Creates an instance of the object.
    /// </summary>
    public UnauthenticatedPrincipal() : base(new UnauthenticatedIdentity()) { }

  }
}
