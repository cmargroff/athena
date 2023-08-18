using UnityEngine;

public class MilitaryShopBehavior : ShopBehavior<PowerUpSO>
{
    [SerializeField]
    private WeaponSO _buildingTurretConfig;
    private ShopBuildingBehavior[] _buildings;
    protected override void Start()
    {
        base.Start();
        _buildings = FindObjectsByType<ShopBuildingBehavior>(FindObjectsSortMode.None);
        SafeAssigned(_buildingTurretConfig);

    }
    protected override string GetTitle()
    {
        return "Research Lab";
    }
    public override void Buy(PowerUpSO item)
    {
        base.Buy(item);

        foreach (var building in _buildings)
        {
            if (building.GetComponent<WeaponBehavior>() == null)
            {
                building.gameObject.AddComponent(typeof(BuildingStatAdjust));
                var weaponBehavior = (BaseWeaponBehavior)building.gameObject.AddComponent(typeof(WeaponBehavior));
                weaponBehavior.WeaponConfig = _buildingTurretConfig;
                weaponBehavior.enabled = true;
            }
        }
        _gameManager.BuildingCharacter.Knockback += item.Knockback;
        _gameManager.BuildingCharacter.Damage += item.Damage;
        if (item.AttackFrequency > 0)
        {
            _gameManager.BuildingCharacter.AttackFrequency += item.AttackFrequency;
            _gameManager.BuildingCharacter.OnStatsChanged?.Invoke();
        }
    }
}