﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Csla.Core;
using System.Collections.Generic;

namespace Csla.Silverlight
{
  public class InvokeMethod
  {
    #region Attached properties

    public static readonly DependencyProperty ResourceProperty =
      DependencyProperty.RegisterAttached("Resource",
      typeof(object),
      typeof(InvokeMethod),
      null);

    public static void SetResource(UIElement element, object value)
    {
      element.SetValue(ResourceProperty, value);
      new InvokeMethod(element);

    }

    private void InvokeMethod_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      Refresh();
    }

    public static object GetResource(UIElement element)
    {
      return element.GetValue(ResourceProperty);
    }

    public static readonly DependencyProperty MethodNameProperty =
      DependencyProperty.RegisterAttached("MethodName",
      typeof(string),
      typeof(InvokeMethod),
      null);

    public static void SetMethodName(UIElement element, string value)
    {
      element.SetValue(MethodNameProperty, value);
      new InvokeMethod(element);
    }

    public static string GetMethodName(UIElement element)
    {
      return (string)element.GetValue(MethodNameProperty);
    }

    public static readonly DependencyProperty TriggerEventProperty =
      DependencyProperty.RegisterAttached("TriggerEvent",
      typeof(string),
      typeof(InvokeMethod),
      null);

    public static void SetTriggerEvent(UIElement element, string value)
    {
      element.SetValue(TriggerEventProperty, value);
      new InvokeMethod(element);
    }

    public static string GetTriggerEvent(UIElement element)
    {
      return (string)element.GetValue(TriggerEventProperty);
    }

    public static readonly DependencyProperty MethodParameterProperty =
      DependencyProperty.RegisterAttached("MethodParameter",
      typeof(object),
      typeof(InvokeMethod),
      null);

    public static void SetMethodParameter(UIElement element, object value)
    {
      element.SetValue(MethodParameterProperty, value);
      new InvokeMethod(element);
    }

    public static object GetMethodParameter(UIElement element)
    {
      return (string)element.GetValue(MethodParameterProperty);
    }

    public static readonly DependencyProperty ManualEnableControlProperty =
      DependencyProperty.RegisterAttached("ManualEnableControl",
      typeof(bool),
      typeof(InvokeMethod),
      null);

    public static void SetManualEnableControl(UIElement element, object value)
    {
      element.SetValue(ManualEnableControlProperty, value);
      new InvokeMethod(element);
    }

    public static object GetManageEnabledState(UIElement element)
    {
      return (bool)element.GetValue(ManualEnableControlProperty);
    }

    private static List<int> processedControls = new List<int>();
    private static object locker = new object();
    private static bool AddControl(int controlId)
    {
      lock (locker)
      {
        if (processedControls.Contains(controlId))
          return false;
        else
        {
          processedControls.Add(controlId);
          return true;
        }
      }
    }
    private ContentControl _contentControl;

    #endregion

    private UIElement _element;
    private System.Reflection.MethodInfo _targetMethod;
    private object _target;

    public InvokeMethod(UIElement element)
    {
      if (element is ContentControl)
        _contentControl = (ContentControl)element;
      _element = element;
      _target = element.GetValue(ResourceProperty);
      if (_target != null)
      {
        var methodName = (string)element.GetValue(MethodNameProperty);
        if (!string.IsNullOrEmpty(methodName))
        {
          var triggerEvent = (string)element.GetValue(TriggerEventProperty);
          if (!string.IsNullOrEmpty(triggerEvent))
          {
            // at this point all required fields have been set,
            // so hook up the event

            _targetMethod = _target.GetType().GetMethod(methodName);

            var eventRef = element.GetType().GetEvent(triggerEvent);
            if (eventRef != null && AddControl(element.GetHashCode()))
            {
              Refresh();

              var invoke = eventRef.EventHandlerType.GetMethod("Invoke");
              var p = invoke.GetParameters();
              if (p.Length == 2)
              {
                if (typeof(RoutedEventArgs).IsAssignableFrom(p[1].ParameterType))
                {
                  eventRef.AddEventHandler(element, new RoutedEventHandler(CallMethod));
                  if (_target is CslaDataProvider)
                  {
                    ((CslaDataProvider)_target).PropertyChanged -= new PropertyChangedEventHandler(InvokeMethod_PropertyChanged);
                    ((CslaDataProvider)_target).PropertyChanged += new PropertyChangedEventHandler(InvokeMethod_PropertyChanged);
                  }
                }
                else if (typeof(EventArgs).IsAssignableFrom(p[1].ParameterType))
                {
                  eventRef.AddEventHandler(element, new EventHandler(CallMethod));
                  if (_target is CslaDataProvider)
                  {
                    ((CslaDataProvider)_target).PropertyChanged -= new PropertyChangedEventHandler(InvokeMethod_PropertyChanged);
                    ((CslaDataProvider)_target).PropertyChanged += new PropertyChangedEventHandler(InvokeMethod_PropertyChanged);
                  }
                }
                else
                  throw new NotSupportedException();
              }
              else
                throw new NotSupportedException();
            }
          }
        }
      }
    }

    private void Refresh()
    {
      if (_target != null && _element != null && _contentControl != null)
      {
        if ((bool)_element.GetValue(ManualEnableControlProperty) == false)
        {
          CslaDataProvider targetProvider = _target as CslaDataProvider;
          if (targetProvider != null)
          {
            string methodName = _element.GetValue(MethodNameProperty).ToString();
            if (methodName == "Save")
              _contentControl.IsEnabled = targetProvider.CanSave;
            if (methodName == "Cancel")
              _contentControl.IsEnabled = targetProvider.CanCancel;
            if (methodName == "Create")
              _contentControl.IsEnabled = targetProvider.CanCreate;
            if (methodName == "Fetch")
              _contentControl.IsEnabled = targetProvider.CanFetch;
            if (methodName == "Delete")
              _contentControl.IsEnabled = targetProvider.CanDelete;
            if (methodName == "RemoveItem")
              _contentControl.IsEnabled = targetProvider.CanRemoveItem;
            if (methodName == "AddNewItem")
              _contentControl.IsEnabled = targetProvider.CanAddNewItem;
          }
          else
          {
            if (_element.GetValue(MethodNameProperty) != null)
            {
              string targetMethodName = (string)_element.GetValue(MethodNameProperty);
              string canPropertyName = "Can" + targetMethodName;
              var propertyInfo = Csla.Reflection.MethodCaller.GetProperty(_target.GetType(), canPropertyName);
              if (propertyInfo != null)
              {
                object returnValue = Csla.Reflection.MethodCaller.GetPropertyValue(_target, propertyInfo);
                if (returnValue != null && returnValue is bool)
                  _contentControl.IsEnabled = (bool)returnValue;
              }
              else
                _contentControl.IsEnabled = false;
            }
          }
        }
      }
    }


    private void CallMethod(object sender, EventArgs e)
    {
      object p = _element.GetValue(MethodParameterProperty);
      if (p == null)
        _targetMethod.Invoke(_target, null);
      else
        _targetMethod.Invoke(_target, new object[] { p });
    }
  }
}