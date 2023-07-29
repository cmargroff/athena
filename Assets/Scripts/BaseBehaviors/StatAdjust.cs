using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

public  class  StatAdjust:AthenaMonoBehavior
{
    public virtual float GetSpeedAdjust()
    {
        return 1f;
    }
    public virtual float GetArmorAdjust()
    {
        return 1f;
    }
 
    public  UnityEvent OnStatsChanged;

    public virtual float GetAttackFrequency()
    {
        return 1f;
    }


  
}

