using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UIViewModels.Attributes
{
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
    //public class LabelBindAttribute : VisualElementBindAttribute
    //{

    //    public LabelBindAttribute(string controlName, string fieldName = "Text") : base(, controlNamefieldName)
    //    {
    //    }

    //}
}
