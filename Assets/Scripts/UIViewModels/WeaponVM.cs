using System;

public class WeaponVM : UIVM
{
    [VisualElementBind]
    public string Name;
    [VisualElementBind]
    public string Description;
    [VisualElementBind]
    public string Cost;

    [VisualElementEvent]
    public Action Buy;

    [VisualElementBind(controlName: "Buy", "enabled")]
    public bool CanBuy;
}