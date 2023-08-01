using System;


[AttributeUsage(AttributeTargets.Field)]
public class VisualElementBindAttribute : Attribute
{
    public string ControlName { get; }
    public string FieldName { get; }

    public VisualElementBindAttribute(string controlName = null, string fieldName = "text")
    {
        ControlName = controlName;
        FieldName = fieldName;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class VisualElementEventAttribute : Attribute
{
    public string ControlName { get; }
    public VisualElementEventAttribute(string controlName = null)
    {
        ControlName = controlName;

    }
}


