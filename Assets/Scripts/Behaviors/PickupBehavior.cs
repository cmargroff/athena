using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupBehavior : AthenaMonoBehavior, IAlive
{
    public int Amount = 1;
    public Color Color;
    public PickupTypeEnum Type;
    public Action SpecialAction;
    public override void OnActive()
    {
        base.OnActive();
        var spriteMat = GetComponentInChildren<SpriteRenderer>().material;
        // clean up this switch statement to something more readable
        spriteMat.color = Color;
        var trail = GetComponentInChildren<TrailRenderer>();
        trail.colorGradient = new Gradient()
        {
            colorKeys = new []
            {
                new GradientColorKey(Color,0),
                new GradientColorKey(Color,1)
            }
        };
        if (Type == PickupTypeEnum.Health)
        {
            SpecialAction = Heal;
        }

    }
    public void Pickup()
    {
        _stateMachine.SetState(typeof(IDeath));
    }

    private void Heal()
    {
        var playerVulnerable = _gameManager.Player.GetComponent<VulnerableBehavior>();
        playerVulnerable.Health = Math.Min(playerVulnerable.MaxHealth,
            playerVulnerable.Health + playerVulnerable.MaxHealth * 0.1f);
        _gameManager.UpdatePlayerHealth(playerVulnerable.Health / playerVulnerable.MaxHealth);
    }

}