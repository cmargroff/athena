using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.UIViewModels.Attributes;
using UnityEngine.UIElements;

public abstract class UIVM
{
    public virtual void Bind(VisualElement element)
    {
        foreach (var field in this.GetType().GetFields())
        {
            foreach (VisualElementBindAttribute attribute in field.GetCustomAttributes(typeof(VisualElementBindAttribute), true))
            {
                var controlName = attribute.ControlName?? field.Name;
                var control = element.Q(controlName);
                control.SetPropertyValue(attribute.FieldName, field.GetValue(this).ToString());
            }
            foreach (VisualElementEventAttribute attribute in field.GetCustomAttributes(typeof(VisualElementEventAttribute), true))
            {
                if (field.FieldType != typeof(Action))
                {
                    throw new Exception("Only actions can be used as events");
                }

                var controlName = attribute.ControlName ?? field.Name;
                var control = element.Q<Button>(controlName);
                
                control.RegisterCallback<ClickEvent>(evt =>((Action)field.GetValue(this))() );
                control.RegisterCallback<NavigationSubmitEvent>(evt => ((Action)field.GetValue(this))());
            }

        }
    }
}

