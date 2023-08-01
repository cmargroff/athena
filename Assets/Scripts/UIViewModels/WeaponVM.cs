using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

public class WeaponVM: UIVM
{
    [VisualElementBind]
    public string Name;
    [VisualElementBind]
    public string Description;
    [VisualElementBind]
    public int Cost;

    [VisualElementEventAttribute] 
    public Action Buy;

}

