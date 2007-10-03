﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Security.Principal;

namespace Csla.Security
{
  /// <summary>
  /// Provides a cache for a limited number of
  /// principal objects at the AppDomain level.
  /// </summary>
  public static class PrincipalCache
  {
    private static List<IPrincipal> _cache = new List<IPrincipal>();

    private static int _maxCacheSize;
    private static int MaxCacheSize
    {
      get
      {
        if (_maxCacheSize == 0)
        {
          string tmp = System.Configuration.ConfigurationManager.AppSettings["CslaPrincipalCacheSize"];
          if (string.IsNullOrEmpty(tmp))
            _maxCacheSize = 10;
          else
            _maxCacheSize = Convert.ToInt32(tmp);
        }
        return _maxCacheSize;
      }
    }

    /// <summary>
    /// Gets a principal from the cache based on
    /// the identity name. If no match is found null
    /// is returned.
    /// </summary>
    /// <param name="name">
    /// The identity name associated with the principal.
    /// </param>
    public static IPrincipal GetPrincipal(string name)
    {
      lock (_cache)
      {
        foreach (IPrincipal item in _cache)
          if (item.Identity.Name == name)
            return item;
        return null;
      }
    }

    /// <summary>
    /// Adds a principal to the cache.
    /// </summary>
    /// <param name="principal">
    /// IPrincipal object to be added.
    /// </param>
    public static void AddPrincipal(IPrincipal principal)
    {
      lock (_cache)
      {
        if (!_cache.Contains(principal))
        {
          _cache.Add(principal);
          if (_cache.Count > MaxCacheSize)
            _cache.RemoveAt(0);
        }
      }
    }

    /// <summary>
    /// Clears the cache.
    /// </summary>
    public static void Clear()
    {
      lock (_cache)
        _cache.Clear();
    }
  }
}
