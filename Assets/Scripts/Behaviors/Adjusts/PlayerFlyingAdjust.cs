using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerFlyingAdjust:FlyingStatAdjust
{
    public override float GetSpeedAdjust()
    {
        return _gameManager.PlayerCharacterBehavior.Speed;
    }
}

