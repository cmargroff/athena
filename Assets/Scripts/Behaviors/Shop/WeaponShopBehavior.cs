using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public  class WeaponShopBehavior : ShopBehavior<WeaponSO>
{
    protected override void Buy(WeaponSO weapon)
    {
        _gameManager.Pickups["Coin"] -= weapon.Cost;
        var weaponBehavior = (BaseWeaponBehavior)_gameManager.Weapons.AddComponent(Type.GetType(weapon.Behavior.ToString()));
        weaponBehavior.WeaponConfig = weapon;
        weaponBehavior.enabled = true;
        Items.Remove(weapon);
        BuildShop();
    }
}
