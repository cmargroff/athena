using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(FlyingBehavior))]
public class  ChaseBehavior: AthenaMonoBehavior, IAlive
{

    private FlyingBehavior _flying;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _flying = GetComponent<FlyingBehavior>();
        SafeAssigned(_gameManager);
    }

    // Update is called once per frame
    protected override void PausibleUpdate()
    {
        var target =  _gameManager.Player.transform.position - _flying.transform.position;
        _flying.MoveAngle = new Vector2(target.x, target.y );
    }
}
