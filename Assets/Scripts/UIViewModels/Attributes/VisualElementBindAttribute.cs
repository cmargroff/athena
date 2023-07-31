using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;

namespace Assets.Scripts.UIViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class VisualElementBindAttribute:Attribute
    {
        public string ControlName { get; }
        public string FieldName { get; }

        public VisualElementBindAttribute(string controlName=null, string fieldName = "text")
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
    //public class LabelBindAttribute : VisualElementBindAttribute
    //{

    //    public LabelBindAttribute(string controlName, string fieldName = "Text") : base(, controlNamefieldName)
    //    {
    //    }

    //}
}
