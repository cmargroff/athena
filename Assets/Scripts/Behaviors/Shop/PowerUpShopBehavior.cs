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
        _gameManager.PlayerCharacter.BulletSize += item.BulletSize;
        if (item.AttackFrequency > 0)
        {
            _gameManager.PlayerCharacter.AttackFrequency += item.AttackFrequency;
            _gameManager.PlayerCharacter.OnStatsChanged?.Invoke();
        }
    }
}

