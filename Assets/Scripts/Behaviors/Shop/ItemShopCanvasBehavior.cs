public class ItemShopCanvasBehavior : ShopCanvasBehavior<PowerUpSO>
{
    protected override string GetTitle()
    {
        return "Items";
    }
    public override void Buy(PowerUpSO item)
    {
        base.Buy(item);
        _gameManager.PlayerCharacter.Armor += item.Armor;
        _gameManager.PlayerCharacter.Knockback += item.Knockback;
        _gameManager.PlayerCharacter.Speed += item.Speed;
        _gameManager.PlayerCharacter.Damage += item.Damage;
        _gameManager.PlayerCharacter.BulletSize += item.BulletSize;

        _gameManager.PlayerCharacter.AttackFrequency += item.AttackFrequency;

        if (item.AttackFrequency > 0)
        {
            _gameManager.PlayerCharacter.OnStatsChanged?.Invoke();
        }
    }
}