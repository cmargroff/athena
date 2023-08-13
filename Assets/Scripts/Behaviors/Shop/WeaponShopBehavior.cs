using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public  class WeaponShopBehavior : ShopBehavior<WeaponSO>
{
    protected override string GetTitle()
    {
        return "Mech Hangar";
    }

    public override void Buy(WeaponSO item)
    {
        base.Buy(item);
        var weaponBehavior = (BaseWeaponBehavior)_gameManager.Weapons.AddComponent(Type.GetType(item.Behavior.ToString()));
        weaponBehavior.WeaponConfig = item;
        weaponBehavior.enabled = true;
    }
}
