public class PlayerStatAdjust : StatAdjust
{
    protected override void Start()
    {
        base.Start();
        _gameManager.PlayerCharacter.OnStatsChanged.AddListener(
            () => OnStatsChanged?.Invoke()
            );
    }
    public override float GetSpeedAdjust()
    {
        return _gameManager.PlayerCharacter.Speed;
    }
    public override float GetArmorAdjust()
    {
        return _gameManager.PlayerCharacter.Armor;
    }
    public override float GetAttackFrequency()
    {
        return _gameManager.PlayerCharacter.AttackFrequency;
    }
}