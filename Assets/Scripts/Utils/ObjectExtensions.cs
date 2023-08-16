using System;
using System.Reflection;
using System.Linq;

public static class ObjectExtensions
{
  public static T GetPropertyValue<T>(this object obj, string propertyName)
  {
    if (obj == null)
    {
      throw new ArgumentNullException(nameof(obj));
    }

    if (string.IsNullOrEmpty(propertyName))
    {
      throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
    }

    Type objectType = obj.GetType();
    PropertyInfo propertyInfo = objectType.GetProperty(propertyName);

    if (propertyInfo == null)
    {
      throw new ArgumentException($"Property '{propertyName}' not found on type '{objectType.FullName}'.");
    }

    if (!propertyInfo.CanRead)
    {
      throw new InvalidOperationException($"Property '{propertyName}' on type '{objectType.FullName}' does not have a public getter.");
    }

    object value = propertyInfo.GetValue(obj);
    if (value == null)
    {
      return default(T); // Or throw an exception if you prefer not to return a default value for null.
    }

    return (T)value;
  }
  public static void SetPropertyValue<T>(this object obj, string propertyName, T value)
  {
    if (obj == null)
    {
      throw new ArgumentNullException(nameof(obj));
    }

    if (string.IsNullOrEmpty(propertyName))
    {
      throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
    }

    Type objectType = obj.GetType();
    PropertyInfo propertyInfo = objectType.GetProperty(propertyName);

    if (propertyInfo == null)
    {
      throw new ArgumentException($"Property '{propertyName}' not found on type '{objectType.FullName}'.");
    }

    if (!propertyInfo.CanWrite)
    {
      throw new InvalidOperationException($"Property '{propertyName}' on type '{objectType.FullName}' does not have a public setter.");
    }

    propertyInfo.SetValue(obj, value);
  }
  //   public static object GetDeepProperty(this object instance, string path)
  //   {
  //     var pp = path.Split('.');
  //     var t = instance.GetType();
  //     foreach (var prop in pp)
  //     {
  //       var propInfo = t.GetProperty(prop);
  //       if (propInfo != null)
  //       {
  //         instance = propInfo.GetValue(instance, null);
  //         t = propInfo.PropertyType;
  //       }
  //       else throw new ArgumentException("Properties path is not correct");
  //     }
  //     return instance;
  //   }
  public static PropertyInfo GetProp(Type baseType, string propertyName)
  {
    string[] parts = propertyName.Split('.');

    return (parts.Length > 1)
        ? GetProp(
            baseType.GetProperty(
                parts[0]).PropertyType,
                parts.Skip(1).Aggregate((a, i) => a + "." + i)
            )
        : baseType.GetProperty(propertyName);
  }
  public static PropertyInfo GetProp(this object obj, string propertyName)
  {
    return GetProp(obj.GetType(), propertyName);
  }
}