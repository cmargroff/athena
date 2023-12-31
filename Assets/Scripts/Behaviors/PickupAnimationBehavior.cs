using UnityEngine;

[RequireComponent(typeof(BaseStateMachineBehavior))]
[RequireComponent(typeof(PickupBehavior))]
public class PickupAnimationBehavior : AthenaMonoBehavior, IDeath
{
    public float TravelSpeed = 1f;
    private GameObject _player;
    private Vector3 _angleAway;
    private float _travelDuration = 0f;
    private Vector3 _direction;
    public override void OnActive()
    {
        base.OnActive();
        _player = _gameManager.Player;
        _angleAway = (transform.position - _player.transform.position).normalized;
        _travelDuration = 0f;
    }

    protected override void PlausibleUpdate()
    {

        var toPlayer = _player.transform.position - transform.position;

        // if the distance is less than 0.1f, then we're close enough to the player to be picked up
        if (toPlayer.magnitude < 0.5f)
        {
            _gameManager.CollectPickup(GetComponent<PickupBehavior>());
            gameObject.SetActive(false);
            return;
        }

        _direction = Vector3.Lerp(_angleAway, toPlayer, _travelDuration);
        transform.position += _direction * (TravelSpeed * Time.deltaTime);

        _travelDuration = Mathf.Clamp01(_travelDuration + Time.deltaTime);
    }
}