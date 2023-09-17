using UnityEngine.Events;

public class BuildingStatAdjust : StatAdjust
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
        return _gameManager.BuildingCharacter.AttackFrequency*_gameManager.BuildingAttackFireRate;
    }
    public override float GetBulletSize()
    {
        return _gameManager.BuildingCharacter.BulletSize;
    }
}