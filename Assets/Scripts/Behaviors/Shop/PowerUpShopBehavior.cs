using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PowerUpShopBehavior : ShopBehavior<PowerUpSO>
{
    protected override string GetTitle()
    {
        return "Research Lab";
    }

    public override void Buy(PowerUpSO item)
    {
        base.Buy(item);
        _gameManager.PlayerCharacter.Armor += item.Armor;
        _gameManager.PlayerCharacter.Knockback += item.Knockback;
        _gameManager.PlayerCharacter.Speed += item.Speed;
        _gameManager.PlayerCharacter.Damage += item.Damage;

        if (item.AttackFrequency > 0)
        {
            _gameManager.PlayerCharacter.AttackFrequency += item.AttackFrequency;
            _gameManager.PlayerCharacter.OnStatsChanged?.Invoke();
        }
    }
}

