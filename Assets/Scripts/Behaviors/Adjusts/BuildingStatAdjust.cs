using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;


public  class BuildingStatAdjust : StatAdjust
{
    protected override void Start()
    {
        base.Start();
        OnStatsChanged = new UnityEvent();
        _gameManager.BuildingCharacter.OnStatsChanged.AddListener(
            () => OnStatsChanged?.Invoke()
        );
    }



    public override float GetAttackFrequency()
    {
        return _gameManager.BuildingCharacter.AttackFrequency;
    }


}

