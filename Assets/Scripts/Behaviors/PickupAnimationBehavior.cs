using DG.Tweening;
using UnityEngine;
using UnityEditor;
[RequireComponent(typeof(BaseStateMachineBehavior))]
[RequireComponent(typeof(PickupBehavior))]
public  class PickupAnimationBehavior: AthenaMonoBehavior
{
    public float TravelSpeed = 1f;
    private GameObject _player;
    private Vector3 _angleAway;
    private bool _running = false;
    private float _travelDuration = 0f;
    private Vector3 _direction;
    public void Run()
    {
        if(_running) return;
        _player = _gameManager.Player;
        _angleAway = (transform.position - _player.transform.position).normalized;
        _travelDuration = 0f;
        _running = true;
    }
    protected override void PlausibleFixedUpdate(){
        if(_running){
            var toPlayer = _player.transform.position - transform.position;
            
            // if the distance is less than 0.1f, then we're close enough to the player to be picked up
            if(toPlayer.magnitude < 0.5f){
                _gameManager.CollectPickup(GetComponent<PickupBehavior>());
                _running = false;
                return;
            }

            _direction = Vector3.Lerp(_angleAway, toPlayer, _travelDuration);
            transform.position += _direction * TravelSpeed * Time.fixedDeltaTime;

            _travelDuration = Mathf.Clamp01(_travelDuration + Time.fixedDeltaTime);
        }
    }
    private void OnDrawGizmos() {
        if(_running){
            Handles.color = Color.cyan;
            Handles.ArrowHandleCap(0, transform.position, Quaternion.LookRotation(_direction), _direction.magnitude, EventType.Repaint);
        }    
    }
}