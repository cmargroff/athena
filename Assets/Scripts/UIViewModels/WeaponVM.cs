using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.UIViewModels.Attributes;
using UnityEngine.UIElements;

public class WeaponVM: UIVM
{
    [VisualElementBind]
    public string Name;
    [VisualElementBind]
    public string Description;
    public string Cost;


}

