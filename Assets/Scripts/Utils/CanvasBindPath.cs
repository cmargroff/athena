using System;
using System.Text.RegularExpressions;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class CanvasBindPath : MonoBehaviour
{
  public static Regex tagReg = new Regex(@"\{\{\s*?([^\s{}]+)\s*?\}\}");
  public string Path;
  public Component Component; //target component of the bind
  public string Property;
  private object _originalValue;
  private bool _isBound = false;
  public void Bind(object obj)
  {
    var srcProp = obj.GetProp(Path);
    if (srcProp == null)
    {
      // bind was not found on input object
      return;
    }
    var value = srcProp.GetValue(obj);
    var destProp = Component.GetType().GetProperty(Property);
    if (destProp == null)
    {
      // skipping destination property not found
      return;
    }

    if (_originalValue == null)
    {
      _originalValue = destProp.GetValue(Component);
    }

    if (destProp.PropertyType == typeof(string))
    {
      AssignStringValue(_originalValue.ToString(), value, srcProp, destProp);
    }
    else if (destProp.PropertyType == typeof(Material))
    {
      AssignMaterialValue(_originalValue as Material, value);
    }
    else if (destProp.PropertyType.IsSubclassOf(typeof(UnityEvent)))
    {
      AssignUnityEventValue(value as Action, srcProp, destProp);
    }
    _isBound = true;
  }
  public void AssignStringValue(string orig, object value, PropertyInfo srcProp, PropertyInfo destProp)
  {
    var match = tagReg.Match(orig);
    if (match.Success && match.Groups[1].Value == srcProp.Name)
    {
      // replace the tag
      var text = tagReg.Replace(_originalValue.ToString(), value.ToString());
      destProp.SetValue(Component, text);
    }
  }
  public void AssignMaterialValue(Material mat, object value)
  {
    if (value is Color)
    {
      mat.color = (Color)value;
    }
    else if (value is Texture)
    {
      mat.mainTexture = (Texture)value;
    }
  }
  public void AssignUnityEventValue(Action value, PropertyInfo srcProp, PropertyInfo destProp)
  {
    if (_isBound) return;
    var unityEvent = destProp.GetValue(Component) as UnityEvent;
    unityEvent.AddListener(() => value());
  }
}