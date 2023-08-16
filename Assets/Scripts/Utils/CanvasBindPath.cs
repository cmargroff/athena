using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class CanvasBindPath : MonoBehaviour
{
  public string Path;
  public Component Component; //target component of the bind
  public string Property;
  private object _originalValue;
  public void Bind(object obj)
  {
    var prop = obj.GetProp(Path);
    if (prop == null)
    {
      // bind was not found on input object
      return;
    }
    var value = prop.GetValue(obj);
    var componentType = Component.GetType();
    var componentProp = componentType.GetProperty(Property);
    if (componentProp == null)
    {
      // skipping destination property not found
      return;
    }

    if (_originalValue == null)
    {
      _originalValue = componentProp.GetValue(Component);
    }

    if (componentType == typeof(TextMeshProUGUI))
    {
      var tagReg = new Regex(@"\{\{\s*?([^\s{}]+)\s*?\}\}");
      var val = _originalValue.ToString();
      var match = tagReg.Match(val);
      if (match.Success && match.Groups[1].Value == prop.Name)
      {
        // replace the tag
        var text = tagReg.Replace(_originalValue.ToString(), value.ToString());
        componentProp.SetValue(Component, text);
      }
    }
    else if(componentType == typeof(Image) && _originalValue.GetType() == typeof(Material)) {
      if(value is Color) {
        (_originalValue as Material).color = (Color)value;
      }
      else if(value is Texture) {
        (_originalValue as Material).mainTexture = (Texture)value;
      }
    }
    else
    {
      componentProp.SetValue(Component, value);
    }
  }
}