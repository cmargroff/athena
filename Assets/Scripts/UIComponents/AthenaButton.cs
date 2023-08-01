using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;


[UnityEngine.Scripting.Preserve]
public class AthenaButton : Button
{
    public bool enabled
    {
        get => enabledSelf;
        set => SetEnabled(value);
    }
    public new class UxmlFactory : UxmlFactory<AthenaButton, UxmlTraits> { }
    public new class UxmlTraits : Button.UxmlTraits
    {
        UxmlBoolAttributeDescription enabledAttr = new UxmlBoolAttributeDescription { name = "enabled", defaultValue = true };
        public override void Init(VisualElement ve, IUxmlAttributes attributes, CreationContext context)
        {
            base.Init(ve, attributes, context);
            AthenaButton instance = (AthenaButton)ve;
            instance.enabled = enabledAttr.GetValueFromBag(attributes, context);
        }
    }
}

