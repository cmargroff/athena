using System;
using System.Text.RegularExpressions;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class CanvasBindPath : MonoBehaviour
{
    public static Regex tagReg = new Regex(@"\{\{\s*?([^\s{}]+)\s*?\}\}");
    public Component Component; //target component of the bind
    public string Property;
    public string Path;
    private object _originalValue;
    private bool _isBound = false;
    public void Bind(object obj)
    {
        var destProp = Component.GetType().GetProperty(Property);
        if (destProp == null)
        {
            // skipping destination property not found
            return;
        }
        var srcProp = obj.GetProp(Path);
        if (srcProp == null && destProp.PropertyType != typeof(string))
        {
            // bind was not found on input object
            return;
        }

        if (_originalValue == null)
        {
            _originalValue = destProp.GetValue(Component);
        }

        if (destProp.PropertyType == typeof(string))
        {
            AssignStringValue(_originalValue.ToString(), obj, destProp);
        }
        else if (destProp.PropertyType == typeof(Material))
        {
            AssignMaterialValue(_originalValue as Material, srcProp.GetValue(obj));
        }
        else if (destProp.PropertyType.IsSubclassOf(typeof(UnityEvent)))
        {
            AssignUnityEventValue(srcProp.GetValue(obj) as Action, destProp);
        }
        else
        {
            // default to assigning value as is
            destProp.SetValue(Component, srcProp.GetValue(obj));
        }
        _isBound = true;
    }
    private void AssignStringValue(string orig, object obj, PropertyInfo destProp)
    {
        // needs to be updated to handle updating only a portion of the string
        var matches = tagReg.Matches(orig);
        var text = orig;
        var updated = false;
        foreach (Match match in matches)
        {
            var srcProp = obj.GetProp(match.Groups[1].Value);
            if (srcProp == null) continue;
            var value = srcProp.GetValue(obj);
            if (value == null) continue;
            updated = true;
            text = text.Replace(match.Groups[0].Value, value.ToString());
        }
        if (updated)
            destProp.SetValue(Component, text);
    }
    private void AssignMaterialValue(Material mat, object value)
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
    private void AssignUnityEventValue(Action value, PropertyInfo destProp)
    {
        if (_isBound) return;
        var unityEvent = destProp.GetValue(Component) as UnityEvent;
        unityEvent.AddListener(() => value());
    }
}