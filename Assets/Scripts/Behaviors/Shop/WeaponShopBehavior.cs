using System;

public class WeaponShopBehavior : ShopBehavior<WeaponSO>
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
