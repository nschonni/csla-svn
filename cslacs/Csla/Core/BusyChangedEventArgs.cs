﻿using System;

namespace Csla.Core
{
  /// <summary>
  /// Delegate for handling the BusyChanged event.
  /// </summary>
  /// <param name="sender">
  /// Object raising the event.
  /// </param>
  /// <param name="e">
  /// Event arguments.
  /// </param>
  public delegate void BusyChangedEventHandler(object sender, BusyChangedEventArgs e);

  /// <summary>
  /// Event arguments for the BusyChanged event.
  /// </summary>
  public class BusyChangedEventArgs : EventArgs
  {
    /// <summary>
    /// New busy value.
    /// </summary>
    public bool Busy { get; protected set; }
    /// <summary>
    /// Property for which the Busy value has changed.
    /// </summary>
    public string PropertyName { get; protected set; }

    /// <summary>
    /// Creates a new instance of the object.
    /// </summary>
    /// <param name="propertyName">
    /// Property for which the Busy value has changed.
    /// </param>
    /// <param name="busy">
    /// New Busy value.
    /// </param>
    public BusyChangedEventArgs(string propertyName, bool busy)
    {
      PropertyName = propertyName;
      Busy = busy;
    }
  }
}
